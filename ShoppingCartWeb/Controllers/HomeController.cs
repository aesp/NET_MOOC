using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartWeb.BL;

namespace ShoppingCartWeb.Controllers
{
    public class HomeController : Controller
	{
        ProductBL ProductBL;

        public HomeController(ProductBL ProductBL)
        {
            this.ProductBL = ProductBL;
        }

        public async Task<IActionResult> Index()
        {
            var products = await ProductBL.GetProduct();
            Console.WriteLine("Products: " + products);
            return View(products);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


    }
}
