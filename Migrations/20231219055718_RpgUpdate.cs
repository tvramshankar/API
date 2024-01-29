using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dev.Migrations
{
    /// <inheritdoc />
    public partial class RpgUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Charecter");

            migrationBuilder.DropColumn(
                name: "SchoolName",
                table: "Charecter");

            migrationBuilder.AddColumn<int>(
                name: "Defence",
                table: "Charecter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitPoints",
                table: "Charecter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intelligence",
                table: "Charecter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Strength",
                table: "Charecter",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Defence",
                table: "Charecter");

            migrationBuilder.DropColumn(
                name: "HitPoints",
                table: "Charecter");

            migrationBuilder.DropColumn(
                name: "Intelligence",
                table: "Charecter");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "Charecter");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Charecter",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SchoolName",
                table: "Charecter",
                type: "longtext",
                nullable: false);
        }
    }
}
