using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamification.Core.Models;

public class PlayerProfile
{
    [Key]
    public Guid Id  { get; set; }
    public string Nickname { get; set; }
    public int TotalXp  { get; set; }
    public int Level { get; set; } 
    public DateTime CreatedAt { get; set; }
    
    public ICollection<PlayerAchievement> PlayerAchievements { get; set; } = new List<PlayerAchievement>();
    public ICollection<XpTransaction> XpTransactions { get; set; } = new List<XpTransaction>();
    
   
}