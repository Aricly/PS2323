using ScrapeList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScrapeList.Data;

public class DropdownService
{
    private readonly ScrapeListContext _context;

    public DropdownService(ScrapeListContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.Include(c => c.Materials).ToListAsync();
    }

    public async Task<List<Material>> GetMaterialsAsync()
    {
        return await _context.Materials.ToListAsync();
    }

    public async Task<IEnumerable<Material>> GetMaterialsByCategoryAsync(int categoryId)
    {
        return await _context.Materials
            .Where(m => m.CategoryID == categoryId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Material>> GetAllMaterialsAsync()
    {
        return await _context.Materials.ToListAsync();
    }


}
