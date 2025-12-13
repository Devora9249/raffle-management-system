using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_RaffleModel_RaffleId",
                table: "Gifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Winnings_RaffleModel_RaffleId",
                table: "Winnings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RaffleModel",
                table: "RaffleModel");

            migrationBuilder.RenameTable(
                name: "RaffleModel",
                newName: "Raffles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Raffles",
                table: "Raffles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_Raffles_RaffleId",
                table: "Gifts",
                column: "RaffleId",
                principalTable: "Raffles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Winnings_Raffles_RaffleId",
                table: "Winnings",
                column: "RaffleId",
                principalTable: "Raffles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_Raffles_RaffleId",
                table: "Gifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Winnings_Raffles_RaffleId",
                table: "Winnings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Raffles",
                table: "Raffles");

            migrationBuilder.RenameTable(
                name: "Raffles",
                newName: "RaffleModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RaffleModel",
                table: "RaffleModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_RaffleModel_RaffleId",
                table: "Gifts",
                column: "RaffleId",
                principalTable: "RaffleModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Winnings_RaffleModel_RaffleId",
                table: "Winnings",
                column: "RaffleId",
                principalTable: "RaffleModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
