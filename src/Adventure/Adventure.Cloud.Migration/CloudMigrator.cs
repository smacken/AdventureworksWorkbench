using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure;
using Adventure.Cloud;

namespace Adventure.Cloud.Migration
{
    public class CloudMigrator
    {
        //StorageRole.WorkerRole _cloudWorker;
        public CloudMigrator()
        {
            
          //  _cloudWorker = new StorageRole.WorkerRole();
            
        }

        public void Migrate()
        { 
        
        }

        internal void Configure()
        {
            //var tableClient = _cloudWorker.GetTableClient();
        }
    }
}
