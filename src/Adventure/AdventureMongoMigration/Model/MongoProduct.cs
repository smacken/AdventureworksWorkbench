using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMongoMigration.Model
{
    public class MongoProduct
    {
        public ObjectId Id { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<int> ProductCategoryID { get; set; }
        public Nullable<int> ProductModelID { get; set; }
        public System.DateTime SellStartDate { get; set; }
        public Nullable<System.DateTime> SellEndDate { get; set; }
        public Nullable<System.DateTime> DiscontinuedDate { get; set; }
        public byte[] ThumbNailPhoto { get; set; }
        public string ThumbnailPhotoFileName { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        public virtual MongoCategory ProductCategory { get; set; }
        public virtual MongoProductModel ProductModel { get; set; }
    }

    public class MongoCategory
    {
        public MongoCategory()
        {
            ChildCategories = new List<MongoCategory>();
        }
        public ObjectId Id { get; set; }
        public int ProductCategoryID { get; set; }
        public Nullable<int> ParentProductCategoryID { get; set; }
        public string Name { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual List<MongoCategory> ChildCategories { get; set; }
    }

    public class MongoProductModel
    {
        public int ProductModelID { get; set; }
        public string Name { get; set; }
        public string CatalogDescription { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
