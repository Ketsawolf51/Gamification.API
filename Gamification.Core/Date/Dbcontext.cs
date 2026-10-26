using Gamification.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamification.Core.Date;

public class Dbcontext: DbContext
{
    public DbSet<PlayerProfile> Players { get; set; }
    public DbSet<XpTransaction> XpTransactions { get; set; }
    public DbSet<AchievementDefinition> Achievements { get; set; }
    public DbSet<PlayerAchievement> PlayerAchievements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(  "Host=localhost;Port=5432;Database=GamificationDb;Username=postgres;Password=123456789");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var achievements = new List<AchievementDefinition>
        {
            new()
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Code = "FIRST_STEPS",
                Title = "Первые шаги",
                Description = "Игрок зарегистрирован",
                XpReward = 10
            },
            new()
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Code = "LEVEL_2",
                Title = "Уровень 2",
                Description = "Достигнут 2-й уровень",
                XpReward = 25
            },
            new()
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Code = "LEVEL_5",
                Title = "Уровень 5",
                Description = "Достигнут 5-й уровень",
                XpReward = 100
            },
            new()
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Code = "LEVEL_10",
                Title = "Мастер",
                Description = "Достигнут 10-й уровень",
                XpReward = 300
            },
            new()
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Code = "COLLECTOR_500",
                Title = "Сборщик",
                Description = "Набрано 500 XP",
                XpReward = 50
            },
            new()
            {
                Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                Code = "COLLECTOR_5000",
                Title = "Охотник за XP",
                Description = "Набрано 5000 XP",
                XpReward = 250
            },
            new()
            {
                Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                Code = "MARATHON_7",
                Title = "Марафонец",
                Description = "7 действий",
                XpReward = 40
            },
            new()
            {
                Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                Code = "DEDICATED",
                Title = "Прилежный",
                Description = "10 действий lesson",
                XpReward = 60
            },
            new()
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                Code = "SHOPAHOLIC",
                Title = "Шопоголик",
                Description = "5 покупок",
                XpReward = 50
            },
            new()
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Code = "SECRET",
                Title = "Секрет",
                Description = "Секретное достижение",
                XpReward = 200
            }
        };

        modelBuilder.Entity<AchievementDefinition>().HasData(achievements);
    }
}