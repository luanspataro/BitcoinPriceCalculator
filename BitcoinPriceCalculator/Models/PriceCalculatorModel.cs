using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinPriceCalculator.Models
{
    public class PriceCalculatorModel
    {
        public int Currency { get; set; }
        public int ActualPrice { get; set; }
        public DateTime PurchaseDate { get; set; }

    }
}
