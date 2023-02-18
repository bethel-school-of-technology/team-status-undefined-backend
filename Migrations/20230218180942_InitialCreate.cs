using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace team_status_undefined_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SignIns",
                columns: table => new
                {
                    SignInId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignIns", x => x.SignInId);
                });

            migrationBuilder.CreateTable(
                name: "Barber",
                columns: table => new
                {
                    BarberId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    LicenseNumber = table.Column<string>(type: "TEXT", maxLength: 7, nullable: false),
                    ProfilePic = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    AuthSignInId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barber", x => x.BarberId);
                    table.ForeignKey(
                        name: "FK_Barber_SignIns_AuthSignInId",
                        column: x => x.AuthSignInId,
                        principalTable: "SignIns",
                        principalColumn: "SignInId");
                });

            migrationBuilder.InsertData(
                table: "Barber",
                columns: new[] { "BarberId", "Address", "AuthSignInId", "City", "Description", "FirstName", "LastName", "LicenseNumber", "PhoneNumber", "ProfilePic", "State" },
                values: new object[] { 1, "123 A Road", null, "Dayton", "Hi everyone! I've got openings, who needs a trim?", "Austin", "Fogle", "C123456", "0123456789", "https://images.unsplash.com/photo-1517832606299-7ae9b720a186?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=772&q=80", "Ohio" });

            migrationBuilder.CreateIndex(
                name: "IX_Barber_AuthSignInId",
                table: "Barber",
                column: "AuthSignInId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Barber");

            migrationBuilder.DropTable(
                name: "SignIns");
        }
    }
}
