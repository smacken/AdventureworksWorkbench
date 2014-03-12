using System.Collections.Generic;
using System.Linq;
using Adventure.Client.App_Start;
using Adventure.Client.Models;
using AutoMapper;
using MongoDB.Driver.Linq;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Adventure.Client.Services
{

    public class ProductsRequest : RestQuery
    {

    }

    public class ProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductService : Service
    {
        [QueryRequestFilter]
        public QueryResponse<List<ProductDto>> Get(ProductsRequest request)
        {
            var req = request as RestQuery;

            List<Product> response;
            var productList = MongoHost.Db.GetCollection<Product>("Products").AsQueryable();

            if (req == null)
            {
                response = productList.Take(50).ToList();
            }
            else
            {
                var sort = req.Sort;
                response = productList.Skip(req.PageNumber*req.PageSize)
                                      .Take(req.PageSize)
                                      .ToList();
            }
            
            //var products = MongoHost.Db.GetCollection<Product>("Products");
            //var response = products.FindAll().SetLimit(50).;
            
            //return new CompressedResult()

            return new QueryResponse<List<ProductDto>>
            {
                ResponseStatus = new ResponseStatus(),
                Results = Mapper.Map<List<ProductDto>>(response),
                Links = new List<Link>{} // show a link to the next page of results
            };
        }

        public object Post(ProductRequest request)
        {
            return request;
        }
    }
}