using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ride_wise_api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryAgents_Rentals_Identification",
                table: "DeliveryAgents");

            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycles_Rentals_Identification",
                table: "Motorcycles");

            migrationBuilder.DropIndex(
                name: "IX_Motorcycles_Identification",
                table: "Motorcycles");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryAgents_Identification",
                table: "DeliveryAgents");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryAgentDriverLicenseNumber",
                table: "Rentals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAgentIdentificationDocument",
                table: "Rentals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotorcycleLicensePlate",
                table: "Rentals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DeliveryAgentDriverLicenseNumber_DeliveryAgentIdent~",
                table: "Rentals",
                columns: new[] { "DeliveryAgentDriverLicenseNumber", "DeliveryAgentIdentificationDocument" });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MotorcycleLicensePlate",
                table: "Rentals",
                column: "MotorcycleLicensePlate");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_DeliveryAgents_DeliveryAgentDriverLicenseNumber_Del~",
                table: "Rentals",
                columns: new[] { "DeliveryAgentDriverLicenseNumber", "DeliveryAgentIdentificationDocument" },
                principalTable: "DeliveryAgents",
                principalColumns: new[] { "DriverLicenseNumber", "IdentificationDocument" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Motorcycles_MotorcycleLicensePlate",
                table: "Rentals",
                column: "MotorcycleLicensePlate",
                principalTable: "Motorcycles",
                principalColumn: "LicensePlate",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_DeliveryAgents_DeliveryAgentDriverLicenseNumber_Del~",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Motorcycles_MotorcycleLicensePlate",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_DeliveryAgentDriverLicenseNumber_DeliveryAgentIdent~",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_MotorcycleLicensePlate",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "DeliveryAgentDriverLicenseNumber",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "DeliveryAgentIdentificationDocument",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "MotorcycleLicensePlate",
                table: "Rentals");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_Identification",
                table: "Motorcycles",
                column: "Identification",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAgents_Identification",
                table: "DeliveryAgents",
                column: "Identification",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryAgents_Rentals_Identification",
                table: "DeliveryAgents",
                column: "Identification",
                principalTable: "Rentals",
                principalColumn: "Identification",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycles_Rentals_Identification",
                table: "Motorcycles",
                column: "Identification",
                principalTable: "Rentals",
                principalColumn: "Identification",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
