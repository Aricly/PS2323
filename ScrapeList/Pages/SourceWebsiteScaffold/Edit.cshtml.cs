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

namespace ScrapeList.Pages.SourceWebsiteScaffold
{
    public class EditModel : PageModel
    {
        private readonly ScrapeList.Data.ScrapeListContext _context;

        public EditModel(ScrapeList.Data.ScrapeListContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SourceWebsite SourceWebsite { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SourceWebsites == null)
            {
                return NotFound();
            }

            var sourcewebsite =  await _context.SourceWebsites.FirstOrDefaultAsync(m => m.WebsiteID == id);
            if (sourcewebsite == null)
            {
                return NotFound();
            }
            SourceWebsite = sourcewebsite;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SourceWebsite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SourceWebsiteExists(SourceWebsite.WebsiteID))
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

        private bool SourceWebsiteExists(int id)
        {
          return (_context.SourceWebsites?.Any(e => e.WebsiteID == id)).GetValueOrDefault();
        }
    }
}
