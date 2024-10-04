using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ride_wise_api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKeyConfigFromMotorcycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Motorcycles_MotorcycleLicensePlate",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_MotorcycleLicensePlate",
                table: "Rentals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Motorcycles",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "MotorcycleLicensePlate",
                table: "Rentals");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Rentals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MotorcycleId",
                table: "Rentals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Motorcycles",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DeliveryAgents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Motorcycles",
                table: "Motorcycles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MotorcycleId",
                table: "Rentals",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_LicensePlate",
                table: "Motorcycles",
                column: "LicensePlate",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Motorcycles_MotorcycleId",
                table: "Rentals",
                column: "MotorcycleId",
                principalTable: "Motorcycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Motorcycles_MotorcycleId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_MotorcycleId",
                table: "Rentals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Motorcycles",
                table: "Motorcycles");

            migrationBuilder.DropIndex(
                name: "IX_Motorcycles_LicensePlate",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "MotorcycleId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DeliveryAgents");

            migrationBuilder.AddColumn<string>(
                name: "MotorcycleLicensePlate",
                table: "Rentals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Motorcycles",
                table: "Motorcycles",
                column: "LicensePlate");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MotorcycleLicensePlate",
                table: "Rentals",
                column: "MotorcycleLicensePlate");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Motorcycles_MotorcycleLicensePlate",
                table: "Rentals",
                column: "MotorcycleLicensePlate",
                principalTable: "Motorcycles",
                principalColumn: "LicensePlate",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
