using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrapeList.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDatabases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Categories_CategoryID",
                table: "Materials");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Materials",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Categories_CategoryID",
                table: "Materials",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Categories_CategoryID",
                table: "Materials");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Materials",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Categories_CategoryID",
                table: "Materials",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
