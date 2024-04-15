using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScrapeList.Data;
using ScrapeList.Models;

namespace ScrapeList.Pages.SourceWebsitePriceRecordScaffold
{
    public class DeleteModel : PageModel
    {
        private readonly ScrapeList.Data.ScrapeListContext _context;

        public DeleteModel(ScrapeList.Data.ScrapeListContext context)
        {
            _context = context;
        }

        [BindProperty]
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? websiteId, int? priceRecordId)
        {
            if (websiteId == null || priceRecordId == null)
            {
                return NotFound();
            }

            var sourcewebsitepricerecord = await _context.SourceWebsitePriceRecords
                .FirstOrDefaultAsync(m => m.WebsiteID == websiteId && m.PriceRecordID == priceRecordId);

            if (sourcewebsitepricerecord != null)
            {
                _context.SourceWebsitePriceRecords.Remove(sourcewebsitepricerecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
