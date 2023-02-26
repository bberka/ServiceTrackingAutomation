using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceTrackingAutomation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductClassId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductClassId",
                table: "Products",
                newName: "ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductClassId",
                table: "Products",
                newName: "IX_Products_ProductTypeId");

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductTypeId",
                table: "Products",
                newName: "ProductClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                newName: "IX_Products_ProductClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductClassId",
                table: "Products",
                column: "ProductClassId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
