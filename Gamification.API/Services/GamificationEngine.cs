using Gamification.Core.Date;
using Gamification.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamification.Core.Services;

public class GamificationEngine(
    Dbcontext _context,
    ILevelService levelService,
    IXpCalculator xpCalculator,
    IAchievementEvaluator evaluator)
{
    public  PlayerProfile RegisterPlayer(string nickname)
    {
        if (string.IsNullOrWhiteSpace(nickname))
            throw new ArgumentException("Ник не может быть пустым.", nameof(nickname));
        var player = new PlayerProfile
        {
            Id = Guid.NewGuid(),
            Nickname = nickname.Trim(),
            Level = 1,
            TotalXp = 0,
            CreatedAt = DateTime.UtcNow
        };
        _context.Players.Add(player);
        var firstSteps =  _context.Achievements
            .FirstOrDefault(a => a.Code == "FIRST_STEPS");
        
        _context.PlayerAchievements.Add(new PlayerAchievement
        {
            Id = Guid.NewGuid(),
            PlayerId = player.Id,
            AchievementId = firstSteps.Id,
            UnlockedAt = DateTime.UtcNow
        });
        
        xpCalculator.AddXp(player, firstSteps.XpReward,"registration");
        
        _context.XpTransactions.Add(new XpTransaction
        {
            Id = Guid.NewGuid(),
            PlayerId = player.Id,
            Amount = firstSteps.XpReward,
            Source = "registration",
            CreatedAt = DateTime.UtcNow
        });
        _context.SaveChanges();
        return player;
    }

    public object GranXp(Guid id, int amount, string source)
    {
        if (amount <= 0)
            throw new ArgumentException("Опыт должен быть больше 0.", nameof(amount));
        if (string.IsNullOrWhiteSpace(source))
            throw new ArgumentException("Источник опыта не задан.", nameof(source));
        
        var player = _context.Players
                .Include(x=>x.XpTransactions)
                .Include(x=>x.PlayerAchievements)
                .FirstOrDefault(x => x.Id == id);//ичем пользователи со всеми его данными 
        
        if (player == null)
        {
            throw new KeyNotFoundException("Игрок не найден.");
        }
        
        int oldLevel =player.Level;

        if (amount > 0)
        {
            xpCalculator.AddXp(player, amount, source);//начислили опыт 
            _context.XpTransactions.Add(new XpTransaction
            {
                Id = Guid.NewGuid(),
                PlayerId = player.Id,
                Amount = amount,
                Source = source,
                CreatedAt = DateTime.UtcNow
            });
        }

        int levelUps = player.Level - oldLevel;//новый минус старый левел
        
        var newAchievements =evaluator.Evaluate(player, _context);//Получаем новые достижения
        
        var unlockedCodes = new List<string>();

        foreach (var ach in  newAchievements)
        {
            _context.PlayerAchievements.Add(new PlayerAchievement
            {
                Id = Guid.NewGuid(),
                PlayerId = player.Id,
                AchievementId = ach.Id,
                UnlockedAt = DateTime.UtcNow
            });
            unlockedCodes.Add(ach.Code);

            if (ach.XpReward > 0)
            {
                xpCalculator.AddXp(player, ach.XpReward, $"achievement_{ach.Code}");
                _context.XpTransactions.Add(new XpTransaction
                {
                    Id = Guid.NewGuid(),
                    PlayerId = player.Id,
                    Amount = ach.XpReward,
                    Source = $"achievement_{ach.Code}",
                    CreatedAt = DateTime.UtcNow
                });
            }
        }

        _context.SaveChanges();
        return new
        {
            totalXp = player.TotalXp,
            level = player.Level,
            levelUps,
            unlocked = unlockedCodes
        };

    }

    public PlayerProfile?  GetProfile(Guid id)
    {
        
        var player =     _context.Players.Include(x => x.PlayerAchievements)
            .ThenInclude(x => x.Achievement)
            .FirstOrDefault(x => x.Id == id);
        if (player == null)
        {
            throw new KeyNotFoundException("Игрок не найден");
        }
        return player;
    }
    
    public List<PlayerProfile> GetLeaderboard(int top = 10)
    {
        if (top < 1)
            throw new ArgumentException("top должен быть не меньше 1.", nameof(top));

        return _context.Players
            .OrderByDescending(p => p.TotalXp)
            .Take(top)
            .ToList();
    }
    
}