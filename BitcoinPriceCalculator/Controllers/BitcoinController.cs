using BitcoinPriceCalculator.Models;
using BitcoinPriceCalculator.Integration.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public async Task<IActionResult> GetBitcoinPriceAsync([FromBody] PriceCalculatorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var btcData = _bitcoinService.BtcCalc(model.PurchaseDate);
                    BitPrecoResponse response = await _bitcoinService.Integration();

                    var profitData = _bitcoinService.ProfitCalc(model.PurchaseValue, btcData.Item1, btcData.Item2, response.Avg);

                    decimal amount = profitData.Item1;
                    decimal percentage = profitData.Item2;
                    decimal profit = profitData.Item3;

                    return Json(new { success = true, amount = amount.ToString("0.#####"), percentage = percentage, profit = profit.ToString("C2", new System.Globalization.CultureInfo("pt-BR")) });
                }
                catch (InvalidOperationException ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }
            }
            return Json(new { success = false, error = "Preencha os campos corretamente." });
        }
    }
}