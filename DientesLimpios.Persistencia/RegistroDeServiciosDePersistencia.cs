using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Persistencia.Repositorios;
using DientesLimpios.Persistencia.UnidadesDeTrabajo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia
{
    public static class RegistroDeServiciosDePersistencia
    {
        public static IServiceCollection AgregarServiciosDePersistencia(this IServiceCollection services) // Método de extensión para registrar servicios de persistencia
        {
            services.AddDbContext<DientesLimpiosDbContext>(options => 
                options.UseSqlServer("name=DientesLimpiosConnectionString")); // Configurar el DbContext con SQL Server
            services.AddScoped<IRepositorioConsultorios, RepositorioConsultorios>(); // Registrar el repositorio de consultorios
            services.AddScoped<IRepositorioPacientes, RepositorioPacientes>(); // Registrar el repositorio de pacientes
            services.AddScoped<IRepositorioDentistas, RepositorioDentistas>(); // Registrar el repositorio de dentistas
            services.AddScoped<IRepositorioCitas, RepositorioCitas>(); // Registrar el repositorio de citas
            services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajoEFCore>(); // Registrar la unidad de trabajo

            return services;
        }
    }
}
