using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Spotify.Services;
using Spotify.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Configuración JSON
builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
DatabaseConnection dbConn = new DatabaseConnection(connectionString);

// ✅ 1. Configura la política CORS (antes de Build)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173") // URL de tu frontend React
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

WebApplication app = builder.Build();

// ✅ 2. Activa la política CORS (antes de mapear los endpoints)
app.UseCors("AllowReactApp");

// Registrar endpoints
app.MapUserEndpoints(dbConn);
app.MapSongEndpoints(dbConn);
app.MapPlaylistEndpoints(dbConn);

// Ejecutar la aplicación
app.Run("http://localhost:5000");
