using DientesLimpios.Aplicacion.Contratos.Identidad;
using DientesLimpios.Dominio.Comunes;
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
        private readonly IServicioUsuarios? servicioUsuarios;

        public DientesLimpiosDbContext(DbContextOptions<DientesLimpiosDbContext> options,
            IServicioUsuarios servicioUsuarios) : base(options)
        {
            this.servicioUsuarios = servicioUsuarios;
        }

        protected DientesLimpiosDbContext()
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (servicioUsuarios is not null)
            {
                foreach (var entry in ChangeTracker.Entries<EntidadAuditable>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.FechaCreacion = DateTime.UtcNow;
                            entry.Entity.CreadoPor = servicioUsuarios.ObtenerUsuarioId();
                            break;
                        case EntityState.Modified:
                            entry.Entity.UltimaFechaModificacion = DateTime.UtcNow;
                            entry.Entity.UltimaModificacionPor = servicioUsuarios.ObtenerUsuarioId();
                            break;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
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
