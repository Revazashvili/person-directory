﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PersonDirectoryApi.Persistence.Repositories;

#nullable disable

namespace PersonDirectoryApi.Persistence.Migrations
{
    [DbContext(typeof(PersonContext))]
    [Migration("20250425132245_initial_commit")]
    partial class initial_commit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PersonDirectoryApi.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_cities");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_cities_name");

                    b.ToTable("cities", (string)null);
                });

            modelBuilder.Entity("PersonDirectoryApi.Entities.Person", b =>
                {
                    b.Property<string>("PersonalNumber")
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)")
                        .HasColumnName("personal_number");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<int>("CityId")
                        .HasColumnType("integer")
                        .HasColumnName("city_id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("image_url");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.HasKey("PersonalNumber")
                        .HasName("pk_people");

                    b.HasIndex("BirthDate")
                        .HasDatabaseName("ix_people_birth_date");

                    b.HasIndex("CityId")
                        .HasDatabaseName("ix_people_city_id");

                    b.HasIndex("FirstName", "LastName")
                        .HasDatabaseName("ix_people_first_name_last_name");

                    b.ToTable("people", (string)null);
                });

            modelBuilder.Entity("PersonDirectoryApi.Entities.PersonRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PersonPersonalNumber")
                        .IsRequired()
                        .HasColumnType("character varying(11)")
                        .HasColumnName("person_personal_number");

                    b.Property<string>("RelatedPersonPersonalNumber")
                        .IsRequired()
                        .HasColumnType("character varying(11)")
                        .HasColumnName("related_person_personal_number");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_person_relations");

                    b.HasIndex("RelatedPersonPersonalNumber")
                        .HasDatabaseName("ix_person_relations_related_person_personal_number");

                    b.HasIndex("PersonPersonalNumber", "RelatedPersonPersonalNumber", "Type")
                        .IsUnique()
                        .HasDatabaseName("ix_person_relations_person_personal_number_related_person_pers");

                    b.ToTable("person_relations", (string)null);
                });

            modelBuilder.Entity("PersonDirectoryApi.Entities.PhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("number");

                    b.Property<string>("PersonPersonalNumber")
                        .IsRequired()
                        .HasColumnType("character varying(11)")
                        .HasColumnName("person_personal_number");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_phone_numbers");

                    b.HasIndex("PersonPersonalNumber")
                        .HasDatabaseName("ix_phone_numbers_person_personal_number");

                    b.ToTable("phone_numbers", (string)null);
                });

            modelBuilder.Entity("PersonDirectoryApi.Entities.Person", b =>
                {
                    b.HasOne("PersonDirectoryApi.Entities.City", "City")
                        .WithMany("Residents")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_people_cities_city_id");

                    b.Navigation("City");
                });

            modelBuilder.Entity("PersonDirectoryApi.Entities.PersonRelation", b =>
                {
                    b.HasOne("PersonDirectoryApi.Entities.Person", "Person")
                        .WithMany("Relations")
                        .HasForeignKey("PersonPersonalNumber")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_person_relations_people_person_personal_number");

                    b.HasOne("PersonDirectoryApi.Entities.Person", "RelatedPerson")
                        .WithMany()
                        .HasForeignKey("RelatedPersonPersonalNumber")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_person_relations_people_related_person_personal_number");

                    b.Navigation("Person");

                    b.Navigation("RelatedPerson");
                });

            modelBuilder.Entity("PersonDirectoryApi.Entities.PhoneNumber", b =>
                {
                    b.HasOne("PersonDirectoryApi.Entities.Person", "Person")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("PersonPersonalNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_phone_numbers_persons_person_personal_number");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("PersonDirectoryApi.Entities.City", b =>
                {
                    b.Navigation("Residents");
                });

            modelBuilder.Entity("PersonDirectoryApi.Entities.Person", b =>
                {
                    b.Navigation("PhoneNumbers");

                    b.Navigation("Relations");
                });
#pragma warning restore 612, 618
        }
    }
}
