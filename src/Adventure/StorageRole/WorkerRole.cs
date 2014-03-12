using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using AzureToolkit;

namespace StorageRole
{
    public class WorkerRole : RoleEntryPoint
    {
        CloudStorageAccount _storageAccount;
        private bool _isRunning = false;

        

        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("StorageRole entry point called", "Information");

            while (true)
            {
                Thread.Sleep(10000);
                Trace.TraceInformation("Working", "Information");
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;
            _isRunning = true;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            _storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            //_storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("CloudConnectionString"));

            var tableClient = this.GetTableClient();

            var cloudMigrator = new CloudMigrator(tableClient);
            cloudMigrator.ConfigureThen().Migrate();

            return base.OnStart();
        }

        public CloudTableClient GetTableClient()
        {
            if (!_isRunning) throw new Exception("The worker role has to be started before retrieving the Table client");

            return _storageAccount.CreateCloudTableClient();
        }
    }
}
