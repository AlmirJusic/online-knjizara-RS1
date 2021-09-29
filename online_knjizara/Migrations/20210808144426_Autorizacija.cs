using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace online_knjizara.Migrations
{
    public partial class Autorizacija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnik_Uloga_Uloga_ID",
                table: "Korisnik");

            migrationBuilder.DropTable(
                name: "Uloga");

            migrationBuilder.DropIndex(
                name: "IX_Korisnik_Uloga_ID",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Uloga_ID",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Korisnik");

            migrationBuilder.CreateTable(
                name: "KorisnickiNalozi",
                columns: table => new
                {
                    KorisnickiNalog_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    Lozinka = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnickiNalozi", x => x.KorisnickiNalog_ID);
                });

            migrationBuilder.CreateTable(
                name: "AutorizacijskiToken",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrijednost = table.Column<string>(nullable: true),
                    KorisnickiNalozi_ID = table.Column<int>(nullable: false),
                    VrijemeEvidentiranja = table.Column<DateTime>(nullable: false),
                    IpAdresa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorizacijskiToken", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AutorizacijskiToken_KorisnickiNalozi_KorisnickiNalozi_ID",
                        column: x => x.KorisnickiNalozi_ID,
                        principalTable: "KorisnickiNalozi",
                        principalColumn: "KorisnickiNalog_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CitaocKorisnik",
                columns: table => new
                {
                    CitaocKorisnik_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Korisnik_ID = table.Column<int>(nullable: false),
                    KorisnickiNalog_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitaocKorisnik", x => x.CitaocKorisnik_ID);
                    table.ForeignKey(
                        name: "FK_CitaocKorisnik_KorisnickiNalozi_KorisnickiNalog_ID",
                        column: x => x.KorisnickiNalog_ID,
                        principalTable: "KorisnickiNalozi",
                        principalColumn: "KorisnickiNalog_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CitaocKorisnik_Korisnik_Korisnik_ID",
                        column: x => x.Korisnik_ID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zaposlenici",
                columns: table => new
                {
                    Zaposlenik_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrucnaSprema = table.Column<string>(nullable: true),
                    Korisnik_ID = table.Column<int>(nullable: false),
                    KorisnickiNalog_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposlenici", x => x.Zaposlenik_ID);
                    table.ForeignKey(
                        name: "FK_Zaposlenici_KorisnickiNalozi_KorisnickiNalog_ID",
                        column: x => x.KorisnickiNalog_ID,
                        principalTable: "KorisnickiNalozi",
                        principalColumn: "KorisnickiNalog_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zaposlenici_Korisnik_Korisnik_ID",
                        column: x => x.Korisnik_ID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutorizacijskiToken_KorisnickiNalozi_ID",
                table: "AutorizacijskiToken",
                column: "KorisnickiNalozi_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CitaocKorisnik_KorisnickiNalog_ID",
                table: "CitaocKorisnik",
                column: "KorisnickiNalog_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CitaocKorisnik_Korisnik_ID",
                table: "CitaocKorisnik",
                column: "Korisnik_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenici_KorisnickiNalog_ID",
                table: "Zaposlenici",
                column: "KorisnickiNalog_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenici_Korisnik_ID",
                table: "Zaposlenici",
                column: "Korisnik_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutorizacijskiToken");

            migrationBuilder.DropTable(
                name: "CitaocKorisnik");

            migrationBuilder.DropTable(
                name: "Zaposlenici");

            migrationBuilder.DropTable(
                name: "KorisnickiNalozi");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Uloga_ID",
                table: "Korisnik",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Uloga",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivUloge = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloga", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_Uloga_ID",
                table: "Korisnik",
                column: "Uloga_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnik_Uloga_Uloga_ID",
                table: "Korisnik",
                column: "Uloga_ID",
                principalTable: "Uloga",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
