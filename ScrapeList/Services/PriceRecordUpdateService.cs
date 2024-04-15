using Microsoft.EntityFrameworkCore;
using ScrapeList.Data;

namespace ScrapeList.Services
{
    public class PriceRecordUpdateService
    {
        private readonly ScrapeListContext _context;
        private readonly PopulateDatabaseService _populateDatabaseService;

        public PriceRecordUpdateService(ScrapeListContext context, PopulateDatabaseService populateDatabaseService)
        {
            _context = context;
            _populateDatabaseService = populateDatabaseService;
        }

        public async Task UpdateMaterialPrices()
        {
            // Get the date a month ago
            var oneMonthAgo = DateTime.Now.AddMonths(-1);

            // Fetch all PriceRecords
            var allPriceRecords = await _context.PriceRecords.Include(pr => pr.SourceWebsitePriceRecords).ToListAsync();

            foreach (var priceRecord in allPriceRecords)
            {
                // Check if there's a PriceRecord for this Material in the last month
                var recentRecordExists = await _context.PriceRecords
                    .Where(pr => pr.MaterialID == priceRecord.MaterialID)
                    .Where(pr => pr.Date >= oneMonthAgo && pr.Date <= DateTime.Now)
                    .AnyAsync();

                if (!recentRecordExists)
                {
                    // If no recent record, get the latest PriceRecord
                    var latestPriceRecord = await _context.PriceRecords
                        .Where(pr => pr.MaterialID == priceRecord.MaterialID)
                        .OrderByDescending(pr => pr.Date)
                        .FirstOrDefaultAsync();

                    if (latestPriceRecord != null)
                    {
                        var materialName = latestPriceRecord.Material.MaterialName;

                        // Loop through related SourceWebsitePriceRecords
                        foreach (var swpr in latestPriceRecord.SourceWebsitePriceRecords)
                        {
                            var sourceWebsite = await _context.SourceWebsites
                                .Where(sw => sw.WebsiteID == swpr.WebsiteID)
                                .FirstOrDefaultAsync();

                            if (sourceWebsite != null)
                            {
                                await _populateDatabaseService.PopulateDatabaseAsync(sourceWebsite.URL, sourceWebsite.xPathPrice, materialName, DateTime.Now);
                            }
                        }
                    }
                }
            }
        }

    }
}
