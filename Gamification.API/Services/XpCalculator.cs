using Gamification.Core.Models;

namespace Gamification.Core.Services;

public class XpCalculator(ILevelService levelService): IXpCalculator
{
    public void AddXp(PlayerProfile player, int amount, string source)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Опыт не можеть быть 0");
        }
        
        player.TotalXp+=amount;
        player.Level = levelService.GetLevelForXp(player.TotalXp);
    }
}