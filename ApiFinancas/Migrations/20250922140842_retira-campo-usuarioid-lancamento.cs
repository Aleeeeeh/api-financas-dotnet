using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFinancas.Migrations
{
    /// <inheritdoc />
    public partial class retiracampousuarioidlancamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                schema: "Financas",
                table: "Lancamentos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                schema: "Financas",
                table: "Lancamentos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
