using System;
using System.Runtime.Serialization;
using MongoDB.Bson;

namespace Adventure.Client.Models
{
    public class Product
    {
        public Product()
        {
        }

        public Product(Int32 id)
        {
                
        }

        [DataMember(Order = 1)]
        public ObjectId Id { get; set; }
        [DataMember(Order = 2)]
        public int ProductID { get; set; }
        [DataMember(Order = 3)]
        public string Name { get; set; }
        [DataMember(Order = 4)]
        public string ProductNumber { get; set; }
        [DataMember(Order = 5)]
        public string Color { get; set; }
        [DataMember(Order = 6)]
        public decimal StandardCost { get; set; }
        [DataMember(Order = 7)]
        public decimal ListPrice { get; set; }
        [DataMember(Order = 8)]
        public string Size { get; set; }
        [DataMember(Order = 9)]
        public Nullable<decimal> Weight { get; set; }
        [DataMember(Order = 10)]
        public Nullable<int> ProductCategoryID { get; set; }
        [DataMember(Order = 11)]
        public Nullable<int> ProductModelID { get; set; }
        [DataMember(Order = 12)]
        public System.DateTime SellStartDate { get; set; }
        [DataMember(Order = 13)]
        public Nullable<System.DateTime> SellEndDate { get; set; }
        [DataMember(Order = 14)]
        public Nullable<System.DateTime> DiscontinuedDate { get; set; }
        [DataMember(Order = 15)]
        public byte[] ThumbNailPhoto { get; set; }
        [DataMember(Order = 16)]
        public string ThumbnailPhotoFileName { get; set; }
        [DataMember(Order = 17)]
        public System.Guid rowguid { get; set; }
        [DataMember(Order = 18)]
        public System.DateTime ModifiedDate { get; set; }

        [DataMember(Order = 19)]
        public virtual Category ProductCategory { get; set; }
        [DataMember(Order = 20)]
        public virtual ProductModel ProductModel { get; set; }
    }

    public class Category
    {
        public Category()
        {
        }

        [DataMember(Order = 1)]
        public int ProductCategoryID { get; set; }
        [DataMember(Order = 2)]
        public Nullable<int> ParentProductCategoryID { get; set; }
        [DataMember(Order = 3)]
        public string Name { get; set; }
        [DataMember(Order = 4)]
        public System.Guid rowguid { get; set; }
        [DataMember(Order = 5)]
        public System.DateTime ModifiedDate { get; set; }
    }

    public class ProductModel
    {
        public ProductModel()
        {
            
        }

        [DataMember(Order = 1)]
        public int ProductModelID { get; set; }
        [DataMember(Order = 2)]
        public string Name { get; set; }
        [DataMember(Order = 3)]
        public string CatalogDescription { get; set; }
        [DataMember(Order = 4)]
        public System.Guid rowguid { get; set; }
        [DataMember(Order = 5)]
        public System.DateTime ModifiedDate { get; set; }
    }
}