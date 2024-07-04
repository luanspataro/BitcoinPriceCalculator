using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinPriceCalculator.Controllers
{
    public class BitcoinPriceCalculator : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
