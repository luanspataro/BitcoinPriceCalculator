using BitcoinPriceCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinPriceCalculator.Controllers
{
    public class BitcoinController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(DatePicker model)
        {
            if (ModelState.IsValid)
            {
                //decimal btcPrice = model.BtcCalc();
                //ViewBag.BtcPrice = btcPrice;

                return View();
            }
            return View(model);
        }
    }
}
