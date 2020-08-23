using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CasaDoCupom.Data.Migrations
{
    public partial class CriandoBancoDeDados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    EmpresaId = table.Column<Guid>(nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    Validado = table.Column<bool>(nullable: false),
                    Codigo = table.Column<string>(maxLength: 20, nullable: false),
                    Reservado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cupons_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cupons_EmpresaId",
                table: "Cupons",
                column: "EmpresaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cupons");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
