using Microsoft.EntityFrameworkCore.Migrations;

namespace PwebTP.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Reservations_ReservationId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "Reviews",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ReservationId",
                table: "Reviews",
                newName: "IX_Reviews_RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Reservations_RoomId",
                table: "Reviews",
                column: "RoomId",
                principalTable: "Reservations",
                principalColumn: "ReservationsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Reservations_RoomId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Reviews",
                newName: "ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_RoomId",
                table: "Reviews",
                newName: "IX_Reviews_ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Reservations_ReservationId",
                table: "Reviews",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationsId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
