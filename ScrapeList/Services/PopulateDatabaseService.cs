using Microsoft.EntityFrameworkCore;
using ScrapeList.Data;
using ScrapeList.Models;
using ScrapeList.Services;
using SQLitePCL;
using YourApp.Services;

namespace ScrapeList.Services
{
    public class PopulateDatabaseService
    {
        private readonly ScrapeListContext _context;
        private readonly ScraperService _scraperService;
        private readonly WaybackService _waybackService;
        public PopulateDatabaseService(ScrapeListContext context, WaybackService waybackService, ScraperService scraperService)
        {
            _context = context;
            _waybackService = waybackService;
            _scraperService = scraperService;
        }

        public async Task PopulateDatabaseAsync(string url, string xpathPrice, string materialName, DateTime date)
        {
            //Assume the ScrapeWebsiteAsync method is now returning a single PriceRecord as per your instruction.
            url = NormalizeWwwInUrl(url);
            //Call a function to Scrape the website and add the scraped information into a PriceRecord variable
            PriceRecord priceRecord = await _scraperService.Scrape(url, xpathPrice, materialName, date);
            if (priceRecord == null)
            {
                throw new Exception("There is no value returned from a Scraper service");
            }
            var existingWebsite = await _context.SourceWebsites
.Where(w => url.Contains(w.URL))
.FirstOrDefaultAsync();
            if (existingWebsite == null)
            {
                throw new Exception("There is no existing website");
            }
            //Check if there is already a material existing in the past month, if there is, do not add to the database
            var oneMonthAgo = date.AddMonths(-1);
            var existingRecord = await _context.PriceRecords
        .Where(pr => pr.MaterialID == priceRecord.MaterialID &&
                     pr.SourceWebsitePriceRecords.Any(swpr => swpr.WebsiteID == existingWebsite.WebsiteID))
        .Where(pr => pr.Date >= oneMonthAgo && pr.Date <= date)
        .FirstOrDefaultAsync();
            if (existingRecord != null)
            {
                Console.WriteLine("There already exists a PriceRecord within the past month");
                return;
            }
            if (existingRecord == null)
            {
                await _context.PriceRecords.AddAsync(priceRecord);
                await _context.SaveChangesAsync();
            }
            //Link the SourceWebsite table with PriceRecord table through SourceWebsitePriceRecord table
            var existingSWPR = await _context.SourceWebsitePriceRecords.Where(m => m.WebsiteID == existingWebsite.WebsiteID && m.PriceRecordID == priceRecord.PriceRecordID).FirstOrDefaultAsync();
            if (existingSWPR == null)
            {
                var sourceWebsitePriceRecord = new SourceWebsitePriceRecord
                {
                    WebsiteID = existingWebsite.WebsiteID,
                    PriceRecordID = priceRecord.PriceRecordID,
                    SourceWebsite = existingWebsite,
                    PriceRecord = priceRecord
                };

                await _context.SourceWebsitePriceRecords.AddAsync(sourceWebsitePriceRecord);
                await _context.SaveChangesAsync();
            }
        }

        public async Task PopulateWaybackAsync(string url, string xpathPrice, string materialName)
        {
            var snapshots = await _waybackService.Check_wayback(url, materialName);

            foreach (var s in snapshots)
            {
                var link = s.Split(", ");
                var dateString = link[1];

                var year = int.Parse(dateString.Substring(0, 4));
                var month = int.Parse(dateString.Substring(4, 2));
                var day = int.Parse(dateString.Substring(6, 2));

                var dateSnapshot = new DateTime(year, month, day);

                await PopulateDatabaseAsync(link[0], xpathPrice, materialName, dateSnapshot);
            }
        }

        public static string NormalizeWwwInUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            // Check if the URL contains "www." after "https://" or "http://"
            if (url.Contains("https://www."))
                return url.Replace("https://www.", "https://");
            else if (url.Contains("http://www."))
                return url.Replace("http://www.", "http://");
            else
                return url;
        }

    }
}
