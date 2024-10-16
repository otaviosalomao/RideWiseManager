﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideWise.Api.Migrations
{
    /// <inheritdoc />
    public partial class createStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryAgents",
                columns: table => new
                {
                    IdentificationDocument = table.Column<string>(type: "text", nullable: false),
                    DriverLicenseNumber = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DriverLicenseFilePath = table.Column<string>(type: "text", nullable: false),
                    DriverLicenseType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAgents", x => new { x.DriverLicenseNumber, x.IdentificationDocument });
                });

            migrationBuilder.CreateTable(
                name: "Motorcycles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    LicensePlate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DeliveryAgentIdentification = table.Column<string>(type: "text", nullable: false),
                    MotorcycleIdentification = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EstimatedEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PlanNumber = table.Column<int>(type: "integer", nullable: false),
                    DailyValue = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalValue = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_LicensePlate",
                table: "Motorcycles",
                column: "LicensePlate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryAgents");

            migrationBuilder.DropTable(
                name: "Motorcycles");

            migrationBuilder.DropTable(
                name: "Rentals");
        }
    }
}
