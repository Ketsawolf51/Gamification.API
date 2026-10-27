using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gamification.Core.Models;

public class AchievementDefinition
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Code { get; set; } = string.Empty;
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    public int XpReward { get; set; }

    [Required]
    public string RuleType { get; set; } = string.Empty;
    public int RuleParameter { get; set; }
    public string RuleSource { get; set; } = string.Empty;
    public bool IsSecret { get; set; }
    
    [JsonIgnore]
    public ICollection<PlayerAchievement> PlayerAchievements { get; set; } = new List<PlayerAchievement>();
    
}