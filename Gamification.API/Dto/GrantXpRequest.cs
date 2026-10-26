using System.ComponentModel.DataAnnotations;

namespace Gamification.API.Dto;

public class GrantXpRequest
{
    [Range(1, 100, ErrorMessage = "Опыт должен быть от 1 до 100")]
    public int Amount { get; set; }

    [Required(ErrorMessage = "Источник обязателен.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Источник должен быть от 2 до 50 символов.")]
    
    public string Source { get; set; } = string.Empty;
}