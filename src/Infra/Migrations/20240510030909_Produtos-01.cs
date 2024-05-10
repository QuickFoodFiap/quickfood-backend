using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class Produtos01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Produto",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(40)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", precision: 2, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Produto",
                columns: new[] { "Id", "Categoria", "Descricao", "Nome", "Preco" },
                values: new object[] { new Guid("2d034eae-b870-4cd6-a6e8-092c305931a3"), "Lanche", "Hamburguer, pão, queijo, alface", "X-Salada", 21m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto",
                schema: "dbo");
        }
    }
}
