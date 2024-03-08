using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EtudeManyToMany.API.Migrations
{
    public partial class initMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    UtilisateurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomVehicule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false)
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
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombrePlace = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<float>(type: "real", nullable: false),
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
                name: "Commentaires",
                columns: table => new
                {
                    CommentaireId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<int>(type: "int", nullable: false),
                    Avis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassagerId = table.Column<int>(type: "int", nullable: false),
                    ConducteurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaires", x => x.CommentaireId);
                    table.ForeignKey(
                        name: "FK_Commentaires_Conducteurs_ConducteurId",
                        column: x => x.ConducteurId,
                        principalTable: "Conducteurs",
                        principalColumn: "ConducteurId");
                    table.ForeignKey(
                        name: "FK_Commentaires_Passagers_PassagerId",
                        column: x => x.PassagerId,
                        principalTable: "Passagers",
                        principalColumn: "PassagerId");
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
                table: "Utilisateurs",
                columns: new[] { "UtilisateurId", "Description", "Email", "Nom", "NomVehicule", "Password", "Phone", "Photo", "isAdmin" },
                values: new object[,]
                {
                    { 1, null, "jean.dupont@gmail.com", "Dupont", null, "1", "0607080910", null, false },
                    { 2, null, "pierre.dujardin@gmail.com", "Dujardin", null, "2", "0607880910", null, false },
                    { 3, null, "john.doe@gmail.com", "Doe", null, "3", "0609090910", null, false },
                    { 4, null, "Admin@gmail.com", "Admin", null, "Admin", "0609090910", null, true }
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
                columns: new[] { "TrajetId", "ConducteurId", "DateTime", "LieuArrivee", "LieuDepart", "NombrePlace", "Prix" },
                values: new object[] { 1, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marseille", "Paris", 0, 0f });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "PassagerId", "TrajetId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "PassagerId", "TrajetId" },
                values: new object[] { 2, 2, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Commentaires_ConducteurId",
                table: "Commentaires",
                column: "ConducteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaires_PassagerId",
                table: "Commentaires",
                column: "PassagerId");

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
                name: "Commentaires");

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
