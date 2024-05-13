using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class Cliente02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.InsertData(
                schema: "dbo",
                table: "Cliente",
                columns: new[] { "Id", "Ativo", "Cpf", "Email", "Nome" },
                values: new object[,]
                {
                    { new Guid("efee2d79-ce89-479a-9667-04f57f9e2e5e"), true, "08062759016", "joao@gmail.com", "João" },
                    { new Guid("fdff63d2-127f-49c5-854a-e47cae8cedb9"), true, "05827307084", "maria@gmail.com", "Maria" }
                });

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("efee2d79-ce89-479a-9667-04f57f9e2e5e"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("fdff63d2-127f-49c5-854a-e47cae8cedb9"));
        }
    }
}
