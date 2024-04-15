using System.ComponentModel.DataAnnotations;

namespace ScrapeList.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
	}
}
