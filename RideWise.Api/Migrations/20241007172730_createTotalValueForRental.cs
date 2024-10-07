using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideWise.Api.Migrations
{
    /// <inheritdoc />
    public partial class createTotalValueForRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "Rentals",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "Rentals");
        }
    }
}
