using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrapeList.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedxPathPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "SourceWebsites",
                newName: "xPathPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "xPathPrice",
                table: "SourceWebsites",
                newName: "Description");
        }
    }
}
