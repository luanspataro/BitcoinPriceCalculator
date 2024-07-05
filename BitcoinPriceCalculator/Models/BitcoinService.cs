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
        private static List<Bitcoin> ReadXls()
        {
            var response = new List<Bitcoin>();

            FileInfo existingFile = new FileInfo(fileName:"\Models\BitcoinDatePriceBRL");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[PositionID: 0];
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
