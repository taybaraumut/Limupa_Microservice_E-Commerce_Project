﻿// <auto-generated />
using System;
using Limupa.Cargo.DataAccessLayer.Concrete.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Limupa.Cargo.DataAccessLayer.Migrations
{
    [DbContext(typeof(CargoContext))]
    partial class CargoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Limupa.Cargo.EntityLayer.Concrete.CargoCompany", b =>
                {
                    b.Property<int>("CargoCompanyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CargoCompanyID"));

                    b.Property<string>("CargoCompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CargoCompanyID");

                    b.ToTable("CargoCompanies");
                });

            modelBuilder.Entity("Limupa.Cargo.EntityLayer.Concrete.CargoCustomer", b =>
                {
                    b.Property<int>("CargoCustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CargoCustomerID"));

                    b.Property<string>("CargoCustomerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CargoCustomerCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CargoCustomerDistrict")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CargoCustomerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CargoCustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CargoCustomerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CargoCustomerSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CargoCustomerUserID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CargoCustomerID");

                    b.ToTable("CargoCustomers");
                });

            modelBuilder.Entity("Limupa.Cargo.EntityLayer.Concrete.CargoDetail", b =>
                {
                    b.Property<int>("CargoDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CargoDetailID"));

                    b.Property<int>("CargoBarcode")
                        .HasColumnType("int");

                    b.Property<int>("CargoCompanyId")
                        .HasColumnType("int");

                    b.Property<string>("CargoReceiverCustomer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CargoSenderCustomer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CargoDetailID");

                    b.HasIndex("CargoCompanyId");

                    b.ToTable("CargoDetails");
                });

            modelBuilder.Entity("Limupa.Cargo.EntityLayer.Concrete.CargoOperation", b =>
                {
                    b.Property<int>("CargoOperationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CargoOperationID"));

                    b.Property<string>("CargoBarcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CargoDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CargoOperationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CargoOperationID");

                    b.ToTable("CargoOperations");
                });

            modelBuilder.Entity("Limupa.Cargo.EntityLayer.Concrete.CargoDetail", b =>
                {
                    b.HasOne("Limupa.Cargo.EntityLayer.Concrete.CargoCompany", "CargoCompany")
                        .WithMany()
                        .HasForeignKey("CargoCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CargoCompany");
                });
#pragma warning restore 612, 618
        }
    }
}
