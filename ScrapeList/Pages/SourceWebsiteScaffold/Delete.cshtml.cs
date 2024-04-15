using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScrapeList.Data;
using ScrapeList.Models;

namespace ScrapeList.Pages.SourceWebsiteScaffold
{
    public class DeleteModel : PageModel
    {
        private readonly ScrapeList.Data.ScrapeListContext _context;

        public DeleteModel(ScrapeList.Data.ScrapeListContext context)
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

            var sourcewebsite = await _context.SourceWebsites.FirstOrDefaultAsync(m => m.WebsiteID == id);

            if (sourcewebsite == null)
            {
                return NotFound();
            }
            else 
            {
                SourceWebsite = sourcewebsite;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.SourceWebsites == null)
            {
                return NotFound();
            }
            var sourcewebsite = await _context.SourceWebsites.FindAsync(id);

            if (sourcewebsite != null)
            {
                SourceWebsite = sourcewebsite;
                _context.SourceWebsites.Remove(SourceWebsite);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
