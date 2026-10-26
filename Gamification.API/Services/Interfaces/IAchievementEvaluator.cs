using System.Data.Common;
using Gamification.Core.Date;
using Gamification.Core.Models;

namespace Gamification.Core.Services;

public interface IAchievementEvaluator
{
    List<AchievementDefinition> Evaluate(PlayerProfile player,  Dbcontext context);
}