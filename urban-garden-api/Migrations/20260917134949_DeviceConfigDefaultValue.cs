using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace urban_garden_api.Migrations
{
    /// <inheritdoc />
    public partial class DeviceConfigDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Config_TemperatureInterval",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 900,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Config_SoilSensorCount",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 5,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Config_SoilMoistureInterval",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 3600,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Config_KeepAliveInterval",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 300,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Config_TemperatureInterval",
                table: "Devices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 900);

            migrationBuilder.AlterColumn<int>(
                name: "Config_SoilSensorCount",
                table: "Devices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 5);

            migrationBuilder.AlterColumn<int>(
                name: "Config_SoilMoistureInterval",
                table: "Devices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 3600);

            migrationBuilder.AlterColumn<int>(
                name: "Config_KeepAliveInterval",
                table: "Devices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 300);
        }
    }
}
