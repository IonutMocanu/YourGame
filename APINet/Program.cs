using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using APINet.Database;
using APINet.Shared.Database.Models;
using APINet.Services.Abstractions;
using APINet.Services.Implementations;
// using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(o => {
    o.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

builder.Services.AddDbContext<GameDatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Game API",
        Description = "API pentru a gestiona partea de backend a jocului făcut în cadrul acest proiect",
        TermsOfService = new Uri("https://example.com/terms")
    });
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICarService, CarService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Asigură-te că folosești numele corect al DbContext-ului tău
        var context = services.GetRequiredService<APINet.Database.GameDatabaseContext>();
        
        // Asta aplică toate migrările care lipsesc (creează tabelele)
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Eroare la migrare: {ex.Message}");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
