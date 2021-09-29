using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace online_knjizara.Migrations
{
    public partial class ADDObavijesti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Obavijesti",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Sadrzaj = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    Slika = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijesti", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Obavijesti");
        }
    }
}
