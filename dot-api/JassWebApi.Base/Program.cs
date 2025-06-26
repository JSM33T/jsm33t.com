using JassWebApi.Entities.Shared;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var allConfig = builder.Configuration.GetSection("JsmtConfig").Get<JsmtConfig>();

builder.Services.AddSingleton(allConfig!);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

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
