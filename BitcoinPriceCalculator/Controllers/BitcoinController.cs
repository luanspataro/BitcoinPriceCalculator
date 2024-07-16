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
        public IActionResult Index(PriceCalculatorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    decimal btcPrice = _bitcoinService.BtcCalc(model.PurchaseDate);
                    ViewBag.BtcPrice = btcPrice.ToString("C2", new System.Globalization.CultureInfo("pt-BR"));

                    return View("~/Views/Home/Index.cshtml", model);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, $"Erro: {ex.Message}");
                }
            }

            return View("~/Views/Home/Index.cshtml", model);
        }
    }
}