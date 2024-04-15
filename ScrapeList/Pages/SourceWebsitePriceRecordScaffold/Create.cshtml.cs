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
    public class CreateModel : PageModel
    {
        private readonly ScrapeList.Data.ScrapeListContext _context;

        public CreateModel(ScrapeList.Data.ScrapeListContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PriceRecordID"] = new SelectList(_context.PriceRecords.Include(pr => pr.Material), "PriceRecordID", "PriceRecordID");
            ViewData["WebsiteID"] = new SelectList(_context.SourceWebsites, "WebsiteID", "Description");
            return Page();
        }

        [BindProperty]
        public SourceWebsitePriceRecord SourceWebsitePriceRecord { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (_context.SourceWebsitePriceRecords == null || SourceWebsitePriceRecord == null)
            {
                return Page();
            }

            _context.SourceWebsitePriceRecords.Add(SourceWebsitePriceRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
