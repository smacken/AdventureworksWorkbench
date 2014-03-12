using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Adventure.Client.Models
{
    [DataContract]
    public class ProductDto
    {
        [DataMember(Order = 1)]
        public int ProductId { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public string ProductNumber { get; set; }
        [DataMember(Order = 4)]
        public string Color { get; set; }
        [DataMember(Order = 5)]
        public decimal StandardCost { get; set; }
        [DataMember(Order = 6)]
        public decimal ListPrice { get; set; }
        [DataMember(Order = 7)]
        public string Size { get; set; }
        [DataMember(Order = 8)]
        public Nullable<decimal> Weight { get; set; }
        [DataMember(Order = 9)]
        public Nullable<int> ProductCategoryID { get; set; }
        [DataMember(Order = 10)]
        public Nullable<int> ProductModelID { get; set; }
        [DataMember(Order = 11)]
        public System.DateTime SellStartDate { get; set; }
        [DataMember(Order = 12)]
        public Nullable<System.DateTime> SellEndDate { get; set; }
        [DataMember(Order = 13)]
        public Nullable<System.DateTime> DiscontinuedDate { get; set; }
        [DataMember(Order = 14)]
        public byte[] ThumbNailPhoto { get; set; }
        [DataMember(Order = 15)]
        public string ThumbnailPhotoFileName { get; set; }
        [DataMember(Order = 16)]
        public System.Guid rowguid { get; set; }
        [DataMember(Order = 17)]
        public System.DateTime ModifiedDate { get; set; }
    }

    [DataContract]
    public class CategoryDto
    {
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
        [DataMember(Order = 6)]
        public List<CategoryDto> ChildCategories { get; set; }
    }
    [DataContract]
    public class ProductModelDto
    {
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