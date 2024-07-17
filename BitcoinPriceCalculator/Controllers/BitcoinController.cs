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

        public IActionResult Create()
        {
            PriceCalculatorModel model = new PriceCalculatorModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult GetBitcoinPrice([FromBody] PriceCalculatorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    decimal btcPrice = _bitcoinService.BtcCalc(model.PurchaseDate);
                    return Json(new { success = true, price = btcPrice.ToString("C2", new System.Globalization.CultureInfo("pt-BR")) });
                }
                catch (InvalidOperationException ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }
            }
            return Json(new { success = false, error = "Dados inválidos." });
        }

    }
}