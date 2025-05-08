// Program.cs
using NumerologyApi.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Numerology API", Version = "v1" });
});
// CORS: Allow frontend (Vite React app)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Dependency Injection for NumerologyService
builder.Services.AddScoped<INumerologyService, NumerologyService>();

var app = builder.Build();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Numerology API V1");
    });
}

// app.UseHttpsRedirection();
// app.UseAuthorization();
app.MapControllers();
app.Run();