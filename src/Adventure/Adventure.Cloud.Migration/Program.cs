using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Cloud.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            var cloudMigrator = new CloudMigrator();
            cloudMigrator.Configure();
            cloudMigrator.Migrate();
        }
    }
}
