using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PersonDirectoryApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class rename_relation_to_relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "person_relations");

            migrationBuilder.CreateTable(
                name: "person_relationships",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: false),
                    person_personal_number = table.Column<string>(type: "character varying(11)", nullable: false),
                    related_person_personal_number = table.Column<string>(type: "character varying(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_relationships", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_relationships_people_person_personal_number",
                        column: x => x.person_personal_number,
                        principalTable: "people",
                        principalColumn: "personal_number",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_person_relationships_people_related_person_personal_number",
                        column: x => x.related_person_personal_number,
                        principalTable: "people",
                        principalColumn: "personal_number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_person_relationships_person_personal_number_related_person_",
                table: "person_relationships",
                columns: new[] { "person_personal_number", "related_person_personal_number", "type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_person_relationships_related_person_personal_number",
                table: "person_relationships",
                column: "related_person_personal_number");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "person_relationships");

            migrationBuilder.CreateTable(
                name: "person_relations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    person_personal_number = table.Column<string>(type: "character varying(11)", nullable: false),
                    related_person_personal_number = table.Column<string>(type: "character varying(11)", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_relations", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_relations_people_person_personal_number",
                        column: x => x.person_personal_number,
                        principalTable: "people",
                        principalColumn: "personal_number",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_person_relations_people_related_person_personal_number",
                        column: x => x.related_person_personal_number,
                        principalTable: "people",
                        principalColumn: "personal_number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_person_relations_person_personal_number_related_person_pers",
                table: "person_relations",
                columns: new[] { "person_personal_number", "related_person_personal_number", "type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_person_relations_related_person_personal_number",
                table: "person_relations",
                column: "related_person_personal_number");
        }
    }
}
