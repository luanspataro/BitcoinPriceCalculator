using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BitcoinPriceCalculator.Models
{
    public class PriceCalculatorModel
    {
        public double BTCAmount { get; set; }
        public double Profit { get; set; }

        public string Currency { get; set; }
        public int ActualPrice { get; set; }


        [Display(Name = "Selecione uma data")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Por favor, selecione uma data.")]
        public DateTime PurchaseDate { get; set; }

    }

    public class Bitcoin
    {
        public decimal Price { get; set; }
        public DateTime PriceDate { get; set; }
    }

}
