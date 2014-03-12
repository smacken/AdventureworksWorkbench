using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Adventure.Client.App_Start;
using Adventure.Core;
using AdventureMongoMigration.Model;
using MongoDB.Driver.Linq;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Adventure.Client.Services
{
    public class CustomersRequest : RestQuery
    {

    }

    public class CustomerService : Service
    {
        [QueryRequestFilter]
        //public QueryResponse<List<CustomerDto>> Get(CustomersRequest request)
        public object Get(CustomersRequest request)
        {
            var cache = Redis.As<CustomerDto>();
            bool cacheExists = cache.Lists["customers"].Any();
            bool enoughResults = cache.Lists["customers"].Count > request.PageSize;

            if (cacheExists && enoughResults)
            {
                var customersResponse = cache.Lists["customers"].AsEnumerable();
                return customersResponse.Skip(request.PageNumber*request.PageSize)
                                        .Take(request.PageSize);
            }

            var customerList = MongoHost.Db.GetCollection<MongoCustomer>("Customers").AsQueryable();

            //var response = customerList.Skip(request.PageNumber*request.PageSize)
            //                           .Take(request.PageSize)
            //                           .ToList();

            var response = customerList.ToList();

            //return new QueryResponse<List<CustomerDto>>
            //{
            //    ResponseStatus = new ResponseStatus(),
            //    Results = AutoMapper.Mapper.Map<List<CustomerDto>>(response),
            //    Links = new List<Link>{}
            //};
            var customerResponse = AutoMapper.Mapper.Map<List<CustomerDto>>(response);

            cache.Lists["customers"].Clear();
            cache.Lists["customers"].AddRange(customerResponse);
            cache.ExpireEntryIn("customers", TimeSpan.FromHours(6));

            return customerResponse.Skip(request.PageNumber * request.PageSize)
                                   .Take(request.PageSize);
        }
    }

    [DataContract]
    public class CustomerDto
    {
        public CustomerDto()
        {
            Addresses = new List<AddressDto>();
        }

        [DataMember(Order = 1)]
        public int CustomerID { get; set; }
        [DataMember(Order = 2)]
        public bool NameStyle { get; set; }
        [DataMember(Order = 3)]
        public string Title { get; set; }
        [DataMember(Order = 4)]
        public string FirstName { get; set; }
        [DataMember(Order = 5)]
        public string MiddleName { get; set; }
        [DataMember(Order = 6)]
        public string LastName { get; set; }
        [DataMember(Order = 7)]
        public string Suffix { get; set; }
        [DataMember(Order = 8)]
        public string CompanyName { get; set; }
        [DataMember(Order = 9)]
        public string SalesPerson { get; set; }
        [DataMember(Order = 10)]
        public string EmailAddress { get; set; }
        [DataMember(Order = 11)]
        public string Phone { get; set; }
        [DataMember(Order = 12)]
        public string PasswordHash { get; set; }
        [DataMember(Order = 13)]
        public string PasswordSalt { get; set; }
        [DataMember(Order = 14)]
        public System.Guid rowguid { get; set; }
        [DataMember(Order = 15)]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember(Order = 16)]
        public virtual List<AddressDto> Addresses { get; set; }
    }

    [DataContract]
    public class AddressDto
    {
        public AddressDto()
        {
            
        }

        [DataMember(Order = 1)]
        public int AddressID { get; set; }
        [DataMember(Order = 2)]
        public string AddressLine1 { get; set; }
        [DataMember(Order = 3)]
        public string AddressLine2 { get; set; }
        [DataMember(Order = 4)]
        public string City { get; set; }
        [DataMember(Order = 5)]
        public string StateProvince { get; set; }
        [DataMember(Order = 6)]
        public string CountryRegion { get; set; }
        [DataMember(Order = 7)]
        public string PostalCode { get; set; }
        [DataMember(Order = 8)]
        public System.Guid rowguid { get; set; }
        [DataMember(Order = 9)]
        public System.DateTime ModifiedDate { get; set; }
    }
}