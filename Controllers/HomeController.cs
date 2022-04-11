using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomPasscodeGenerator.Models;
using Microsoft.AspNetCore.Http;

namespace RandomPasscodeGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public static string Generator()
        {
            var code = new Dictionary<int, string[]>(){};
            code.Add(1, new string[] { "A", "B", "C", "D", "E", "F", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });
            code.Add(2, new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });

                string Passcode = "";
            for (int i = 0; i < 15; i++)
            {
                Random rand = new Random();
                int numForCode = rand.Next(1,3);
                int numforIndexLetters = rand.Next(0, code[1].Length);
                int numforIndexNumbers = rand.Next(0, code[2].Length);
                if (numForCode == 1)
                {
                    Passcode = Passcode + code[numForCode][numforIndexLetters];
                }
                else if (numForCode == 2)
                {
                    Passcode = Passcode + code[numForCode][numforIndexNumbers];
                }
            }
            Console.WriteLine(Passcode);
            return Passcode;
        }



        


        public IActionResult Index()
        {   
            if(HttpContext.Session.GetInt32("count") == null)
            {
            HttpContext.Session.SetInt32("count",0);
            ViewBag.count = HttpContext.Session.GetInt32("count");
            }else{
            HttpContext.Session.SetInt32("count",(int)HttpContext.Session.GetInt32("count") +1);
            ViewBag.count = HttpContext.Session.GetInt32("count");
            }

            string code = Generator();
            return View("index", code);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
