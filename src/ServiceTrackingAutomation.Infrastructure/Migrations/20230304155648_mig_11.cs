using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceTrackingAutomation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceActions");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedFromServiceDate",
                table: "Complaints",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentToServiceDate",
                table: "Complaints",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Complaints",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceNote",
                table: "Complaints",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ServiceId",
                table: "Complaints",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Services_ServiceId",
                table: "Complaints",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Services_ServiceId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ServiceId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ReceivedFromServiceDate",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "SentToServiceDate",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ServiceNote",
                table: "Complaints");

            migrationBuilder.CreateTable(
                name: "ServiceActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ReceivedFromServiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentToServiceDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceActions_Complaints_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "Complaints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceActions_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceActions_ComplaintId",
                table: "ServiceActions",
                column: "ComplaintId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceActions_ServiceId",
                table: "ServiceActions",
                column: "ServiceId");
        }
    }
}
