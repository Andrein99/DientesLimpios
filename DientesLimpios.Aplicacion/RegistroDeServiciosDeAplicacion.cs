using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion
{
    public static class RegistroDeServiciosDeAplicacion
    {
        public static IServiceCollection AgregarServiciosDeAplicacion(this IServiceCollection services)
        {
            services.AddTransient<IMediator, MediadorSimple>();

            services.Scan(scan =>
                scan.FromAssembliesOf(typeof(IMediator))
                    .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>))) // Sin respuesta
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                    .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>))) // Con respuesta
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()); // Registro automático de todos los manejadores de solicitudes

            // Registro manual de casos de uso (opcional si no se usa el escaneo automático). Usado como referencia por si no se quiere usar en otro contexto (No se actualizará porque se usará el escaneo automático).
            //services.AddScoped<IRequestHandler<ComandoCrearConsultorio, Guid>, 
            //                                    CasoDeUsoCrearConsultorio>(); // Registro del caso de uso
            //services.AddScoped<IRequestHandler<ComandoActualizarConsultorio>, CasoDeUsoActualizarConsultorio>(); // Registro del caso de uso
            //services.AddScoped<IRequestHandler<ComandoBorrarConsultorio>, CasoDeUsoBorrarConsultorio>(); // Registro del caso de uso
            //services.AddScoped<IRequestHandler<ConsultaObtenerDetalleConsultorio, ConsultorioDetalleDTO>, 
            //                                    CasoDeUsoObtenerDetalleConsultorio>(); // Registro del caso de uso
            //services.AddScoped<IRequestHandler<ConsultaObtenerListadoConsultorios, List<ConsultorioListadoDTO>>,
            //                                    CasoDeUsoObtenerListadoConsultorios>(); // Registro del caso de uso
            return services;
        }
    }
}
