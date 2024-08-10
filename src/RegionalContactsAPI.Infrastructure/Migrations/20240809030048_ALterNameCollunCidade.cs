using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegionalContactsAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ALterNameCollunCidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Cidades",
                newName: "NomeCidade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeCidade",
                table: "Cidades",
                newName: "Nome");
        }
    }
}
