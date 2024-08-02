using BitcoinPriceCalculator.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinPriceCalculator.Models
{
    public class BitcoinService
    {
        public Tuple<decimal, decimal, decimal> ProfitCalc(decimal price, DateTime priceDate, decimal actualPrice)
        {
            var bitcoin = new Bitcoin();
            var model = new PriceCalculatorModel();

            bitcoin.Amount = model.PurchaseValue / price;
            bitcoin.Percentage = (price / actualPrice - 1) * 100;
            bitcoin.Profit = actualPrice - price;

            /*qtdecompradabtc;
            porcentagemlucro;
            valorfinal;
            
            colocar outra cor quando for < 0*/

            var profitData = Tuple.Create(bitcoin.Amount, bitcoin.Percentage, bitcoin.Profit);
            return profitData;
        }

        public Tuple<decimal, DateTime> BtcCalc(DateTime userDate)
        {
            var sheet = ReadXls();

            foreach (var item in sheet)
            {
                if (item.PriceDate.ToShortDateString() == userDate.ToShortDateString())
                {
                    var btcData = Tuple.Create(item.Price, item.PriceDate);
                    return btcData;
                }
            }
            throw new InvalidOperationException("Data não encontrada.");
        }

        private static List<Bitcoin> ReadXls()
        {
            var response = new List<Bitcoin>();

            FileInfo existingFile = new FileInfo(@"X:\repositorios\Luan\BitcoinPriceCalculator\BitcoinPriceCalculator\Models\BitcoinDatePriceBRL.xlsx");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    var bitcoin = new Bitcoin();
                    bitcoin.Price = Convert.ToDecimal(worksheet.Cells[row, 2].Value);
                    bitcoin.PriceDate = Convert.ToDateTime(worksheet.Cells[row, 1].Value);

                    response.Add(bitcoin);
                }
            }
            return response;
        }
    }
}
