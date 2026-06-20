using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace try4.Migrations
{
    /// <inheritdoc />
    public partial class FixSearchTripsHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Searches_Trips_TripId",
                table: "Searches");

            migrationBuilder.DropIndex(
                name: "IX_Searches_TripId",
                table: "Searches");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Searches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Searches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Searches_TripId",
                table: "Searches",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Searches_Trips_TripId",
                table: "Searches",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId");
        }
    }
}
