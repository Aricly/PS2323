using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScrapeList.Data;
using ScrapeList.Models;

namespace ScrapeList.Pages.ProjectionScaffold
{
    public class DetailsModel : PageModel
    {
        private readonly ScrapeList.Data.ScrapeListContext _context;

        public DetailsModel(ScrapeList.Data.ScrapeListContext context)
        {
            _context = context;
        }

      public Projection Projection { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Projections == null)
            {
                return NotFound();
            }

            var projection = await _context.Projections.Include(m => m.Material).FirstOrDefaultAsync(m => m.ProjectionID == id);
            if (projection == null)
            {
                return NotFound();
            }
            else 
            {
                Projection = projection;
            }
            return Page();
        }
    }
}
