using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class changesInArchicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_Raffles_RaffleId",
                table: "Gifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Winnings_Raffles_RaffleId",
                table: "Winnings");

            migrationBuilder.DropTable(
                name: "Raffles");

            migrationBuilder.DropIndex(
                name: "IX_Winnings_RaffleId",
                table: "Winnings");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_RaffleId",
                table: "Gifts");

            migrationBuilder.DropColumn(
                name: "RaffleDate",
                table: "Winnings");

            migrationBuilder.DropColumn(
                name: "RaffleId",
                table: "Winnings");

            migrationBuilder.DropColumn(
                name: "RaffleId",
                table: "Gifts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RaffleDate",
                table: "Winnings",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "RaffleId",
                table: "Winnings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RaffleId",
                table: "Gifts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Raffles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RaffleDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raffles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Winnings_RaffleId",
                table: "Winnings",
                column: "RaffleId");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_RaffleId",
                table: "Gifts",
                column: "RaffleId");

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
    }
}
