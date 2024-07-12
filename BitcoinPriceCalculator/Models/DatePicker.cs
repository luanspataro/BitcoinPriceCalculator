using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BitcoinPriceCalculator.Models
{
    public class DatePicker
    {
        [Display(Name = "Selecione uma data")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Por favor, selecione uma data.")]
        public DateTime ChosenDate { get; set; }
    }
}
