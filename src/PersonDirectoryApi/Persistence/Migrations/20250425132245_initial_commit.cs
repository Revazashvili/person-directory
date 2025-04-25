using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PersonDirectoryApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial_commit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_person_relations_people_person_id",
                table: "person_relations");

            migrationBuilder.DropForeignKey(
                name: "fk_person_relations_people_related_person_id",
                table: "person_relations");

            migrationBuilder.DropForeignKey(
                name: "fk_phone_numbers_persons_person_id",
                table: "phone_numbers");

            migrationBuilder.DropIndex(
                name: "ix_phone_numbers_person_id",
                table: "phone_numbers");

            migrationBuilder.DropIndex(
                name: "ix_person_relations_person_id_related_person_id_type",
                table: "person_relations");

            migrationBuilder.DropIndex(
                name: "ix_person_relations_related_person_id",
                table: "person_relations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_people",
                table: "people");

            migrationBuilder.DropIndex(
                name: "ix_people_personal_number",
                table: "people");

            migrationBuilder.DropColumn(
                name: "person_id",
                table: "phone_numbers");

            migrationBuilder.DropColumn(
                name: "person_id",
                table: "person_relations");

            migrationBuilder.DropColumn(
                name: "related_person_id",
                table: "person_relations");

            migrationBuilder.DropColumn(
                name: "id",
                table: "people");

            migrationBuilder.AddColumn<string>(
                name: "person_personal_number",
                table: "phone_numbers",
                type: "character varying(11)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "person_personal_number",
                table: "person_relations",
                type: "character varying(11)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "related_person_personal_number",
                table: "person_relations",
                type: "character varying(11)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_people",
                table: "people",
                column: "personal_number");

            migrationBuilder.CreateIndex(
                name: "ix_phone_numbers_person_personal_number",
                table: "phone_numbers",
                column: "person_personal_number");

            migrationBuilder.CreateIndex(
                name: "ix_person_relations_person_personal_number_related_person_pers",
                table: "person_relations",
                columns: new[] { "person_personal_number", "related_person_personal_number", "type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_person_relations_related_person_personal_number",
                table: "person_relations",
                column: "related_person_personal_number");

            migrationBuilder.AddForeignKey(
                name: "fk_person_relations_people_person_personal_number",
                table: "person_relations",
                column: "person_personal_number",
                principalTable: "people",
                principalColumn: "personal_number",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_relations_people_related_person_personal_number",
                table: "person_relations",
                column: "related_person_personal_number",
                principalTable: "people",
                principalColumn: "personal_number",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_phone_numbers_persons_person_personal_number",
                table: "phone_numbers",
                column: "person_personal_number",
                principalTable: "people",
                principalColumn: "personal_number",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_person_relations_people_person_personal_number",
                table: "person_relations");

            migrationBuilder.DropForeignKey(
                name: "fk_person_relations_people_related_person_personal_number",
                table: "person_relations");

            migrationBuilder.DropForeignKey(
                name: "fk_phone_numbers_persons_person_personal_number",
                table: "phone_numbers");

            migrationBuilder.DropIndex(
                name: "ix_phone_numbers_person_personal_number",
                table: "phone_numbers");

            migrationBuilder.DropIndex(
                name: "ix_person_relations_person_personal_number_related_person_pers",
                table: "person_relations");

            migrationBuilder.DropIndex(
                name: "ix_person_relations_related_person_personal_number",
                table: "person_relations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_people",
                table: "people");

            migrationBuilder.DropColumn(
                name: "person_personal_number",
                table: "phone_numbers");

            migrationBuilder.DropColumn(
                name: "person_personal_number",
                table: "person_relations");

            migrationBuilder.DropColumn(
                name: "related_person_personal_number",
                table: "person_relations");

            migrationBuilder.AddColumn<int>(
                name: "person_id",
                table: "phone_numbers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "person_id",
                table: "person_relations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "related_person_id",
                table: "person_relations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "people",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_people",
                table: "people",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_phone_numbers_person_id",
                table: "phone_numbers",
                column: "person_id");

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
                name: "ix_people_personal_number",
                table: "people",
                column: "personal_number",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_person_relations_people_person_id",
                table: "person_relations",
                column: "person_id",
                principalTable: "people",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_relations_people_related_person_id",
                table: "person_relations",
                column: "related_person_id",
                principalTable: "people",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_phone_numbers_persons_person_id",
                table: "phone_numbers",
                column: "person_id",
                principalTable: "people",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
