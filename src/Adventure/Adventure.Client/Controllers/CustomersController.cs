using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Adventure.Client.Services;
using ServiceStack.Plugins.MsgPack;
using ServiceStack.Plugins.ProtoBuf;

namespace Adventure.Client.Controllers
{
    public class CustomersController : Controller
    {
        //
        // GET: /Customers/

        public ActionResult Index()
        {
            var client = new ProtoBufServiceClient(@"http://localhost:40147");
            var customers = client.Get<List<CustomerDto>>("/api/customers");
            //var customers = client.Get<QueryResponse<List<CustomerDto>>>("/api/customers");

            return View(customers);
        }

        //public ActionResult PostCsv()
        //{
        //    var client = new MsgPackServiceClient(@"http://localhost:40147");
        //    //client.PostFile<>()
        //    return JsonResult();
        //}

    }
}
