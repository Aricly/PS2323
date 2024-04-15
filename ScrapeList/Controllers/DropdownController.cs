using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrapeList.Models;
using System.Threading.Tasks;

using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class DropdownController : ControllerBase
{
    private readonly DropdownService _dropdownService;

    public DropdownController(DropdownService dropdownService)
    {
        _dropdownService = dropdownService;
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _dropdownService.GetCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("materials")]
    public async Task<IActionResult> GetMaterials(int? categoryID)
    {
        if (categoryID.HasValue)
        {
            var materials = await _dropdownService.GetMaterialsByCategoryAsync(categoryID.Value);
            return Ok(materials);
        }

        // If no categoryID is provided, return all materials (optional)
        var allMaterials = await _dropdownService.GetAllMaterialsAsync();
        return Ok(allMaterials);
    }

}
