using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAPTechChallenge.Migrations
{
    /// <inheritdoc />
    public partial class PromotionGameManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "GamePromotion",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    PromotionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePromotion", x => new { x.GamesId, x.PromotionId });
                    table.ForeignKey(
                        name: "FK_GamePromotion_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePromotion_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePromotion_PromotionId",
                table: "GamePromotion",
                column: "PromotionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePromotion");

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
    }
}
