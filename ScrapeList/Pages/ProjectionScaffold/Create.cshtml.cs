using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScrapeList.Data;
using ScrapeList.Models;

namespace ScrapeList.Pages.ProjectionScaffold
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
        ViewData["MaterialID"] = new SelectList(_context.Materials, "MaterialID", "MaterialName");
            return Page();
        }

        [BindProperty]
        public Projection Projection { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (_context.Projections == null || Projection == null)
            {
                return Page();
            }

            _context.Projections.Add(Projection);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
