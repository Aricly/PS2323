using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScrapeList.Data;
using ScrapeList.Models;

namespace ScrapeList.Pages.MaterialScaffold
{
    public class DeleteModel : PageModel
    {
        private readonly ScrapeList.Data.ScrapeListContext _context;

        public DeleteModel(ScrapeList.Data.ScrapeListContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Material Material { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Materials == null)
            {
                return NotFound();
            }

            var material = await _context.Materials
       .Include(m => m.Category)  // This line ensures the Category entity is loaded along with Material.
       .FirstOrDefaultAsync(m => m.MaterialID == id);

            if (material == null)
            {
                return NotFound();
            }
            else 
            {
                Material = material;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Materials == null)
            {
                return NotFound();
            }
            var material = await _context.Materials.FindAsync(id);

            if (material != null)
            {
                Material = material;
                _context.Materials.Remove(Material);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
