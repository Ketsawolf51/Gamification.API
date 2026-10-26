
using Gamification.Core.Models;

namespace Gamification.Core.Services;

public  interface IXpCalculator
{ 
    void AddXp(PlayerProfile player, int amount, string source);
}