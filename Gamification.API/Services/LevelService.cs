namespace Gamification.Core.Services;

public class LevelService:ILevelService
{
    private int Base = 100;

    public int GetXpForLevel(int level)
    {
        return Base * level * (level - 1) / 2;
    }

    public int GetLevelForXp(int xp)
    { 
        int level = 1;
        while (GetXpForLevel(level + 1) <= xp)
        {
            level++;
        }

        return level;
    }

    public double GetProgressToNextLevel(int xp)
    {
        int level = GetLevelForXp(xp);
        int current = GetXpForLevel(level);
        int next = GetXpForLevel(level + 1);

        return (double)(xp - current) / (next - current);
    }
}