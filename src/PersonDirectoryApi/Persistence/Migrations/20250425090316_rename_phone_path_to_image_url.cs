using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonDirectoryApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class rename_phone_path_to_image_url : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo_path",
                table: "people");

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "people",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_url",
                table: "people");

            migrationBuilder.AddColumn<string>(
                name: "photo_path",
                table: "people",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
