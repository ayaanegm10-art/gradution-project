using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace try4.Migrations
{
    /// <inheritdoc />
    public partial class addCenterAndRoutTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Centers",
                columns: table => new
                {
                    CenterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centers", x => x.CenterId);
                });

            migrationBuilder.CreateTable(
                name: "Routs",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterAId = table.Column<int>(type: "int", nullable: false),
                    CenterBId = table.Column<int>(type: "int", nullable: false),
                    MinCenterId = table.Column<int>(type: "int", nullable: false),
                    MaxCenterId = table.Column<int>(type: "int", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routs", x => x.RouteId);
                    table.ForeignKey(
                        name: "FK_Routs_Centers_CenterAId",
                        column: x => x.CenterAId,
                        principalTable: "Centers",
                        principalColumn: "CenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Routs_Centers_CenterBId",
                        column: x => x.CenterBId,
                        principalTable: "Centers",
                        principalColumn: "CenterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_FromCenterId",
                table: "Trips",
                column: "FromCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_RouteId",
                table: "Trips",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ToCenterId",
                table: "Trips",
                column: "ToCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Routs_CenterAId",
                table: "Routs",
                column: "CenterAId");

            migrationBuilder.CreateIndex(
                name: "IX_Routs_CenterBId",
                table: "Routs",
                column: "CenterBId");

            // Ensure any Centers referenced by existing Trips are present before adding FK constraints.
            // Insert missing FromCenterId values
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Centers ON;

                INSERT INTO Centers (CenterId, Name, OrderIndex, IsActive)
                SELECT DISTINCT t.FromCenterId, 'Unknown', NULL, 1
                FROM Trips t
                WHERE t.FromCenterId IS NOT NULL
                  AND t.FromCenterId NOT IN (SELECT CenterId FROM Centers);

                INSERT INTO Centers (CenterId, Name, OrderIndex, IsActive)
                SELECT DISTINCT t.ToCenterId, 'Unknown', NULL, 1
                FROM Trips t
                WHERE t.ToCenterId IS NOT NULL
                  AND t.ToCenterId NOT IN (SELECT CenterId FROM Centers);

                SET IDENTITY_INSERT Centers OFF;

                SET IDENTITY_INSERT Routs ON;

                -- Insert missing Routs referenced by Trips, using Trip's from/to centers
                INSERT INTO Routs (RouteId, CenterAId, CenterBId, MinCenterId, MaxCenterId, DurationMinutes, IsActive)
                SELECT DISTINCT t.RouteId,
                       ISNULL(t.FromCenterId, t.ToCenterId),
                       ISNULL(t.ToCenterId, t.FromCenterId),
                       ISNULL(t.FromCenterId, t.ToCenterId),
                       ISNULL(t.ToCenterId, t.FromCenterId),
                       0, 1
                FROM Trips t
                WHERE t.RouteId IS NOT NULL
                  AND t.RouteId NOT IN (SELECT RouteId FROM Routs);

                SET IDENTITY_INSERT Routs OFF;
            ");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Centers_FromCenterId",
                table: "Trips",
                column: "FromCenterId",
                principalTable: "Centers",
                principalColumn: "CenterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Centers_ToCenterId",
                table: "Trips",
                column: "ToCenterId",
                principalTable: "Centers",
                principalColumn: "CenterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Routs_RouteId",
                table: "Trips",
                column: "RouteId",
                principalTable: "Routs",
                principalColumn: "RouteId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Centers_FromCenterId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Centers_ToCenterId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Routs_RouteId",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "Routs");

            migrationBuilder.DropTable(
                name: "Centers");

            migrationBuilder.DropIndex(
                name: "IX_Trips_FromCenterId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_RouteId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_ToCenterId",
                table: "Trips");
        }
    }
}
