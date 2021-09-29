using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace online_knjizara.Migrations
{
    public partial class ADDDatumRodjenjaZaKorisnika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatumRodjenja",
                table: "Korisnik",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumRodjenja",
                table: "Korisnik");
        }
    }
}
