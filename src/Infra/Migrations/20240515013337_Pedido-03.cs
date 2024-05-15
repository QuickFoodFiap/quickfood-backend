using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class Pedido03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorUnitario",
                schema: "dbo",
                table: "PedidoItem",
                type: "decimal(18,2)",
                precision: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                schema: "dbo",
                table: "Pedido",
                type: "decimal(18,2)",
                precision: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorUnitario",
                schema: "dbo",
                table: "PedidoItem");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                schema: "dbo",
                table: "Pedido");
        }
    }
}
