using MangaMechiApi.Infrastructure.Data;
using MangaMechiApi.Infrastructure.Repositories;
using MangaMechiApi.Core.Interfaces;
using MangaMechiApi.Application.Services;
using MangaMechiApi.Application.Mappings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Configure Database
var databaseSettings = new DatabaseSettings();
builder.Services.AddSingleton(databaseSettings);

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => { });

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Register repositories
builder.Services.AddScoped<IMangaRepository, SqlServerMangaRepository>();
builder.Services.AddScoped<IPrestamoRepository, SqlServerPrestamoRepository>();

// Register services
builder.Services.AddScoped<IMangaService, MangaService>();
builder.Services.AddScoped<IPrestamoService, PrestamoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Mi API V1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
