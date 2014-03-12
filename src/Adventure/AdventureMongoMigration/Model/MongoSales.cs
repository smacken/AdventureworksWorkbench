using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMongoMigration.Model
{
    public class MongoSales
    {

        public MongoSales()
        {
            this.SalesOrderDetails = new List<MongoSalesDetail>();
        }
        public ObjectId Id { get; set; }

        public int SalesOrderID { get; set; }
        public byte RevisionNumber { get; set; }
        public System.DateTime OrderDate { get; set; }
        public System.DateTime DueDate { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public byte Status { get; set; }
        public bool OnlineOrderFlag { get; set; }
        public string SalesOrderNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string AccountNumber { get; set; }
        public int CustomerID { get; set; }
        public Nullable<int> ShipToAddressID { get; set; }
        public Nullable<int> BillToAddressID { get; set; }
        public string ShipMethod { get; set; }
        public string CreditCardApprovalCode { get; set; }
        public double SubTotal { get; set; }
        public double TaxAmt { get; set; }
        public double Freight { get; set; }
        public double TotalDue { get; set; }
        public string Comment { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        public virtual List<MongoSalesDetail> SalesOrderDetails { get; set; }
    }

    public class MongoSalesDetail
    {
        public int SalesOrderID { get; set; }
        public int SalesOrderDetailID { get; set; }
        public short OrderQty { get; set; }
        public int ProductID { get; set; }
        public double UnitPrice { get; set; }
        public double UnitPriceDiscount { get; set; }
        public double LineTotal { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
