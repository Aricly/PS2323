using Microsoft.EntityFrameworkCore;
using ScrapeList.Data;

namespace ScrapeList.Services
{
    public class RecordCheckService
    {
        private readonly ScrapeListContext _context;

        public RecordCheckService(ScrapeListContext context)
        {
            _context = context;
        }

        public bool DoesPriceRecordExist(string materialName, string url, DateTime date)
        {
            using (_context)
            {
                return _context.PriceRecords
                    .Any(pr => pr.Material.MaterialName == materialName && _context.SourceWebsites.Any(sw => sw.URL == url) && pr.Date == date);

            }
        }


    }
}
