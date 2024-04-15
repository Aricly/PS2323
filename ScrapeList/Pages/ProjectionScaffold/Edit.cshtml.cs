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

namespace ScrapeList.Pages.ProjectionScaffold
{
    public class EditModel : PageModel
    {
        private readonly ScrapeList.Data.ScrapeListContext _context;

        public EditModel(ScrapeList.Data.ScrapeListContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Projection Projection { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Projections == null)
            {
                return NotFound();
            }

            var projection =  await _context.Projections.FirstOrDefaultAsync(m => m.ProjectionID == id);
            if (projection == null)
            {
                return NotFound();
            }
            Projection = projection;
           ViewData["MaterialID"] = new SelectList(_context.Materials, "MaterialID", "MaterialName");
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

            _context.Attach(Projection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectionExists(Projection.ProjectionID))
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

        private bool ProjectionExists(int id)
        {
          return (_context.Projections?.Any(e => e.ProjectionID == id)).GetValueOrDefault();
        }
    }
}
