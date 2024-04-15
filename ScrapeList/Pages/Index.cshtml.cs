using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ScrapeList.Data;
using ScrapeList.Models;
using ScrapeList.Services;
using System.Reflection.Metadata;
using static System.Net.WebRequestMethods;

namespace ScrapeList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ScraperService _scraperService;
        private readonly WaybackService _waybackService;
        private readonly RecordCheckService _recordCheckService;
        private readonly PopulateDatabaseService _populateDatabaseService;
        private readonly PriceRecordUpdateService _priceRecordUpdateService;
        private ScrapeListContext _context;
        public IndexModel(ILogger<IndexModel> logger, ScraperService scraperService, WaybackService waybackService, RecordCheckService recordCheckService, PopulateDatabaseService populateDatabaseService,PriceRecordUpdateService priceRecordUpdateService ,ScrapeListContext context)
        {
            _logger = logger;
            _scraperService = scraperService;
            _waybackService = waybackService;
            _recordCheckService = recordCheckService;
            _priceRecordUpdateService = priceRecordUpdateService;
            _populateDatabaseService = populateDatabaseService;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var url = "https://fourseasonsnursery.com.au/blue-metal-20mm-20kg/";
            string xpathprice = "(//span[@class='price'])[1]/text()";
            //await PopulateDatabaseAsync(url, xpathprice, "Blue Metal 20mm 20kg", DateTime.Today);

            //url = "https://anlscape.com.au/landscaping/bagged-products/building-products/blue-metal-20mm-2okg";
            //xpathprice = "(//span[@class='prod-price']/text())[1]";
            //await PopulateDatabaseAsync(url, xpathprice, "Blue Metal 20mm 20kg", DateTime.Today);

            //url = "https://landscapesuppliesonline.com.au/blue-metal-20mm-20kg/";
            //xpathprice = "(//span[@data-product-price-with-tax]/text())[1]";
            //await PopulateDatabaseAsync(url, xpathprice, "Blue Metal 20mm 20kg", DateTime.Today);

            //url = "https://poppysgc.com.au/products/aggregate-blue-metal-20mm-20kg";
            //xpathprice = "//div[@id='product-price']//span[@class='price-item price-item--regular']/text()";
            //await PopulateDatabaseAsync(url, xpathprice, "Blue Metal 20mm 20kg", DateTime.Today);

            //url = "https://anlscape.com.au/landscaping/bagged-products/building-products/concrete-mix-20kg";
            //xpathprice = "//span[@class='prod-price']/text()[1]";
            //await PopulateDatabaseAsync(url, xpathprice, "Concrete Mix 20kg", DateTime.Today);

            //url = "https://priceproduce.com.au/products/easy-mix-concrete-20kg";
            //xpathprice = "//span[@id='ProductPrice-product-template']/text()[1]";
            //await PopulateDatabaseAsync(url, xpathprice, "Concrete Mix 20kg", DateTime.Today);


            //url = "https://strathalbynmitre10.com.au/shop/building-timber/fencing/fence-posts-rail/concrete/";
            //xpathprice = "//span[@class='woocommerce-Price-amount amount']/bdi/text()[1]";
            //await PopulateDatabaseAsync(url, xpathprice, "Concrete Mix 20kg", DateTime.Today);

            //url = "https://soilworx.com.au/shop/product/Concrete-Mix-1";
            //xpathprice = "//p[@class='pricing']/span[@class='price']/text()[1]";
            //await PopulateDatabaseAsync(url, xpathprice, "Concrete Mix 20kg", DateTime.Today);

            //url = "https://blacktownbuildingsupplies.com.au/product/besser-block-full-390-x-190-x-190-masonry-concrete-hollow-grey-block/";
            //xpathprice = "//p[@class='price']/span[@class='woocommerce-Price-amount amount']/bdi/text()";
            //await PopulateDatabaseAsync(url, xpathprice, "Concrete Block 390x190x190", DateTime.Today);


            //url = "https://blacktownbuildingsupplies.com.au/product/concrete-mesh-square-6-0-x-2-4m-sl62-reinforcement-steel-f62/";
            //xpathprice = "//p[@class='price']/span[@class='woocommerce-Price-amount amount']/bdi/text()";
            //await PopulateDatabaseAsync(url, xpathprice, "Mesh Square 5.8 x 2.4", DateTime.Today);

            //url = "https://metalandmachinery.com.au/product/reinforcing-mesh-reo-sl62-6-0mm-wire-x-200mm-square/";
            //xpathprice = "//p[@class='price']/span[@class='woocommerce-Price-amount amount']/span/following-sibling::text()";
            //await PopulateDatabaseAsync(url, xpathprice, "Mesh Square 5.8 x 2.4", DateTime.Today);

            //url = "https://onlinetilestore.com.au/gloss-white-wall-150x150/";
            //xpathprice = "//div[@class='price-wrapper']/p[@class='price product-page-price price-on-sale']/ins/span[@class='woocommerce-Price-amount amount']/bdi";
            //await PopulateDatabaseAsync(url, xpathprice, "White Tile 150x150", DateTime.Today);

            //url = "https://simonsseconds.com.au/products/myst-400x400x40mm-concrete-pavers-charcoal-1st-quality";
            //xpathprice = "//span[@class='price-item price-item--regular']/text()";
            //await PopulateDatabaseAsync(url, xpathprice, "400 x 400 x 40mm Charcoal Paver", DateTime.Today);

            //url = "https://tileauctions.com.au/products/diy-concrete-stone-paver-400x400x40";
            //xpathprice = "//*[@id='productPrice']/text()";
            //await PopulateDatabaseAsync(url, xpathprice, "400 x 400 x 40mm Charcoal Paver", DateTime.Today;

            //url = "https://bunnings.com.au/arc-20mm-x-6m-reinforcing-deformed-bar_p1060294";
            //xpathprice = "//p[contains(@data-locator, 'product-price')]";
            //await PopulateDatabaseAsync(url, xpathprice, "20mm x 6m Reinforcing bar", DateTime.Today);

            //url = "https://atexsupplies.com.au/product/n20-galvanised-6m-bar/";
            //xpathprice = "//span[@class='woocommerce-Price-amount amount']/bdi/text()";
            //await PopulateDatabaseAsync(url, xpathprice, "20mm x 6m Reinforcing bar", DateTime.Today);

            //url = "https://blacktownbuildingsupplies.com.au/product/concrete-steel-reo-bar-12mm-x-6000mm-reinforcing-deformed-rebar/";
            //xpathprice = "//p[@class='price']/span[@class='woocommerce-Price-amount amount']/bdi/text()";
            //await PopulateDatabaseAsync(url, xpathprice, "12mm x 6m Reinforcing bar", DateTime.Today);

            //url = "https://allcongroup.com.au/product/800mm-x-4m-formatube/";
            //xpathprice = "//span[@class='woocommerce-Price-amount amount']/bdi/text()";
            //await PopulateDatabaseAsync(url, xpathprice, "800mm x 4m Form Tube", DateTime.Today);

            //url = "https://trade-line.com.au/product/pier-plastic-form-tube-800mm-x-4m/";
            //xpathprice = "//span[@class='woocommerce-Price-amount amount']/text()";
            //await PopulateDatabaseAsync(url, xpathprice, "800mm x 4m Form Tube", DateTime.Today);

            //url = "https://www.networkbuilding.com.au/shop/gyprock-re-10mm-1200-x-2400/";
            //xpathprice = "//span[@class='woocommerce-Price-amount amount']/bdi/text()";
            //await PopulateDatabaseAsync(url, xpathprice, "Gyprock RE 10mm 1200 x 2400", DateTime.Today);

            //url = "https://wallceiling.com.au/products/gyprock-csr-2400-x-1200-x-10mm-2-88sqm-plasterboard-re";
            //xpathprice = "//*[@id='price-template--20074854482195__main']/div/div/div[1]";
            //await PopulateDatabaseAsync(url, xpathprice, "Gyprock RE 10mm 1200 x 2400", DateTime.Today);

            //url = "https://blacktownbuildingsupplies.com.au/product/washed-sydney-sand-20kg-australian-sands/";
            //xpathprice = "//p[@class='price']/span[@class='woocommerce-Price-amount amount']/bdi/text()";
            //await PopulateDatabaseAsync(url, xpathprice, "Sydney Sand 20kg", DateTime.Today);

            //url = "https://anlscape.com.au/landscaping/bagged-products/building-products/sydney-sand-20kg";
            //xpathprice = "(//span[@class='prod-price']/text())[1]";
            //await PopulateDatabaseAsync(url, xpathprice, "Sydney Sand 20kg", DateTime.Today);

            //var snapshots = _waybackService.Check_wayback(url, "Concrete Block 390x190x190");
            //var resultSnapshots = await snapshots;
            //foreach (var s in resultSnapshots)
            //{
            //    var link = s.Split(", ");
            //    var date = link[1];

            //    date = link[1].Substring(0, 4);
            //    var year = Convert.ToInt32(date);

            //    date = link[1].Substring(4, 2);
            //    var month = Convert.ToInt32(date);

            //    date = link[1].Substring(6, 2);
            //    var day = Convert.ToInt32(date);

            //    var date_snapshot = new DateTime(year, month, day);
            //    await PopulateDatabaseAsync(link[0], xpathprice, "Concrete Block 390x190x190", date_snapshot);
            //}
            await _priceRecordUpdateService.UpdateMaterialPrices();
            return Page();
        }


        //Function to add single record into the database from the given url, xpath for the Price, material, and date
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
        //Remove www. from websites to normalise
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