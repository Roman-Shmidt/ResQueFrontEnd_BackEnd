using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResQueDal.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldToReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReservationApproved",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReservationCompleted",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReservationApproved",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IsReservationCompleted",
                table: "Reservations");
        }
    }
}
