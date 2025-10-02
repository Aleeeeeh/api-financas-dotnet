using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFinancas.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoestaexcluido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstaExcluido",
                schema: "Financas",
                table: "Usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EstaExcluido",
                schema: "Financas",
                table: "Lancamentos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaExcluido",
                schema: "Financas",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EstaExcluido",
                schema: "Financas",
                table: "Lancamentos");
        }
    }
}
