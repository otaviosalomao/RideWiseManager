﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ride_wise_api.Infrastructure;

#nullable disable

namespace ride_wise_api.Migrations
{
    [DbContext(typeof(RiseWiseManagerDbContext))]
    [Migration("20241005133857_removeDriverLicenseImageFromDeliveryAgent")]
    partial class removeDriverLicenseImageFromDeliveryAgent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ride_wise_api.Domain.Models.DeliveryAgent", b =>
                {
                    b.Property<int>("DriverLicenseNumber")
                        .HasColumnType("integer");

                    b.Property<string>("IdentificationDocument")
                        .HasColumnType("text");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DriverLicenseType")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Identification")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DriverLicenseNumber", "IdentificationDocument");

                    b.ToTable("DeliveryAgents");
                });

            modelBuilder.Entity("ride_wise_api.Domain.Models.Motorcycle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Identification")
                        .IsRequired()
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

            modelBuilder.Entity("ride_wise_api.Domain.Models.Rental", b =>
                {
                    b.Property<string>("Identification")
                        .HasColumnType("text");

                    b.Property<int>("DeliveryAgentDriverLicenseNumber")
                        .HasColumnType("integer");

                    b.Property<string>("DeliveryAgentIdentification")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeliveryAgentIdentificationDocument")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EndDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EstimatedEndDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("MotorcycleId")
                        .HasColumnType("integer");

                    b.Property<string>("MotorcycleIdentification")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PlanNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Identification");

                    b.HasIndex("MotorcycleId");

                    b.HasIndex("DeliveryAgentDriverLicenseNumber", "DeliveryAgentIdentificationDocument");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("ride_wise_api.Domain.Models.Rental", b =>
                {
                    b.HasOne("ride_wise_api.Domain.Models.Motorcycle", "Motorcycle")
                        .WithMany()
                        .HasForeignKey("MotorcycleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ride_wise_api.Domain.Models.DeliveryAgent", "DeliveryAgent")
                        .WithMany()
                        .HasForeignKey("DeliveryAgentDriverLicenseNumber", "DeliveryAgentIdentificationDocument")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryAgent");

                    b.Navigation("Motorcycle");
                });
#pragma warning restore 612, 618
        }
    }
}
