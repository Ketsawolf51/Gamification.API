using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Gamification.Core.Models;

public class PlayerAchievement
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey("Player")]
    public Guid PlayerId { get; set; }
    [ForeignKey("Achievement")]
    public Guid AchievementId { get; set; }
    public DateTime UnlockedAt { get; set; }
    [JsonIgnore]
    public PlayerProfile? Player { get; set; }
    public AchievementDefinition? Achievement { get; set; }
}