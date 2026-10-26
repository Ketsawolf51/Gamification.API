using Gamification.Core.Date;
using Gamification.Core.Models;

namespace Gamification.Core.Services;

public class AchievementEvaluator:IAchievementEvaluator
{
    public List<AchievementDefinition> Evaluate(PlayerProfile player, Dbcontext context)
    {
        var un = new List<AchievementDefinition>(); //это пустой лист в который будет записоваться достижение играка

        var allAchievements = context.Achievements.ToList();//берем из бд весь всписок достижение
        
        var playerAchIds = player.PlayerAchievements.Select(x=>x.AchievementId).ToList();//Узнаём, какие достижения уже выданы
        foreach (var achievement in allAchievements.Where(x=>!playerAchIds.Contains(x.Id)))
        {
           
            bool isUnlocked = false;
            switch (achievement.Code)
            {
                case "FIRST_STEPS":
                    isUnlocked = true;
                    break;

                case "LEVEL_2":
                    isUnlocked = player.Level >= 2;
                    break;

                case "LEVEL_5":
                    isUnlocked = player.Level >= 5;
                    break;

                case "LEVEL_10":
                    isUnlocked = player.Level >= 10;
                    break;

                case "COLLECTOR_500":
                    isUnlocked = player.TotalXp >= 500;
                    break;

                case "COLLECTOR_5000":
                    isUnlocked = player.TotalXp >= 5000;
                    break;

                case "MARATHON_7":
                    isUnlocked = player.XpTransactions.Count >= 7;
                    break;

                case "DEDICATED":
                    isUnlocked = player.XpTransactions.Count(x => x.Source == "lesson") >= 10;
                    break;

                case "SHOPAHOLIC":
                    isUnlocked = player.XpTransactions.Count(x => x.Source == "purchase") >= 5;
                    break;

                case "SECRET":
                    int sourceCount = player.XpTransactions.Select(x => x.Source).Distinct().Count();
                    isUnlocked = player.Level >= 7 && sourceCount >= 3;
                    break;

                default:
                    isUnlocked = false;
                    break;
            }

            if (isUnlocked)
            {
                un.Add(achievement);
            }
            
        }
        
        return un;
    }
}