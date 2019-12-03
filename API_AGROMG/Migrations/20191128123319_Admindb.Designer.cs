﻿// <auto-generated />
using System;
using API_AGROMG.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_AGROMG.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20191128123319_Admindb")]
    partial class Admindb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API_AGROMG.Model.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("API_AGROMG.Model.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<int>("HumanCount");

                    b.Property<string>("Name");

                    b.Property<int?>("PackageId");

                    b.Property<bool>("Status");

                    b.Property<DateTime>("StatusFinishDate");

                    b.Property<string>("Tel");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("API_AGROMG.Model.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Key");

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("API_AGROMG.Model.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("API_AGROMG.Model.LanguageContext", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Context");

                    b.Property<string>("Key");

                    b.Property<int>("LanguageId");

                    b.HasKey("Id");

                    b.ToTable("LanguageContexts");
                });

            modelBuilder.Entity("API_AGROMG.Model.Modul", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Key");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Moduls");
                });

            modelBuilder.Entity("API_AGROMG.Model.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HumanCount");

                    b.Property<string>("Key");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("API_AGROMG.Model.PackageModul", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ModulId");

                    b.Property<int?>("PackageId");

                    b.HasKey("Id");

                    b.HasIndex("ModulId");

                    b.HasIndex("PackageId");

                    b.ToTable("PackageModuls");
                });

            modelBuilder.Entity("API_AGROMG.Model.Profession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Key");

                    b.Property<bool>("ShowStatus");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Professions");
                });

            modelBuilder.Entity("API_AGROMG.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API_AGROMG.Model.Company", b =>
                {
                    b.HasOne("API_AGROMG.Model.Package", "Package")
                        .WithMany("Companies")
                        .HasForeignKey("PackageId");
                });

            modelBuilder.Entity("API_AGROMG.Model.PackageModul", b =>
                {
                    b.HasOne("API_AGROMG.Model.Modul", "Modul")
                        .WithMany("PackageModuls")
                        .HasForeignKey("ModulId");

                    b.HasOne("API_AGROMG.Model.Package", "Package")
                        .WithMany("PackageModuls")
                        .HasForeignKey("PackageId");
                });
#pragma warning restore 612, 618
        }
    }
}
