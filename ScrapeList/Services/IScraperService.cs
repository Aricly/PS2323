using ScrapeList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourApp.Services
{
    public interface IScraperService
    {
        Task<List<PriceRecord>> ScrapeWebsiteAsync(string url, string xpathName, string xpathPrice, string materialName);
    }
}
