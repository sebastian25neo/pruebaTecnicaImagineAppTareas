using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using pruebaTecnicaImagineAppTareas.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración del puerto HTTP
builder.WebHost.UseUrls("http://localhost:8080");

// Verifica si la cadena de conexión es válida
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Connection string: {connectionString}");

// Configuración de la conexión a PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configuración de NewtonsoftJson
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

// Configurar CORS para permitir solicitudes desde http://localhost:4200
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // Permitir el frontend
              .AllowAnyMethod()  // Permitir todos los métodos HTTP (GET, POST, etc.)
              .AllowAnyHeader()  // Permitir cualquier encabezado
              .AllowCredentials(); // Permitir credenciales (tokens, cookies)
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar CORS antes de los middlewares de seguridad
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
