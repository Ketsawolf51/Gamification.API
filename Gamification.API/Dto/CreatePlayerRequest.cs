using System.ComponentModel.DataAnnotations;

namespace Gamification.API.Dto;

public class CreatePlayerRequest
{
    [Required(ErrorMessage = "Ник обязателен.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Ник должен быть от 3 до 30 символов.")]
    public string Nickname { get; set; } = string.Empty;
}