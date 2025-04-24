using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PersonDirectoryApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "people",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    personal_number = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    birth_date = table.Column<DateTime>(type: "date", nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    photo_path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_people", x => x.id);
                    table.ForeignKey(
                        name: "fk_people_cities_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "person_relations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                    related_person_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_relations", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_relations_people_person_id",
                        column: x => x.person_id,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_person_relations_people_related_person_id",
                        column: x => x.related_person_id,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "phone_numbers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: false),
                    number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_phone_numbers", x => x.id);
                    table.ForeignKey(
                        name: "fk_phone_numbers_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cities_name",
                table: "cities",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_people_birth_date",
                table: "people",
                column: "birth_date");

            migrationBuilder.CreateIndex(
                name: "ix_people_city_id",
                table: "people",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_people_first_name_last_name",
                table: "people",
                columns: new[] { "first_name", "last_name" });

            migrationBuilder.CreateIndex(
                name: "ix_people_personal_number",
                table: "people",
                column: "personal_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_person_relations_person_id_related_person_id_type",
                table: "person_relations",
                columns: new[] { "person_id", "related_person_id", "type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_person_relations_related_person_id",
                table: "person_relations",
                column: "related_person_id");

            migrationBuilder.CreateIndex(
                name: "ix_phone_numbers_person_id",
                table: "phone_numbers",
                column: "person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "person_relations");

            migrationBuilder.DropTable(
                name: "phone_numbers");

            migrationBuilder.DropTable(
                name: "people");

            migrationBuilder.DropTable(
                name: "cities");
        }
    }
}
