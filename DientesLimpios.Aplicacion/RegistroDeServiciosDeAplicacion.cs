using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
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
            services.AddScoped<IRequestHandler<ComandoCrearConsultorio, Guid>, 
                                                CasoDeUsoCrearConsultorio>(); // Registro del caso de uso
            services.AddScoped<IRequestHandler<ConsultaObtenerDetalleConsultorio, ConsultorioDetalleDTO>, 
                                                CasoDeUsoObtenerDetalleConsultorio>(); // Registro del caso de uso
            services.AddScoped<IRequestHandler<ConsultaObtenerListadoConsultorios, List<ConsultorioListadoDTO>>,
                                                CasoDeUsoObtenerListadoConsultorios>(); // Registro del caso de uso
            services.AddScoped<IRequestHandler<ComandoActualizarConsultorio>, CasoDeUsoActualizarConsultorio>(); // Registro del caso de uso
            return services;
        }
    }
}
