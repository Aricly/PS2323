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
    public class IndexModel : PageModel
    {
        private readonly ScrapeList.Data.ScrapeListContext _context;

        public IndexModel(ScrapeList.Data.ScrapeListContext context)
        {
            _context = context;
        }

        public IList<Material> Material { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Materials != null)
            {
                Material = await _context.Materials
                .Include(m => m.Category).ToListAsync();
            }
        }
    }
}
