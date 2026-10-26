using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gamification.Core.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    XpReward = table.Column<int>(type: "integer", nullable: false),
                    RuleType = table.Column<string>(type: "text", nullable: false),
                    RuleParameter = table.Column<int>(type: "integer", nullable: false),
                    RuleSource = table.Column<string>(type: "text", nullable: false),
                    IsSecret = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: false),
                    TotalXp = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerAchievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    AchievementId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnlockedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerAchievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerAchievements_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerAchievements_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XpTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XpTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XpTransactions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "Code", "Description", "IsSecret", "RuleParameter", "RuleSource", "RuleType", "Title", "XpReward" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "FIRST_STEPS", "Игрок зарегистрирован", false, 0, "", "", "Первые шаги", 10 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "LEVEL_2", "Достигнут 2-й уровень", false, 0, "", "", "Уровень 2", 25 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "LEVEL_5", "Достигнут 5-й уровень", false, 0, "", "", "Уровень 5", 100 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "LEVEL_10", "Достигнут 10-й уровень", false, 0, "", "", "Мастер", 300 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "COLLECTOR_500", "Набрано 500 XP", false, 0, "", "", "Сборщик", 50 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "COLLECTOR_5000", "Набрано 5000 XP", false, 0, "", "", "Охотник за XP", 250 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "MARATHON_7", "7 действий", false, 0, "", "", "Марафонец", 40 },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "DEDICATED", "10 действий lesson", false, 0, "", "", "Прилежный", 60 },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "SHOPAHOLIC", "5 покупок", false, 0, "", "", "Шопоголик", 50 },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "SECRET", "Секретное достижение", false, 0, "", "", "Секрет", 200 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAchievements_AchievementId",
                table: "PlayerAchievements",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAchievements_PlayerId",
                table: "PlayerAchievements",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_XpTransactions_PlayerId",
                table: "XpTransactions",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerAchievements");

            migrationBuilder.DropTable(
                name: "XpTransactions");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
