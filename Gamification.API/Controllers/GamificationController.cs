using System.Security.Claims;
using Gamification.API.Dto;
using Gamification.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamification.API.Controllers;

[ApiController]
[Route("api")]

public class GamificationController: ControllerBase
{
    private readonly GamificationEngine _engine;
    
    public GamificationController(GamificationEngine engine)
    {
        _engine = engine;
    }
    
    [HttpPost("players")]
    public IActionResult RegisterPlayer([FromBody] CreatePlayerRequest request)
    {
        var player = _engine.RegisterPlayer(request.Nickname);

        var body = new { player.Id, player.Nickname, player.TotalXp, player.Level, player.CreatedAt };
        return CreatedAtAction(nameof(GetProfile), new { id = player.Id }, body);
    }
    
    [HttpGet("players/{id:guid}")]
    public IActionResult GetProfile(Guid id)
    {
        var player = _engine.GetProfile(id)!;

        return Ok(new
        {
            player.Id,
            player.Nickname,
            player.TotalXp,
            player.Level,
            player.CreatedAt,
            achievements = player.PlayerAchievements.Select(x => new
            {
                code = x.Achievement!.Code,
                title = x.Achievement.Title,
                unlockedAt = x.UnlockedAt
            })
        });
    }

    
    [HttpPost("players/{id:guid}/xp")]
    public IActionResult GrantXp(Guid id, [FromBody] GrantXpRequest request)
        => Ok(_engine.GranXp(id, request.Amount, request.Source));   

   
    [HttpGet("players/{id:guid}/achievements")]
    public IActionResult GetPlayerAchievements(Guid id)
    {
        var player = _engine.GetProfile(id)!;

        return Ok(player.PlayerAchievements.Select(pa => new
        {
            code = pa.Achievement!.Code,
            title = pa.Achievement.Title,
            description = pa.Achievement.Description,
            unlockedAt = pa.UnlockedAt
        }));
    }

  
    [HttpGet("leaderboard")]
    public IActionResult GetLeaderboard([FromQuery] int top = 10)
    {
        var leaders = _engine.GetLeaderboard(top)
            .Select((p, i) => new { rank = i + 1, p.Id, p.Nickname, p.Level, p.TotalXp });

        return Ok(leaders);
    }
}