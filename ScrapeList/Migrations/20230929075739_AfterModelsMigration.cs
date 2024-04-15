using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrapeList.Migrations
{
    /// <inheritdoc />
    public partial class AfterModelsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialModel");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "SourceWebsites",
                columns: table => new
                {
                    WebsiteID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    URL = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceWebsites", x => x.WebsiteID);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaterialName = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialID);
                    table.ForeignKey(
                        name: "FK_Materials_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceRecords",
                columns: table => new
                {
                    PriceRecordID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaterialID = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRecords", x => x.PriceRecordID);
                    table.ForeignKey(
                        name: "FK_PriceRecords_Materials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materials",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projections",
                columns: table => new
                {
                    ProjectionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaterialID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProjectedPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projections", x => x.ProjectionID);
                    table.ForeignKey(
                        name: "FK_Projections_Materials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materials",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SourceWebsitePriceRecords",
                columns: table => new
                {
                    WebsiteID = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceRecordID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceWebsitePriceRecords", x => new { x.WebsiteID, x.PriceRecordID });
                    table.ForeignKey(
                        name: "FK_SourceWebsitePriceRecords_PriceRecords_PriceRecordID",
                        column: x => x.PriceRecordID,
                        principalTable: "PriceRecords",
                        principalColumn: "PriceRecordID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SourceWebsitePriceRecords_SourceWebsites_WebsiteID",
                        column: x => x.WebsiteID,
                        principalTable: "SourceWebsites",
                        principalColumn: "WebsiteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_CategoryID",
                table: "Materials",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceRecords_MaterialID",
                table: "PriceRecords",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_Projections_MaterialID",
                table: "Projections",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_SourceWebsitePriceRecords_PriceRecordID",
                table: "SourceWebsitePriceRecords",
                column: "PriceRecordID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projections");

            migrationBuilder.DropTable(
                name: "SourceWebsitePriceRecords");

            migrationBuilder.DropTable(
                name: "PriceRecords");

            migrationBuilder.DropTable(
                name: "SourceWebsites");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.CreateTable(
                name: "MaterialModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialModel", x => x.Id);
                });
        }
    }
}
