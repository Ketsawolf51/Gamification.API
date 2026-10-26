namespace Gamification.Core.Services;

public interface ILevelService
{
    int GetLevelForXp(int xp);//>Возвращает уровень
    int GetXpForLevel(int level);//Возвращает суммарный опыт
    double GetProgressToNextLevel(int xp);//Возвращает долю прогресса до следующего уровня
}