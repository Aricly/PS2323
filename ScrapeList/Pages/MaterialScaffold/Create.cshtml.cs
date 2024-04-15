using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScrapeList.Data;
using ScrapeList.Models;

namespace ScrapeList.Pages.MaterialScaffold
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
        ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Material Material { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (Material == null || _context.Materials == null)
            {
                var errors = ModelState
                .SelectMany(x => x.Value.Errors, (x, e) => x.Key + ": " + e.ErrorMessage)
                .ToList();
                Console.WriteLine(string.Join(", ", errors));
                ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
                return Page();
            }

            _context.Materials.Add(Material);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
