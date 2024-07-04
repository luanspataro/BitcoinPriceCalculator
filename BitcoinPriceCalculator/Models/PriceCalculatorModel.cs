using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinPriceCalculator.Models
{
    public class PriceCalculatorModel
    {
        private double BTCAmount { get; set; }
        private double Profit { get; set; }

        public string Currency { get; set; }
        public int ActualPrice { get; set; }
        public DateTime PurchaseDate { get; set; }

    }
}
