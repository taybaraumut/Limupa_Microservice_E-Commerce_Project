using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Limupa.UI.Controllers
{
    public class ProductApplePhoneListController : Controller
    {
        [HttpGet]
        public IActionResult Index(List<string> productName, List<string> productPrice, List<string> productModel, List<string> productInternalMemorySize)
        {
            List<(decimal, decimal)> parsedValues = new List<(decimal, decimal)>();

            foreach (string value in productPrice)
            {
                string pattern = @"(\d+)-(\d+)"; // Örnek bir desen, decimal sayılar bekliyoruz

                MatchCollection matches = Regex.Matches(value, pattern);

                // Her bir value için tüm eşleşmeleri işliyoruz
                foreach (Match match in matches)
                {
                    string firstNumber = match.Groups[1].Value;
                    string secondNumber = match.Groups[2].Value;

                    decimal firstDecimal = decimal.Parse(firstNumber);
                    decimal secondDecimal = decimal.Parse(secondNumber);

                    parsedValues.Add((firstDecimal, secondDecimal));
                }
            }

            List<decimal> values = new List<decimal>();

            foreach (var tuple in parsedValues)
            {
                values.Add(tuple.Item1); // İlk decimal değeri values listesine ekliyoruz
                values.Add(tuple.Item2); // İkinci decimal değeri values listesine ekliyoruz
            }

            TempData["productName"] = productName;
            TempData["productPrice"] = values;
            TempData["productModel"] = productModel;
            TempData["productInternalMemorySize"] = productInternalMemorySize;

            return View(
                new
                {
                    productName = TempData["productName"],
                    productPrice = TempData["productPrice"],
                    productModel = TempData["productModel"],
                    productInternalMemorySize = TempData["productInternalMemorySize"],
                });
        }
    }   
}
