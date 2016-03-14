using AspNet5Template.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Template.Controllers{
    [Route("api/[controller]")]
    public class HomeController : Controller{
        [HttpGet]
        public IActionResult Index() {
            //new ViewResult() { };
            return View(new TestModel() { Value = "Test Message!" });
        }
    }
}
