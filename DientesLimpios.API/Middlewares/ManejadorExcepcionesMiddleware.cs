using DientesLimpios.Aplicacion.Excepciones;
using System.Net;
using System.Text.Json;

namespace DientesLimpios.API.Middlewares
{
    public class ManejadorExcepcionesMiddleware
    {
        private readonly RequestDelegate _next;

        public ManejadorExcepcionesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ManejarExcepcion(context, ex);
            }
        }

        private Task ManejarExcepcion(HttpContext context, Exception excepcion)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError; // Código de estado por defecto
            context.Response.ContentType = "application/json"; // Tipo de contenido de la respuesta
            var resultado = string.Empty; // Resultado por defecto

            switch (excepcion)
            {
                case ExcepcionNoEncontrado:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case ExcepcionDeValidacion excepcionDeValidacion:
                    httpStatusCode = HttpStatusCode.BadRequest; // 400 Bad Request
                    resultado = JsonSerializer.Serialize(excepcionDeValidacion.ErroresDeValidacion); // Serializar los errores de validación a JSON
                    break;

            }

            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(resultado); // Escribir la respuesta al cliente
        }

    }
    public static class ManejadorExcepcionesMiddlewareExtensions
    {
        public static IApplicationBuilder UseManejadorExcepciones(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ManejadorExcepcionesMiddleware>(); // Registrar el middleware en el pipe de solicitudes
        }
    }
}
