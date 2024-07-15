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

        public decimal BtcCalc(DateTime userDate)
        {
            var sheet = ReadXls();

            foreach (var item in sheet)
            {
                if (item.PriceDate.ToShortDateString() == userDate.ToShortDateString())
                {
                    Console.WriteLine($"Preco: {item.Price} \nData: {item.PriceDate}\n");
                    return item.Price;
                }
            }

            throw new InvalidOperationException("Data não encontrada");

        }

        private static List<Bitcoin> ReadXls()
        {
            var response = new List<Bitcoin>();

            FileInfo existingFile = new FileInfo(@"\Models\BitcoinDatePriceBRL.xlsx");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;

                int rowCount = worksheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    var bitcoin = new Bitcoin();
                    bitcoin.Price = Convert.ToDecimal(worksheet.Cells[row, Col: 1].Value);
                    bitcoin.PriceDate = Convert.ToDateTime(worksheet.Cells[row, Col: 2].Value);

                    response.Add(bitcoin);
                }
            }

            return response;

        }
    }
}
