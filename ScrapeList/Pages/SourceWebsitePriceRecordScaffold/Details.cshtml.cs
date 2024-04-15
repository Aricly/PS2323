using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScrapeList.Data;
using ScrapeList.Models;

namespace ScrapeList.Pages.SourceWebsitePriceRecordScaffold
{
    public class DetailsModel : PageModel
    {
        private readonly ScrapeList.Data.ScrapeListContext _context;

        public DetailsModel(ScrapeList.Data.ScrapeListContext context)
        {
            _context = context;
        }

        public SourceWebsitePriceRecord SourceWebsitePriceRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? websiteId, int? priceRecordId)
        {
            if (websiteId == null || priceRecordId == null || _context.SourceWebsitePriceRecords == null)
            {
                return NotFound();
            }

            var sourcewebsitepricerecord = await _context.SourceWebsitePriceRecords
    .Include(s => s.SourceWebsite)
    .Include(p => p.PriceRecord)
    .FirstOrDefaultAsync(m => m.WebsiteID == websiteId && m.PriceRecordID == priceRecordId);

            if (sourcewebsitepricerecord == null)
            {
                return NotFound();
            }

            SourceWebsitePriceRecord = sourcewebsitepricerecord;
            ViewData["PriceRecordID"] = new SelectList(_context.PriceRecords, "PriceRecordID", "PriceRecordID", SourceWebsitePriceRecord.PriceRecordID);
            ViewData["WebsiteID"] = new SelectList(_context.SourceWebsites, "WebsiteID", "WebsiteID", SourceWebsitePriceRecord.WebsiteID);

            return Page();
        }
    }
}
