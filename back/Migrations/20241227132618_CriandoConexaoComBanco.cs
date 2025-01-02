using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiNotaFiscal.Migrations
{
    /// <inheritdoc />
    public partial class CriandoConexaoComBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotaFiscal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    NumeroNota = table.Column<int>(type: "int", nullable: false),
                    ChaveNota = table.Column<string>(type: "nvarchar(44)", maxLength: 44, nullable: false),
                    CnpjEmitente = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    NomeEmitente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ValorNota = table.Column<double>(type: "float", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaFiscal", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotaFiscal");
        }
    }
}
