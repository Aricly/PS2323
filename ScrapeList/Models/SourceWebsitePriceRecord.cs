using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ScrapeList.Models
{
    public class SourceWebsitePriceRecord
    {
        [Key, Column(Order = 0), ForeignKey("SourceWebsite")]
        public int WebsiteID { get; set; }

        [Key, Column(Order = 1), ForeignKey("PriceRecord")]
        public int PriceRecordID { get; set; }

        public SourceWebsite SourceWebsite { get; set; }
        public PriceRecord PriceRecord { get; set; }
    }
}
