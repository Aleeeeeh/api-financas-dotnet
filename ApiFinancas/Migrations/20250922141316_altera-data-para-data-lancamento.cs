using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFinancas.Migrations
{
    /// <inheritdoc />
    public partial class alteradataparadatalancamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                schema: "Financas",
                table: "Lancamentos",
                newName: "DataLancamento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataLancamento",
                schema: "Financas",
                table: "Lancamentos",
                newName: "Data");
        }
    }
}
