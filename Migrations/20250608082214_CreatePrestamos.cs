using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaMechiApi.Migrations
{
    /// <inheritdoc />
    public partial class CreatePrestamos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "prestamos",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    manga_id = table.Column<int>(type: "int", nullable: false),
                    fecha_prestamo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_devolucion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    prestatario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prestamos", x => x.id);
                    table.ForeignKey(
                        name: "FK_prestamos_Mangas_manga_id",
                        column: x => x.manga_id,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_prestamos_manga_id",
                schema: "dbo",
                table: "prestamos",
                column: "manga_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prestamos",
                schema: "dbo");
        }
    }
}
