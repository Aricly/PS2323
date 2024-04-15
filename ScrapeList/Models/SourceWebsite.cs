using System.ComponentModel.DataAnnotations;

namespace ScrapeList.Models
{
    public class SourceWebsite
    {
        [Key]
        public int WebsiteID { get; set; }

        public string URL { get; set; }
        public string xPathPrice { get; set; }

        public virtual ICollection<SourceWebsitePriceRecord> SourceWebsitePriceRecords { get; set; } = new List<SourceWebsitePriceRecord>();
    }

}
