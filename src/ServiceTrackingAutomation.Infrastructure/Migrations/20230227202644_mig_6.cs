using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceTrackingAutomation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Products",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CargoTrackingNumberFromCustomer",
                table: "Complaints",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CargoTrackingNumberToCustomer",
                table: "Complaints",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CargoTrackingNumberFromCustomer",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "CargoTrackingNumberToCustomer",
                table: "Complaints");
        }
    }
}
