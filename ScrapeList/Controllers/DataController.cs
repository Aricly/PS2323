using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrapeList.Data;
using ScrapeList.Models;

namespace ScrapeList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ScrapeListContext _context;

        public DataController(ScrapeListContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("priceRecords")]
        public async Task<IActionResult> GetPriceRecords(int? materialID)
        {
            if (!materialID.HasValue)
            {
                // Handle the case where materialID is not provided
                return BadRequest("Material ID is required.");
            }

            var priceRecords = await GetPriceRecordsByMaterialAsync(materialID.Value);

            if (priceRecords == null || !priceRecords.Any())
            {
                // Handle the case where no price records were found
                return NotFound();
            }

            return Ok(priceRecords);
        }

        private async Task<IEnumerable<PriceRecord>> GetPriceRecordsByMaterialAsync(int materialID)
        {
            return await _context.PriceRecords
                .Where(pr => pr.MaterialID == materialID).OrderBy(pr => pr.Date)
                .ToListAsync();
        }

    }
}
