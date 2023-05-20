using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpravaPenezDeti.Migrations
{
    /// <inheritdoc />
    public partial class SqlServerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jmeno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CasVytoreni = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ucty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jmeno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zustatek = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CasVytoreni = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiteUcet",
                columns: table => new
                {
                    DiteIdd = table.Column<int>(type: "int", nullable: false),
                    UcetIdd = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiteUcet", x => new { x.DiteIdd, x.UcetIdd });
                    table.ForeignKey(
                        name: "FK_DiteUcet_Deti_UcetIdd",
                        column: x => x.UcetIdd,
                        principalTable: "Deti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiteUcet_Ucty_DiteIdd",
                        column: x => x.DiteIdd,
                        principalTable: "Ucty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pohyby",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Castka = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Detaily = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UcetId = table.Column<int>(type: "int", nullable: false),
                    CasVytoreni = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pohyby", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pohyby_Ucty_UcetId",
                        column: x => x.UcetId,
                        principalTable: "Ucty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiteUcet_UcetIdd",
                table: "DiteUcet",
                column: "UcetIdd");

            migrationBuilder.CreateIndex(
                name: "IX_Pohyby_UcetId",
                table: "Pohyby",
                column: "UcetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiteUcet");

            migrationBuilder.DropTable(
                name: "Pohyby");

            migrationBuilder.DropTable(
                name: "Deti");

            migrationBuilder.DropTable(
                name: "Ucty");
        }
    }
}
