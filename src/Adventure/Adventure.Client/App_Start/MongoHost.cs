using MongoDB.Driver;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Adventure.Client.App_Start.MongoHost), "Start")]

namespace Adventure.Client.App_Start
{
    public class MongoHost
    {
        public static void Start()
        {
            new MongoHost().InitDb();
        }

        public static MongoDatabase Db { get; set; }

        private void InitDb()
        {
            var server = new MongoClient("mongodb://localhost").GetServer();
            Db = server.GetDatabase("Adventure");

            Ensure("Customers", "Products");
        }

        /// <summary>
        /// Ensures that a list of collections exists
        /// </summary>
        /// <param name="collections">MongoDB collections which are required to exist.</param>
        private void Ensure(params string[] collections)
        {
            foreach (var collection in collections)
            {
                if (!Db.CollectionExists(collection))
                    Db.CreateCollection(collection);
            }
        }
    }
}