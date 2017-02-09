using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LimitLess.Controllers
{
    public class SpreadsheetController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveData(IDictionary<string, object> excelData)
        {
            Console.WriteLine(excelData);
            return new JsonResult { Data = new { name = "a"} };
        }
    }
}