﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RideWise.Api.Infrastructure;

#nullable disable

namespace RideWise.Api.Migrations
{
    [DbContext(typeof(RideWiseApiDbContext))]
    partial class RideWiseApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RideWise.Api.Domain.Models.DeliveryAgent", b =>
                {
                    b.Property<int>("DriverLicenseNumber")
                        .HasColumnType("integer");

                    b.Property<string>("IdentificationDocument")
                        .HasColumnType("text");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DriverLicenseFilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DriverLicenseType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DriverLicenseNumber", "IdentificationDocument");

                    b.ToTable("DeliveryAgents");
                });

            modelBuilder.Entity("RideWise.Api.Domain.Models.Motorcycle", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LicensePlate")
                        .IsUnique();

                    b.ToTable("Motorcycles");
                });

            modelBuilder.Entity("RideWise.Api.Domain.Models.Rental", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("DailyValue")
                        .HasColumnType("numeric");

                    b.Property<string>("DeliveryAgentIdentification")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EstimatedEndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("MotorcycleIdentification")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PlanNumber")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Rentals");
                });
#pragma warning restore 612, 618
        }
    }
}
