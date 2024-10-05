using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ride_wise_api.Migrations
{
    /// <inheritdoc />
    public partial class removeDriverLicenseImageFromDeliveryAgent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverLicenseImage",
                table: "DeliveryAgents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverLicenseImage",
                table: "DeliveryAgents",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
