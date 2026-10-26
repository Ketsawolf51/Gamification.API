using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gamification.Core.Models;

public class AchievementDefinition
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Code { get; set; }
    
    [Required]
    public string Title { get; set; } 
    
    [Required]
    public string Description { get; set; } 
    public int XpReward { get; set; }

    [Required]
    public string RuleType { get; set; } 
    public int RuleParameter { get; set; }
    public string RuleSource { get; set; } 
    public bool IsSecret { get; set; }
    
    [JsonIgnore]
    public ICollection<PlayerAchievement> PlayerAchievements { get; set; } = new List<PlayerAchievement>();
    
}