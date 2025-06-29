using JassWebApi.Data;
using JassWebApi.Entities.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var allConfig = builder.Configuration.GetSection("JsmtConfig").Get<JsmtConfig>();

builder.Services.AddSingleton(allConfig!);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(allConfig?.ConnectionSettings.PostgresConstr));

builder.Services.AddScoped<IChangeLogRepository, ChangeLogRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
