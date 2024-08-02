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
        private readonly BitcoinService _bitcoinService;
        
        public BitcoinController(BitcoinService bitcoinService)
        {
            _bitcoinService = bitcoinService;
        }

        public IActionResult Index()
        {
            return View(new PriceCalculatorModel());
        }

        [HttpPost]
        public IActionResult GetBitcoinPrice([FromBody] PriceCalculatorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var btcData = _bitcoinService.BtcCalc(model.PurchaseDate);
                    /*decimal btcPrice = btcData.Item1;
                    DateTime priceDate = btcData.Item2;*/

                    var profitData = _bitcoinService.ProfitCalc(btcData.Item1, btcData.Item2, ("api price"));

                    decimal amount, decimal percentage, decimal profit = profitData;
                    /*return Json(new { success = true, price = btcPrice.ToString("C2", new System.Globalization.CultureInfo("pt-BR")) });*/
                    return Json(new { success = true, price = btcPrice.ToString("C2", new System.Globalization.CultureInfo("pt-BR")) });
                }
                catch (InvalidOperationException ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }
            }
            return Json(new { success = false, error = "Data Inválida." });
        }
    }
}