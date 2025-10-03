using DientesLimpios.API.Middlewares;
using DientesLimpios.Aplicacion;
using DientesLimpios.Persistencia;
using DientesLimpios.Infraestructura;
using DientesLimpios.API.Jobs;
using DientesLimpios.Identidad;
using DientesLimpios.Identidad.Modelos;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opciones =>
{
    opciones.Filters.Add(new AuthorizeFilter("esadmin")); // A�ade la necesidad de autorizaci�n a todos los endpoints. De lo contrario se deber�a poner [Authorize] encima de los endpoints.
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AgregarServiciosDeAplicacion(); // Extensi�n para agregar servicios de la capa de aplicaci�n
builder.Services.AgregarServiciosDePersistencia(); // Extensi�n para agregar servicios de la capa de persistencia
builder.Services.AgregarServiciosDeInfraestructura(); // Extensi�n para agregar servicios de la capa de infraestructura
builder.Services.AgregarServiciosDeIdentidad(); // Extensi�n para agregar servicios de identidad

builder.Services.AddHostedService<RecordatorioCitasJob>(); // Registro del servicio en segundo plano

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapIdentityApi<Usuario>(); // Mapeo de endpoints de identidad

app.UseManejadorExcepciones(); // Middleware personalizado para manejo de excepciones

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
