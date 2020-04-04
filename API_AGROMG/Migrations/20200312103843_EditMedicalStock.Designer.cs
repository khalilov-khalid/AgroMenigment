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
    [Migration("20200312103843_EditMedicalStock")]
    partial class EditMedicalStock
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

                    b.Property<int?>("PacketId");

                    b.Property<bool>("Status");

                    b.Property<DateTime>("StatusFinishDate");

                    b.Property<string>("Tel");

                    b.HasKey("Id");

                    b.HasIndex("PacketId");

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

                    b.Property<string>("code");

                    b.Property<string>("code2");

                    b.Property<string>("iso");

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

                    b.Property<string>("LangUnicode");

                    b.HasKey("Id");

                    b.ToTable("LanguageContexts");
                });

            modelBuilder.Entity("API_AGROMG.Model.MainIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId");

                    b.Property<string>("Name");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("MainIngredients");
                });

            modelBuilder.Entity("API_AGROMG.Model.MeasurementUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("LanguageId");

                    b.Property<int>("MainId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("MeasurementUnits");
                });

            modelBuilder.Entity("API_AGROMG.Model.MedicalStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Barcode");

                    b.Property<int?>("CompanyId");

                    b.Property<decimal>("Count");

                    b.Property<DateTime>("Expirydate");

                    b.Property<int?>("NameOfDrugId");

                    b.Property<int>("WareHourse");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("NameOfDrugId");

                    b.ToTable("MedicalStock");
                });

            modelBuilder.Entity("API_AGROMG.Model.Modules", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameKey");

                    b.Property<string>("NumberKey");

                    b.HasKey("Id");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("API_AGROMG.Model.NameOfDrug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category");

                    b.Property<int?>("CompanyId");

                    b.Property<int?>("MainIngredientId");

                    b.Property<int>("MeasurementUnit");

                    b.Property<string>("Name");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("MainIngredientId");

                    b.ToTable("NameOfDrugs");
                });

            modelBuilder.Entity("API_AGROMG.Model.Packet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<int>("HumanCount");

                    b.Property<string>("NameKey");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Packets");
                });

            modelBuilder.Entity("API_AGROMG.Model.PermissionsGroups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId");

                    b.Property<string>("Name");

                    b.Property<string>("RolContent");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("PermissionsGroups");
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

            modelBuilder.Entity("API_AGROMG.Model.Technique", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color");

                    b.Property<int?>("CompanyId");

                    b.Property<DateTime>("DateOfPurchase");

                    b.Property<string>("EngineNumber");

                    b.Property<string>("EnginePower");

                    b.Property<int>("IsBusy");

                    b.Property<string>("Name");

                    b.Property<DateTime>("ProductionYear");

                    b.Property<string>("RegistrationNumber");

                    b.Property<bool>("Status");

                    b.Property<int>("TechnicalCondition");

                    b.Property<int?>("TechniqueCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("TechniqueCategoryId");

                    b.ToTable("Techniques");
                });

            modelBuilder.Entity("API_AGROMG.Model.TechniqueCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsTrailer");

                    b.Property<string>("Key");

                    b.HasKey("Id");

                    b.ToTable("TechniqueCategories");
                });

            modelBuilder.Entity("API_AGROMG.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AdminStatus");

                    b.Property<string>("Adress");

                    b.Property<DateTime>("Birthday");

                    b.Property<int?>("CompanyId");

                    b.Property<string>("Email");

                    b.Property<int>("Gender");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<int?>("PermissionsGroupsId");

                    b.Property<decimal>("Salary");

                    b.Property<bool>("Status");

                    b.Property<string>("Tel");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("PermissionsGroupsId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API_AGROMG.Model.UserProfessions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProfessionId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProfessionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProfessions");
                });

            modelBuilder.Entity("API_AGROMG.Model.WareHourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category");

                    b.Property<int?>("CompanyId");

                    b.Property<int?>("LanguageId");

                    b.Property<int>("MainId");

                    b.Property<string>("Name");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("LanguageId");

                    b.ToTable("WareHourses");
                });

            modelBuilder.Entity("API_AGROMG.Model.WareHouseCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("LanguageId");

                    b.Property<int>("MainId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("WareHouseCategories");
                });

            modelBuilder.Entity("API_AGROMG.Model.Company", b =>
                {
                    b.HasOne("API_AGROMG.Model.Packet", "Packet")
                        .WithMany()
                        .HasForeignKey("PacketId");
                });

            modelBuilder.Entity("API_AGROMG.Model.MainIngredient", b =>
                {
                    b.HasOne("API_AGROMG.Model.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("API_AGROMG.Model.MeasurementUnit", b =>
                {
                    b.HasOne("API_AGROMG.Model.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("API_AGROMG.Model.MedicalStock", b =>
                {
                    b.HasOne("API_AGROMG.Model.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("API_AGROMG.Model.NameOfDrug", "NameOfDrug")
                        .WithMany()
                        .HasForeignKey("NameOfDrugId");
                });

            modelBuilder.Entity("API_AGROMG.Model.NameOfDrug", b =>
                {
                    b.HasOne("API_AGROMG.Model.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("API_AGROMG.Model.MainIngredient", "MainIngredient")
                        .WithMany()
                        .HasForeignKey("MainIngredientId");
                });

            modelBuilder.Entity("API_AGROMG.Model.PermissionsGroups", b =>
                {
                    b.HasOne("API_AGROMG.Model.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("API_AGROMG.Model.Technique", b =>
                {
                    b.HasOne("API_AGROMG.Model.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("API_AGROMG.Model.TechniqueCategory", "TechniqueCategory")
                        .WithMany()
                        .HasForeignKey("TechniqueCategoryId");
                });

            modelBuilder.Entity("API_AGROMG.Model.User", b =>
                {
                    b.HasOne("API_AGROMG.Model.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("API_AGROMG.Model.PermissionsGroups", "PermissionsGroups")
                        .WithMany()
                        .HasForeignKey("PermissionsGroupsId");
                });

            modelBuilder.Entity("API_AGROMG.Model.UserProfessions", b =>
                {
                    b.HasOne("API_AGROMG.Model.Profession", "Profession")
                        .WithMany()
                        .HasForeignKey("ProfessionId");

                    b.HasOne("API_AGROMG.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("API_AGROMG.Model.WareHourse", b =>
                {
                    b.HasOne("API_AGROMG.Model.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("API_AGROMG.Model.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("API_AGROMG.Model.WareHouseCategory", b =>
                {
                    b.HasOne("API_AGROMG.Model.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });
#pragma warning restore 612, 618
        }
    }
}
