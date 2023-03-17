using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace team_status_undefined_backend.Migrations
{
    /// <inheritdoc />
    public partial class Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BarberImageLinks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "BarberImageLinks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "BarberImageLinks");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "BarberImageLinks");
        }
    }
}
