using MangaMechiApi.Data.Repositories;
using MangaMechiApi.Services.Interfaces;
using MangaMechiApi.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register services and repositories
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IMangaRepository, InMemoryMangaRepository>();
builder.Services.AddScoped<IPrestamoRepository, InMemoryPrestamoRepository>();
builder.Services.AddScoped<IMangaService, MangaService>();
builder.Services.AddScoped<IPrestamoService, PrestamoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // AÑADE Y CONFIGURA ESTAS LÍNEAS PARA SWAGGER UI:
    app.UseSwaggerUI(options =>
    {
        // Apunta la UI al endpoint de la especificación OpenAPI generado por MapOpenApi
        options.SwaggerEndpoint("/openapi/v1.json", "Mi API V1");
        // Define la ruta donde se servirá Swagger UI.
        // Con "swagger", accederás desde /swagger
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
