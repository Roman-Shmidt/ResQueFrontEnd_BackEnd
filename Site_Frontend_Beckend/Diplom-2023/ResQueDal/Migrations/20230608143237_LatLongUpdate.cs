using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResQueDal.Migrations
{
    /// <inheritdoc />
    public partial class LatLongUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Restaurants");

            migrationBuilder.AddColumn<decimal>(
                name: "LatitudeGoogleMap",
                table: "Restaurants",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudeGoogleMap",
                table: "Restaurants",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatitudeGoogleMap",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "LongitudeGoogleMap",
                table: "Restaurants");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
