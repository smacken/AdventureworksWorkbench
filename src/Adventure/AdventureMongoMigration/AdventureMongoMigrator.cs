using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Core;
using System.Data.Entity;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using AdventureMongoMigration.Model;
using AutoMapper;

namespace AdventureMongoMigration
{
    public class AdventureMongoMigrator
    {
        public MongoDatabase TargetDb { get; set; }

        public MongoCollection<MongoProduct> Products { get { return TargetDb.GetCollection<MongoProduct>("Products"); } }
        public MongoCollection<MongoCustomer> Customers { get { return TargetDb.GetCollection<MongoCustomer>("Customers"); } }
        public MongoCollection<MongoSales> Sales { get { return TargetDb.GetCollection<MongoSales>("Sales"); } }
        public MongoCollection<MongoCategory> Categories { get { return TargetDb.GetCollection<MongoCategory>("Categories"); } }

        /// <summary>
        /// The mongo server and database could be configurable
        /// </summary>
        public AdventureMongoMigrator()
        {
            var server = new MongoClient("mongodb://localhost").GetServer();
            this.TargetDb = server.GetDatabase("Adventure");

            Ensure("Products", "Customers", "Sales", "Categories");

            CreateDataMappings();
        }

        
        /// <summary>
        /// Migrate the database into a flatter nosql document structure
        /// </summary>
        public void Migrate()
        {
            Drop(Products, Customers, Sales, Categories);

            using (var db = new AdventureDB())
            {
                MigrateProducts(db);

                MigrateCustomers(db);

                MigrateSales(db);

                MigrateCategories(db);
            }
        }

        private void MigrateCategories(AdventureDB db)
        {
            var parentCategories = db.ProductCategories.Where(cat => cat.ParentProductCategoryID == null);
            
            Console.WriteLine("Categories - " + parentCategories.Count());
            
            foreach (var parentCategory in parentCategories)
            {
                ProductCategory category = parentCategory;
                var childCategories = db.ProductCategories.Where(cat => cat.ParentProductCategoryID == category.ProductCategoryID);

                var mongoCategory = Mapper.Map<MongoCategory>(category);
                mongoCategory.ChildCategories = Mapper.Map<List<MongoCategory>>(childCategories);

                Categories.Insert(mongoCategory);
            }
        }

        private void MigrateProducts(AdventureDB db)
        {
            var dbProducts = db.Products.Include(product => product.ProductCategory)
                                        .Include(product => product.ProductModel);

            Console.WriteLine("Products - " + dbProducts.Count());

            Products.InsertBatch<MongoProduct>(Mapper.Map<List<MongoProduct>>(dbProducts.ToList()));
        }

        private void MigrateCustomers(AdventureDB db)
        {
            // customers

            var dbCustomers = db.Customers.Include(cust => cust.CustomerAddresses)
                                          .Include(cust => cust.SalesOrderHeaders);
            var customers = Mapper.Map<List<MongoCustomer>>(dbCustomers.ToList());

            Console.WriteLine("Customers - " + customers.Count);

            foreach (var customer in customers)
            {
                var addresses = from address in db.CustomerAddresses.Include(ca => ca.Address)
                                where address.CustomerID == customer.CustomerID &&
                                      address.Address != null
                                select address.Address;

                var addressList = addresses.Where(ad => ad != null).ToList();
                if (addressList.Any())
                    customer.Addresses.AddRange(Mapper.Map<List<MongoAddress>>(addressList.ToList()));

                var sales = from s in db.SalesOrderHeaders.Include(c => c.SalesOrderDetails)
                            where s.CustomerID == customer.CustomerID &&
                                  s != null
                            select s;

                if (sales.Any())
                {
                    var customerSales = Mapper.Map<List<MongoSales>>(sales.ToList());
                    customer.Sales.AddRange(customerSales);
                }
            }
            Customers.InsertBatch<MongoCustomer>(customers);
        }

        private void MigrateSales(AdventureDB db)
        {
            // sales

            var dbSales = db.SalesOrderHeaders
                            .Include(sales => sales.SalesOrderDetails)
                            .ToList();

            Console.WriteLine("Sales - " + dbSales.Count);

            if (dbSales.Any())
            {
                var orders = Mapper.Map<List<MongoSales>>(dbSales);

                Sales.InsertBatch<MongoSales>(orders);
            }
        }

        private void Ensure(params string[] collections)
        {
            foreach (var collection in collections)
            {
                if (!TargetDb.CollectionExists(collection))
                    TargetDb.CreateCollection(collection);
            }
        }

        private void Drop(params MongoCollection[] collections)
        {
            foreach (var collection in collections)
            {
                if (TargetDb.CollectionExists(collection.Name))
                    collection.Drop();
            }
        }

        private void CreateDataMappings()
        {
            AutoMapper.Mapper.CreateMap<Product, MongoProduct>();
            AutoMapper.Mapper.CreateMap<ProductCategory, MongoCategory>();
            AutoMapper.Mapper.CreateMap<ProductModel, MongoProductModel>();

            AutoMapper.Mapper.CreateMap<Customer, MongoCustomer>();
            AutoMapper.Mapper.CreateMap<Address, MongoAddress>();

            AutoMapper.Mapper.CreateMap<SalesOrderHeader, MongoSales>();
            AutoMapper.Mapper.CreateMap<SalesOrderDetail, MongoSalesDetail>();
        }
    }
}
