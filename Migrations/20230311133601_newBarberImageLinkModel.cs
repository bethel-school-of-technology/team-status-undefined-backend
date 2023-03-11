using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace team_status_undefined_backend.Migrations
{
    /// <inheritdoc />
    public partial class newBarberImageLinkModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarberImageLinks",
                columns: table => new
                {
                    BarberImageLinkId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BarberId = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarberImageLinks", x => x.BarberImageLinkId);
                    table.ForeignKey(
                        name: "FK_BarberImageLinks_Barber_BarberId",
                        column: x => x.BarberId,
                        principalTable: "Barber",
                        principalColumn: "BarberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarberImageLinks_BarberId",
                table: "BarberImageLinks",
                column: "BarberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarberImageLinks");
        }
    }
}
