using System;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ScrapeList.Data;
using ScrapeList.Models;

namespace ScrapeList.Services

{
    public class ScraperService
    {
        private readonly ScrapeListContext _context;

        public ScraperService(ScrapeListContext context)
        {
            _context = context;
        }
        static string Remove_dollar(string s)
        {
            // Check if the string is null or empty
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            return s.Replace("$", "").Replace("\"", "").Replace(";", "");
        }

        public async Task<PriceRecord> Scrape(string url, string xpathPrice, string materialName, DateTime date)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--incognito --headless=new");

            using (IWebDriver driver = new ChromeDriver(chromeOptions))
            {
                driver.Navigate().GoToUrl(url);
                var pageSource = driver.PageSource;

                var doc = new HtmlDocument();
                doc.LoadHtml(pageSource);

                var productNodes = doc.DocumentNode.SelectNodes("/html");
                decimal price = 0m;

                if (productNodes != null)
                {
                    foreach (var productNode in productNodes)
                    {
                        var priceNode = productNode.SelectSingleNode(xpathPrice);
                        if (priceNode == null) { Console.WriteLine("-------------PRICENODE NULL-------------"); return null; }
                        var priceText = priceNode.InnerText.Trim();
                        priceText = Remove_dollar(priceText);
                        bool success = decimal.TryParse(priceText, out price);

                        if (success)
                        {
                            Material existingMaterial = new Material();
                            string pattern = $"%{materialName}%";
                            try
                            {
                                existingMaterial = await _context.Materials
                                    .FirstOrDefaultAsync(m => EF.Functions.Like(m.MaterialName, pattern));  // Updated line
                            }
                            catch (ObjectDisposedException ex)
                            {
                                // Inspect exception and _context
                                throw;
                            }


                            if (existingMaterial != null)
                            {
                                return new PriceRecord
                                {
                                    MaterialID = existingMaterial.MaterialID,
                                    Date = date,
                                    Price = price,
                                    Material = existingMaterial
                                };
                            }
                            else
                            {
                                Console.WriteLine("-------------Unsuccessful material db query-------------");
                            }
                        }
                        else { Console.WriteLine("-------------PRODUCT NULL-------------"); }
                    }
                }
                else { Console.WriteLine("-------------NODES NULL-------------"); }
            }

            return null;
        }
    }
}
