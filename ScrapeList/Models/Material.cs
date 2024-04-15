using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrapeList.Models
{
    public class Material
    {
        [Key]
        public int MaterialID { get; set; }

        public string MaterialName { get; set; }

        // [ForeignKey("Category")] // Original ForeignKey Attribute
        public int? CategoryID { get; set; }

        [ForeignKey("CategoryID")]  // New ForeignKey Attribute referring to the property name
        public virtual Category Category { get; set; }

        public virtual ICollection<Projection> Projections { get; set; } = new List<Projection>();
        public virtual ICollection<PriceRecord> PriceRecords { get; set; } = new List<PriceRecord>();
    }
}
