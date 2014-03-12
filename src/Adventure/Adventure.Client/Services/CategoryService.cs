using System.Collections.Generic;
using System.Linq;
using Adventure.Client.App_Start;
using Adventure.Client.Models;
using AdventureMongoMigration.Model;
using MongoDB.Driver.Linq;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace Adventure.Client.Services
{
    public class CategoriesRequest
    {

    }

    public class CategoryService : Service
    {
        public object Get(CategoriesRequest request)
        {
            var cache = Redis.As<CategoryDto>();

            bool cacheExists = cache.Lists["categories"].Any();

            if (cacheExists)
            {
                var response = cache.Lists["categories"].AsEnumerable();
                return response.ToList<CategoryDto>();
            }

            var categoryList = MongoHost.Db.GetCollection<MongoCategory>("Categories").AsQueryable();

            var categoryResponse = AutoMapper.Mapper.Map<List<CategoryDto>>(categoryList.ToList());
            
            cache.Lists["categories"].Clear();
            cache.Lists["categories"].AddRange(categoryResponse);

            return categoryResponse;
        }
    }

    
}