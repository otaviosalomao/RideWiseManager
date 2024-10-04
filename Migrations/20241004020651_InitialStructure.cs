using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ride_wise_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Identification = table.Column<string>(type: "text", nullable: false),
                    DeliveryAgentIdentification = table.Column<string>(type: "text", nullable: false),
                    MotorcycleIdentification = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<string>(type: "text", nullable: false),
                    EndDate = table.Column<string>(type: "text", nullable: false),
                    EstimatedEndDate = table.Column<string>(type: "text", nullable: false),
                    PlanNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Identification);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryAgents",
                columns: table => new
                {
                    IdentificationDocument = table.Column<string>(type: "text", nullable: false),
                    DriverLicenseNumber = table.Column<int>(type: "integer", nullable: false),
                    Identification = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DriverLicenseType = table.Column<int>(type: "integer", nullable: false),
                    DriverLicenseImage = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAgents", x => new { x.DriverLicenseNumber, x.IdentificationDocument });
                    table.ForeignKey(
                        name: "FK_DeliveryAgents_Rentals_Identification",
                        column: x => x.Identification,
                        principalTable: "Rentals",
                        principalColumn: "Identification",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Motorcycles",
                columns: table => new
                {
                    LicensePlate = table.Column<string>(type: "text", nullable: false),
                    Identification = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.LicensePlate);
                    table.ForeignKey(
                        name: "FK_Motorcycles_Rentals_Identification",
                        column: x => x.Identification,
                        principalTable: "Rentals",
                        principalColumn: "Identification",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAgents_Identification",
                table: "DeliveryAgents",
                column: "Identification",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_Identification",
                table: "Motorcycles",
                column: "Identification",
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
