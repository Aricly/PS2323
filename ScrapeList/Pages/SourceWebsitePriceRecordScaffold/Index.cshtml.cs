using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScrapeList.Data;
using ScrapeList.Models;

namespace ScrapeList.Pages.SourceWebsitePriceRecordScaffold
{
    public class IndexModel : PageModel
    {
        private readonly ScrapeList.Data.ScrapeListContext _context;

        public IndexModel(ScrapeList.Data.ScrapeListContext context)
        {
            _context = context;
        }

        public IList<SourceWebsitePriceRecord> SourceWebsitePriceRecord { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.SourceWebsitePriceRecords != null)
            {
                SourceWebsitePriceRecord = await _context.SourceWebsitePriceRecords
                .Include(s => s.PriceRecord)
                .Include(s => s.SourceWebsite).ToListAsync();
            }
        }
    }
}
