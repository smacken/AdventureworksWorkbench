using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Adventure.Client.Models;
using Adventure.Client.Services;
using ServiceStack.Plugins.MsgPack;

namespace Adventure.Client.Controllers
{
    public class ProductsController : Controller
    {
        //
        // GET: /Products/

        public ActionResult Index()
        {
            var client = new MsgPackServiceClient(@"http://localhost:40147");
            Stopwatch duration = new Stopwatch();
            duration.Start();

            var products = client.Get<QueryResponse<List<ProductDto>>>("/api/products?pageNumber=1&pageSize=200");
            
            duration.Stop();
            return View(products.Results);
        }

    }
}
