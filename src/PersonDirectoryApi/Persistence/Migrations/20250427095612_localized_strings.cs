using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonDirectoryApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class localized_strings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_localisations",
                table: "localisations");

            migrationBuilder.RenameTable(
                name: "localisations",
                newName: "localizations");

            migrationBuilder.RenameIndex(
                name: "ix_localisations_key_culture",
                table: "localizations",
                newName: "ix_localizations_key_culture");

            migrationBuilder.AddPrimaryKey(
                name: "pk_localizations",
                table: "localizations",
                columns: new[] { "key", "culture" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_localizations",
                table: "localizations");

            migrationBuilder.RenameTable(
                name: "localizations",
                newName: "localisations");

            migrationBuilder.RenameIndex(
                name: "ix_localizations_key_culture",
                table: "localisations",
                newName: "ix_localisations_key_culture");

            migrationBuilder.AddPrimaryKey(
                name: "pk_localisations",
                table: "localisations",
                columns: new[] { "key", "culture" });
        }
    }
}
