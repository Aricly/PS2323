using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScrapeList.Data;
using ScrapeList.Models;

namespace ScrapeList.Pages.PriceRecordScaffold
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
        public PriceRecord PriceRecord { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (_context.PriceRecords == null || PriceRecord == null)
            {
                return Page();
            }

            _context.PriceRecords.Add(PriceRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
