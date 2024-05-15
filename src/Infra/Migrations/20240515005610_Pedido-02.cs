using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class Pedido02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.AddColumn<int>(
                name: "PedidoStatus",
                schema: "dbo",
                table: "Pedido",
                type: "int",
                nullable: false,
                defaultValue: 0);

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropColumn(
                name: "PedidoStatus",
                schema: "dbo",
                table: "Pedido");
    }
}
