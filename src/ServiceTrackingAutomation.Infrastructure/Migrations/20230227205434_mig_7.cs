using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceTrackingAutomation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargoTrackingNumberFromCustomer",
                table: "Complaints");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CargoTrackingNumberFromCustomer",
                table: "Complaints",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}
