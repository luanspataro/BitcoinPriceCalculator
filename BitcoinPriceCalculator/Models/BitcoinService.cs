using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        FileInfo existingFile = new FileInfo(@"X:\repositorios\Luan\BitcoinPriceCalculator\BitcoinPriceCalculator\Models\BitcoinDatePriceBRL.xlsx");

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (ExcelPackage package = new ExcelPackage(existingFile))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.End.Row;

            for (int row = 2; row <= rowCount; row++)
            {
                var bitcoin = new Bitcoin();

                try
                {
                    
                    var cellValuePrice = worksheet.Cells[row, 2].Value;
                    Console.WriteLine($"Linha {row}, Coluna 2 (Preço): '{cellValuePrice}'");

                    if (cellValuePrice != null)
                    {
                        string priceString = cellValuePrice.ToString();
                        if (decimal.TryParse(priceString, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal price))
                        {
                            bitcoin.Price = price;
                        }
                        else
                        {
                            throw new InvalidOperationException($"Erro ao converter valor da célula para decimal na linha {row}: '{cellValuePrice}'");
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"Célula vazia na linha {row}, coluna 2");
                    }

                    
                    var cellValueDate = worksheet.Cells[row, 1].Value;
                    Console.WriteLine($"Linha {row}, Coluna 1 (Data): '{cellValueDate}'");

                    if (cellValueDate != null && DateTime.TryParse(cellValueDate.ToString(), out DateTime date))
                    {
                        bitcoin.PriceDate = date;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Erro ao converter valor da célula para DateTime na linha {row}: '{cellValueDate}'");
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Erro na linha {row}: {ex.Message}", ex);
                }

                response.Add(bitcoin);
            }
        }

        return response;
    }



}
}
