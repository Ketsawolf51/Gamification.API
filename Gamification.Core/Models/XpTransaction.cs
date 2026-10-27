using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Gamification.Core.Models;

public class XpTransaction
{
    [Key]
    public Guid Id { get; set; }// это самой записи зачесление опыта 
    public int Amount { get; set; }
    public string Source{ get; set; }// еще проверка на больше 0 
    public DateTime CreatedAt { get; set; } 
    
    [ForeignKey("Player")]
    public Guid  PlayerId{ get; set; }// это уже играка
    [JsonIgnore]
    public PlayerProfile? Player { get; set; }
    
}