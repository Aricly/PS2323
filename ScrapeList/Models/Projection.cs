using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScrapeList.Models

{
    public class Projection
    {
        [Key]
        public int ProjectionID { get; set; }

        [ForeignKey("Material")]
        public int MaterialID { get; set; }
        [DataType(DataType.Date)]
        public DateTime ProjectedDate { get; set; }
        [DataType(DataType.Currency)]
        public decimal ProjectedPrice { get; set; }

        public Material Material { get; set; }
    }
}
