using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAPTechChallenge.Migrations
{
    /// <inheritdoc />
    public partial class PromotionWithGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Games_GameId",
                table: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Promotions_GameId",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Promotions");

            migrationBuilder.AddColumn<int>(
                name: "PromotionId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_PromotionId",
                table: "Games",
                column: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Promotions_PromotionId",
                table: "Games",
                column: "PromotionId",
                principalTable: "Promotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Promotions_PromotionId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_PromotionId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Promotions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_GameId",
                table: "Promotions",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Games_GameId",
                table: "Promotions",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
