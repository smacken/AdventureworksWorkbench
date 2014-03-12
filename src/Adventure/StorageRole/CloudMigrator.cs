using Adventure.Core;
using AutoMapper;
using AzureToolkit;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using StorageRole.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageRole
{
    public class CloudMigrator
    {
        private   Microsoft.WindowsAzure.Storage.Table.CloudTableClient tableClient;
        public CloudTable Products { get; set; }
        public CloudTable Sales { get; set; }
        public CloudTable Customers { get; set; }



        public int MyProperty { get; set; }

        public CloudMigrator(Microsoft.WindowsAzure.Storage.Table.CloudTableClient tableClient)
        {
            this.tableClient = tableClient;
        }

        public CloudMigrator()
        {
            
        }

        public CloudMigrator ConfigureThen()
        {
            CreateMigrationMappings();

            var products = tableClient.GetTableReference("Products");
            bool exists = products.Exists();

            if (exists)
            {
                products.Delete();
                products.Create();
            }
            else {
                products.Create();
            }
            //products.DeleteIfExists();
            //products.CreateIfNotExists();

            Products = products;
            return this;
        }

        private void Drop(params string[] tables)
        {
            foreach (var table in tables)
            {
                tableClient.GetTableReference(table).DeleteIfExists();
            }
        }

        private void Ensure(params string[] tables)
        {
            foreach (var table in tables)
            {
                tableClient.GetTableReference(table).CreateIfNotExists();
            }
        }

        public void Migrate()
        {
            
            using (var db = new AdventureDB())
            {
                var createTableBatch = new TableBatchOperation { };
                var dbProducts = db.Products.Include(x => x.ProductCategory).ToList();

                foreach (var prod in dbProducts)
                {
                    var productTable = prod.AsProductTable();
                    Products.Execute(TableOperation.Insert(productTable));
                }

                //dbProducts.ForEach(product => createTableBatch.Insert(product.AsProductTable()));

                //Products.ExecuteBatch(createTableBatch);
            }
        }

        protected virtual void CreateMigrationMappings()
        {
            AutoMapper.Mapper.CreateMap<Product, ProductTable>();
        }     
    }

    public static class DbExtensions
    {
        public static ProductTable AsProductTable(this Product product)
        {
            var productTable = Mapper.Map<ProductTable>(product);
            productTable.RowKey = product.ProductID.ToString();
            productTable.PartitionKey = product.ProductCategoryID.ToString();
            return productTable;
        }

        public static IEnumerable<ProductTable> AsProductTable(this IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                yield return product.AsProductTable();
            }
        }
    }
}
