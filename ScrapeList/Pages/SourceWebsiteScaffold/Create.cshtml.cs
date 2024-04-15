using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScrapeList.Data;
using ScrapeList.Models;

namespace ScrapeList.Pages.SourceWebsiteScaffold
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
            return Page();
        }

        [BindProperty]
        public SourceWebsite SourceWebsite { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.SourceWebsites == null || SourceWebsite == null)
            {
                return Page();
            }

            _context.SourceWebsites.Add(SourceWebsite);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
