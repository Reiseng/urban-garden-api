using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace urban_garden_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CropTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Season = table.Column<int[]>(type: "integer[]", nullable: false),
                    IsPerennial = table.Column<bool>(type: "boolean", nullable: false),
                    Disponible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GardenPlots",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<double>(type: "double precision", nullable: false),
                    Location_Street = table.Column<string>(type: "text", nullable: false),
                    Location_City = table.Column<string>(type: "text", nullable: false),
                    Location_State = table.Column<string>(type: "text", nullable: false),
                    Location_ZipCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GardenPlots", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    MacAddress = table.Column<string>(type: "text", nullable: false),
                    ApiKey = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastSeenAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GardenPlotId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Devices_GardenPlots_GardenPlotId",
                        column: x => x.GardenPlotId,
                        principalTable: "GardenPlots",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PlantedCrops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CropTypeId = table.Column<int>(type: "integer", nullable: false),
                    GardenPlotId = table.Column<int>(type: "integer", nullable: false),
                    PlantedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantedCrops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantedCrops_CropTypes_CropTypeId",
                        column: x => x.CropTypeId,
                        principalTable: "CropTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantedCrops_GardenPlots_GardenPlotId",
                        column: x => x.GardenPlotId,
                        principalTable: "GardenPlots",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoilSensorReadings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceID = table.Column<Guid>(type: "uuid", nullable: false),
                    SensorIndex = table.Column<int>(type: "integer", nullable: false),
                    Moisture = table.Column<double>(type: "double precision", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilSensorReadings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SoilSensorReadings_Devices_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Devices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureSensorReadings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceID = table.Column<Guid>(type: "uuid", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    Humidity = table.Column<double>(type: "double precision", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureSensorReadings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TemperatureSensorReadings_Devices_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Devices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Harvests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlantedCropId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Harvests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Harvests_PlantedCrops_PlantedCropId",
                        column: x => x.PlantedCropId,
                        principalTable: "PlantedCrops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_GardenPlotId",
                table: "Devices",
                column: "GardenPlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Harvests_PlantedCropId",
                table: "Harvests",
                column: "PlantedCropId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantedCrops_CropTypeId",
                table: "PlantedCrops",
                column: "CropTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantedCrops_GardenPlotId",
                table: "PlantedCrops",
                column: "GardenPlotId");

            migrationBuilder.CreateIndex(
                name: "IX_SoilSensorReadings_DeviceID",
                table: "SoilSensorReadings",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureSensorReadings_DeviceID",
                table: "TemperatureSensorReadings",
                column: "DeviceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Harvests");

            migrationBuilder.DropTable(
                name: "SoilSensorReadings");

            migrationBuilder.DropTable(
                name: "TemperatureSensorReadings");

            migrationBuilder.DropTable(
                name: "PlantedCrops");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "CropTypes");

            migrationBuilder.DropTable(
                name: "GardenPlots");
        }
    }
}
