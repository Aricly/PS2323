using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrapeList.Migrations
{
    /// <inheritdoc />
    public partial class MaterialModelChange1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "MaterialModel");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "MaterialModel");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "MaterialModel");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "MaterialModel",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "MaterialModel",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "MaterialModel",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "MaterialModel",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "MaterialModel",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
