using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maqui.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RutaHojaVida = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RutaFoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.CheckConstraint("CK_TipoDocumento", "[TipoDocumento] IN ('DNI', 'RUC', 'CarnetExtranjeria')");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EsActivo",
                table: "Clientes",
                column: "EsActivo");

            migrationBuilder.CreateIndex(
                name: "UQ_NumeroDocumento",
                table: "Clientes",
                column: "NumeroDocumento",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
