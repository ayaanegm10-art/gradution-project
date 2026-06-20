using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace try4.Migrations
{
    /// <inheritdoc />
    public partial class SearchTripsHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Searches_Trips_TripId",
                table: "Searches");

            migrationBuilder.DropIndex(
                name: "IX_Searches_UserId",
                table: "Searches");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ToId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "SearchId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "Searches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FromId",
                table: "Searches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SearchDate",
                table: "Searches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "Searches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToId",
                table: "Searches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Searches_FromId",
                table: "Searches",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Searches_ToId",
                table: "Searches",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_Searches_UserId",
                table: "Searches",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Searches_Centers_FromId",
                table: "Searches",
                column: "FromId",
                principalTable: "Centers",
                principalColumn: "CenterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Searches_Centers_ToId",
                table: "Searches",
                column: "ToId",
                principalTable: "Centers",
                principalColumn: "CenterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Searches_Trips_TripId",
                table: "Searches",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Searches_Centers_FromId",
                table: "Searches");

            migrationBuilder.DropForeignKey(
                name: "FK_Searches_Centers_ToId",
                table: "Searches");

            migrationBuilder.DropForeignKey(
                name: "FK_Searches_Trips_TripId",
                table: "Searches");

            migrationBuilder.DropIndex(
                name: "IX_Searches_FromId",
                table: "Searches");

            migrationBuilder.DropIndex(
                name: "IX_Searches_ToId",
                table: "Searches");

            migrationBuilder.DropIndex(
                name: "IX_Searches_UserId",
                table: "Searches");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "Searches");

            migrationBuilder.DropColumn(
                name: "SearchDate",
                table: "Searches");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Searches");

            migrationBuilder.DropColumn(
                name: "ToId",
                table: "Searches");

            migrationBuilder.AddColumn<int>(
                name: "FromId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "Searches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Searches_UserId",
                table: "Searches",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Searches_Trips_TripId",
                table: "Searches",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
