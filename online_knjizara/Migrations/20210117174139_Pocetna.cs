using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace online_knjizara.Migrations
{
    public partial class Pocetna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzava",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Odlomak",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sadrzaj = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odlomak", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Stanje",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipStanja = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanje", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Uloga",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivUloge = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloga", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Zanr",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zanr", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Grad",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Drzava_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grad", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Grad_Drzava_Drzava_ID",
                        column: x => x.Drzava_ID,
                        principalTable: "Drzava",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Grad_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Autor_Grad_Grad_ID",
                        column: x => x.Grad_ID,
                        principalTable: "Grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Izdavac",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Grad_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izdavac", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Izdavac_Grad_Grad_ID",
                        column: x => x.Grad_ID,
                        principalTable: "Grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Grad_ID = table.Column<int>(nullable: false),
                    Uloga_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Korisnik_Grad_Grad_ID",
                        column: x => x.Grad_ID,
                        principalTable: "Grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Korisnik_Uloga_Uloga_ID",
                        column: x => x.Uloga_ID,
                        principalTable: "Uloga",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skladiste",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    Kolicina = table.Column<int>(nullable: false),
                    Grad_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skladiste", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Skladiste_Grad_Grad_ID",
                        column: x => x.Grad_ID,
                        principalTable: "Grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Knjiga",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Autor_ID = table.Column<int>(nullable: false),
                    Izdavac_ID = table.Column<int>(nullable: false),
                    Skladiste_ID = table.Column<int>(nullable: false),
                    Zanr_ID = table.Column<int>(nullable: false),
                    Stanje_ID = table.Column<int>(nullable: false),
                    Odlomak_ID = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    Cijena = table.Column<float>(nullable: false),
                    DatumIzdavanja = table.Column<DateTime>(nullable: false),
                    Slika = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knjiga", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Knjiga_Autor_Autor_ID",
                        column: x => x.Autor_ID,
                        principalTable: "Autor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Knjiga_Izdavac_Izdavac_ID",
                        column: x => x.Izdavac_ID,
                        principalTable: "Izdavac",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Knjiga_Odlomak_Odlomak_ID",
                        column: x => x.Odlomak_ID,
                        principalTable: "Odlomak",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Knjiga_Skladiste_Skladiste_ID",
                        column: x => x.Skladiste_ID,
                        principalTable: "Skladiste",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Knjiga_Stanje_Stanje_ID",
                        column: x => x.Stanje_ID,
                        principalTable: "Stanje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Knjiga_Zanr_Zanr_ID",
                        column: x => x.Zanr_ID,
                        principalTable: "Zanr",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KomentarOcjena",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Korisnik_ID = table.Column<int>(nullable: false),
                    Knjiga_ID = table.Column<int>(nullable: false),
                    Komentar = table.Column<string>(nullable: true),
                    Ocjena = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomentarOcjena", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KomentarOcjena_Knjiga_Knjiga_ID",
                        column: x => x.Knjiga_ID,
                        principalTable: "Knjiga",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KomentarOcjena_Korisnik_Korisnik_ID",
                        column: x => x.Korisnik_ID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Narudzba",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Knjiga_ID = table.Column<int>(nullable: false),
                    Korisnik_ID = table.Column<int>(nullable: false),
                    Kolicina = table.Column<int>(nullable: false),
                    Cijena = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzba", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Narudzba_Knjiga_Knjiga_ID",
                        column: x => x.Knjiga_ID,
                        principalTable: "Knjiga",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Narudzba_Korisnik_Korisnik_ID",
                        column: x => x.Korisnik_ID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autor_Grad_ID",
                table: "Autor",
                column: "Grad_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Grad_Drzava_ID",
                table: "Grad",
                column: "Drzava_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Izdavac_Grad_ID",
                table: "Izdavac",
                column: "Grad_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Knjiga_Autor_ID",
                table: "Knjiga",
                column: "Autor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Knjiga_Izdavac_ID",
                table: "Knjiga",
                column: "Izdavac_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Knjiga_Odlomak_ID",
                table: "Knjiga",
                column: "Odlomak_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Knjiga_Skladiste_ID",
                table: "Knjiga",
                column: "Skladiste_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Knjiga_Stanje_ID",
                table: "Knjiga",
                column: "Stanje_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Knjiga_Zanr_ID",
                table: "Knjiga",
                column: "Zanr_ID");

            migrationBuilder.CreateIndex(
                name: "IX_KomentarOcjena_Knjiga_ID",
                table: "KomentarOcjena",
                column: "Knjiga_ID");

            migrationBuilder.CreateIndex(
                name: "IX_KomentarOcjena_Korisnik_ID",
                table: "KomentarOcjena",
                column: "Korisnik_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_Grad_ID",
                table: "Korisnik",
                column: "Grad_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_Uloga_ID",
                table: "Korisnik",
                column: "Uloga_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_Knjiga_ID",
                table: "Narudzba",
                column: "Knjiga_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_Korisnik_ID",
                table: "Narudzba",
                column: "Korisnik_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Skladiste_Grad_ID",
                table: "Skladiste",
                column: "Grad_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KomentarOcjena");

            migrationBuilder.DropTable(
                name: "Narudzba");

            migrationBuilder.DropTable(
                name: "Knjiga");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Izdavac");

            migrationBuilder.DropTable(
                name: "Odlomak");

            migrationBuilder.DropTable(
                name: "Skladiste");

            migrationBuilder.DropTable(
                name: "Stanje");

            migrationBuilder.DropTable(
                name: "Zanr");

            migrationBuilder.DropTable(
                name: "Uloga");

            migrationBuilder.DropTable(
                name: "Grad");

            migrationBuilder.DropTable(
                name: "Drzava");
        }
    }
}
