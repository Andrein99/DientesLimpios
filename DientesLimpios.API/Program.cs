using DientesLimpios.API.Middlewares;
using DientesLimpios.Aplicacion;
using DientesLimpios.Persistencia;
using DientesLimpios.Infraestructura;
using DientesLimpios.API.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AgregarServiciosDeAplicacion(); // Extensión para agregar servicios de la capa de aplicación
builder.Services.AgregarServiciosDePersistencia(); // Extensión para agregar servicios de la capa de persistencia
builder.Services.AgregarServiciosDeInfraestructura(); // Extensión para agregar servicios de la capa de infraestructura

builder.Services.AddHostedService<RecordatorioCitasJob>(); // Registro del servicio en segundo plano

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseManejadorExcepciones(); // Middleware personalizado para manejo de excepciones

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
