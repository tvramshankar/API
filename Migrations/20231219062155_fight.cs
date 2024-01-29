using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dev.Migrations
{
    /// <inheritdoc />
    public partial class fight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Defeats",
                table: "Charecter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fights",
                table: "Charecter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Victories",
                table: "Charecter",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Defeats",
                table: "Charecter");

            migrationBuilder.DropColumn(
                name: "Fights",
                table: "Charecter");

            migrationBuilder.DropColumn(
                name: "Victories",
                table: "Charecter");
        }
    }
}
