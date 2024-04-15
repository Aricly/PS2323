using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ScrapeList.Models
{
    public class PriceRecord
    {
        [Key]
        public int PriceRecordID { get; set; }

        [ForeignKey("Material")]
        public int MaterialID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public Material Material { get; set; }

        public virtual ICollection<SourceWebsitePriceRecord> SourceWebsitePriceRecords { get; set; } = new List<SourceWebsitePriceRecord>();
    }
}
