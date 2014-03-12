using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMongoMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting the migration...");
            var migrator = new AdventureMongoMigrator();
            migrator.Migrate();

            Console.WriteLine("Migration complete.");
            Console.ReadKey();
        }
    }
}
