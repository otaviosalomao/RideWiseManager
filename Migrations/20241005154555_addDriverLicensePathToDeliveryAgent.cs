using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ride_wise_api.Migrations
{
    /// <inheritdoc />
    public partial class addDriverLicensePathToDeliveryAgent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "DeliveryAgents",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "DriverLicenseFilePath",
                table: "DeliveryAgents",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverLicenseFilePath",
                table: "DeliveryAgents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "DeliveryAgents",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
