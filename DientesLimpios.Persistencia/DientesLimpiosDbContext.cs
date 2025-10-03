using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia
{
    public class DientesLimpiosDbContext : DbContext
    {
        public DientesLimpiosDbContext(DbContextOptions<DientesLimpiosDbContext> options) : base(options)
        {
        }

        protected DientesLimpiosDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Sobrescribe el método para configurar el modelo
        {
            base.OnModelCreating(modelBuilder); // Llama al método base para asegurar que la configuración predeterminada se aplique

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DientesLimpiosDbContext).Assembly); // Aplica todas las configuraciones de entidades en este ensamblado
        }

        public DbSet<Consultorio> Consultorios { get; set; } // DbSet para la entidad Consultorio
        public DbSet<Paciente> Pacientes { get; set; } // DbSet para la entidad Paciente
        public DbSet<Dentista> Dentistas { get; set; } // DbSet para la entidad Dentista
        public DbSet<Cita> Citas { get; set; } // DbSet para la entidad Cita
    }
}
