using Gamification.API.Middleware;
using Gamification.Core.Date;
using Gamification.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Dbcontext>();
builder.Services.AddSingleton<ILevelService, LevelService>();
builder.Services.AddSingleton<IXpCalculator, XpCalculator>();
builder.Services.AddSingleton<IAchievementEvaluator, AchievementEvaluator>();

builder.Services.AddScoped<GamificationEngine>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();