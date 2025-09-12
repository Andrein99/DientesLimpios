using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Configuraciones
{
    public class ConsultorioConfig : IEntityTypeConfiguration<Consultorio> // Implementa la interfaz para configurar la entidad Consultorio
    {
        public void Configure(EntityTypeBuilder<Consultorio> builder) // Método para configurar la entidad Consultorio
        {
            builder.Property(prop => prop.Nombre) // Configura la propiedad Nombre de la entidad Consultorio
                .HasMaxLength(150) // Establece la longitud máxima a 150 caracteres
                .IsRequired(); // Marca la propiedad como obligatoria (no nula)
        }
    }
}
