using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace urban_garden_api.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Config_KeepAliveInterval",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Config_SoilMoistureInterval",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Config_SoilSensorCount",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Config_TemperatureInterval",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Config_KeepAliveInterval",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Config_SoilMoistureInterval",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Config_SoilSensorCount",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Config_TemperatureInterval",
                table: "Devices");
        }
    }
}
