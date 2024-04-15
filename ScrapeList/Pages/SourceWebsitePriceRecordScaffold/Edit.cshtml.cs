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
    public class EditModel : PageModel
    {
        private readonly ScrapeList.Data.ScrapeListContext _context;

        public EditModel(ScrapeList.Data.ScrapeListContext context)
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
           ViewData["PriceRecordID"] = new SelectList(_context.PriceRecords, "PriceRecordID", "PriceRecordID");
           ViewData["WebsiteID"] = new SelectList(_context.SourceWebsites, "WebsiteID", "Description");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Attach(SourceWebsitePriceRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SourceWebsitePriceRecordExists(SourceWebsitePriceRecord.WebsiteID, SourceWebsitePriceRecord.PriceRecordID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SourceWebsitePriceRecordExists(int websiteId, int priceRecordId)
        {
            return (_context.SourceWebsitePriceRecords?.Any(e => e.WebsiteID == websiteId && e.PriceRecordID == priceRecordId)).GetValueOrDefault();
        }

    }
}
