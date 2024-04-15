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

namespace ScrapeList.Pages.PriceRecordScaffold
{
    public class EditModel : PageModel
    {
        private readonly ScrapeList.Data.ScrapeListContext _context;

        public EditModel(ScrapeList.Data.ScrapeListContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PriceRecord PriceRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PriceRecords == null)
            {
                return NotFound();
            }

            var pricerecord =  await _context.PriceRecords.FirstOrDefaultAsync(m => m.PriceRecordID == id);
            if (pricerecord == null)
            {
                return NotFound();
            }
            PriceRecord = pricerecord;
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

            _context.Attach(PriceRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceRecordExists(PriceRecord.PriceRecordID))
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

        private bool PriceRecordExists(int id)
        {
          return (_context.PriceRecords?.Any(e => e.PriceRecordID == id)).GetValueOrDefault();
        }
    }
}
