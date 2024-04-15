using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScrapeList.Models;



namespace ScrapeList.Data
{
    public class ScrapeListContext : DbContext
    {
        public ScrapeListContext(DbContextOptions<ScrapeListContext> options)
            : base(options)
        {
        }

        public DbSet<Projection> Projections { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PriceRecord> PriceRecords { get; set; }
        public DbSet<SourceWebsite> SourceWebsites { get; set; }
        public DbSet<SourceWebsitePriceRecord> SourceWebsitePriceRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SourceWebsitePriceRecord>()
                .HasKey(swpr => new { swpr.WebsiteID, swpr.PriceRecordID });

            modelBuilder.Entity<SourceWebsitePriceRecord>()
                .HasOne(swpr => swpr.PriceRecord)
                .WithMany(pr => pr.SourceWebsitePriceRecords)
                .HasForeignKey(swpr => swpr.PriceRecordID);

            modelBuilder.Entity<SourceWebsitePriceRecord>()
                .HasOne(swpr => swpr.SourceWebsite)
                .WithMany(sw => sw.SourceWebsitePriceRecords)
                .HasForeignKey(swpr => swpr.WebsiteID);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.Category)
                .WithMany(c => c.Materials)
                .HasForeignKey(m => m.CategoryID)
                .IsRequired(false);
                }
    }
}
