using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EtudeManyToMany.API.Migrations
{
    public partial class initMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdministrateurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdministrateurId);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    UtilisateurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.UtilisateurId);
                });

            migrationBuilder.CreateTable(
                name: "Conducteurs",
                columns: table => new
                {
                    ConducteurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conducteurs", x => x.ConducteurId);
                    table.ForeignKey(
                        name: "FK_Conducteurs_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passagers",
                columns: table => new
                {
                    PassagerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passagers", x => x.PassagerId);
                    table.ForeignKey(
                        name: "FK_Passagers_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trajets",
                columns: table => new
                {
                    TrajetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LieuDepart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LieuArrivee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConducteurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trajets", x => x.TrajetId);
                    table.ForeignKey(
                        name: "FK_Trajets_Conducteurs_ConducteurId",
                        column: x => x.ConducteurId,
                        principalTable: "Conducteurs",
                        principalColumn: "ConducteurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrajetId = table.Column<int>(type: "int", nullable: false),
                    PassagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Passagers_PassagerId",
                        column: x => x.PassagerId,
                        principalTable: "Passagers",
                        principalColumn: "PassagerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Trajets_TrajetId",
                        column: x => x.TrajetId,
                        principalTable: "Trajets",
                        principalColumn: "TrajetId");
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdministrateurId", "Nom", "Password" },
                values: new object[] { 1, "Admin", "PAss62!" });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "UtilisateurId", "Email", "Nom", "Phone" },
                values: new object[,]
                {
                    { 1, "jean.dupont@gmail.com", "Dupont", "0607080910" },
                    { 2, "pierre.dujardin@gmail.com", "Dujardin", "0607880910" },
                    { 3, "john.doe@gmail.com", "Doe", "0609090910" }
                });

            migrationBuilder.InsertData(
                table: "Conducteurs",
                columns: new[] { "ConducteurId", "UtilisateurId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Passagers",
                columns: new[] { "PassagerId", "UtilisateurId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Trajets",
                columns: new[] { "TrajetId", "ConducteurId", "LieuArrivee", "LieuDepart" },
                values: new object[] { 1, 2, "Marseille", "Paris" });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "PassagerId", "TrajetId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "PassagerId", "TrajetId" },
                values: new object[] { 2, 2, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Conducteurs_UtilisateurId",
                table: "Conducteurs",
                column: "UtilisateurId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passagers_UtilisateurId",
                table: "Passagers",
                column: "UtilisateurId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PassagerId",
                table: "Reservations",
                column: "PassagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TrajetId",
                table: "Reservations",
                column: "TrajetId");

            migrationBuilder.CreateIndex(
                name: "IX_Trajets_ConducteurId",
                table: "Trajets",
                column: "ConducteurId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Passagers");

            migrationBuilder.DropTable(
                name: "Trajets");

            migrationBuilder.DropTable(
                name: "Conducteurs");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
