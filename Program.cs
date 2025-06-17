using MangaMechiApi.Data;
using MangaMechiApi.Data.Repositories;
using MangaMechiApi.Services.Interfaces;
using MangaMechiApi.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Configure Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services and repositories
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IMangaRepository, SqlServerMangaRepository>();
builder.Services.AddScoped<IPrestamoRepository, SqlServerPrestamoRepository>();
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
