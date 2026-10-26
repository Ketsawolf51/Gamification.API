namespace Gamification.Core.Services;

public interface IGameEvent
{
    Guid PlayerId { get; }
    DateTime OccurredAt { get; }
}

public class XpGained : IGameEvent
{
    public Guid PlayerId { get; }
    public int Amount { get; }
    public string Source { get; }
    public DateTime OccurredAt { get; }

    public XpGained(Guid playerId, int amount, string source, DateTime occurredAt)
    {
        PlayerId = playerId;
        Amount = amount;
        Source = source;
        OccurredAt = occurredAt;
    }
}

//Игрок повысил уровень на один шаг
public class LevelUp : IGameEvent
{
    public Guid PlayerId { get; }
    public int OldLevel { get; }
    public int NewLevel { get; }
    public DateTime OccurredAt { get; }

    public LevelUp(Guid playerId, int oldLevel, int newLevel, DateTime occurredAt)
    {
        PlayerId = playerId;
        OldLevel = oldLevel;
        NewLevel = newLevel;
        OccurredAt = occurredAt;
    }
}

//Игроку выдано достижение
public class AchievementUnlocked : IGameEvent
{
    public Guid PlayerId { get; }
    public string Code { get; }
    public DateTime OccurredAt { get; }

    public AchievementUnlocked(Guid playerId, string code, DateTime occurredAt)
    {
        PlayerId = playerId;
        Code = code;
        OccurredAt = occurredAt;
    }
}