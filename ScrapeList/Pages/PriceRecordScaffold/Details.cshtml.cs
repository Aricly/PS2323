using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScrapeList.Data;
using ScrapeList.Models;

namespace ScrapeList.Pages.PriceRecordScaffold
{
    public class DetailsModel : PageModel
    {
        private readonly ScrapeList.Data.ScrapeListContext _context;

        public DetailsModel(ScrapeList.Data.ScrapeListContext context)
        {
            _context = context;
        }

      public PriceRecord PriceRecord { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PriceRecords == null)
            {
                return NotFound();
            }

            var pricerecord = await _context.PriceRecords.Include(m => m.Material).FirstOrDefaultAsync(m => m.PriceRecordID == id);
            if (pricerecord == null)
            {
                return NotFound();
            }
            else 
            {
                PriceRecord = pricerecord;
            }
            return Page();
        }
    }
}
