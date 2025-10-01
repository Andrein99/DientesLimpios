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
    public class DentistaConfig : IEntityTypeConfiguration<Dentista>
    {
        public void Configure(EntityTypeBuilder<Dentista> builder)
        {
            builder.Property(prop => prop.Nombre)
                .HasMaxLength(250)
                .IsRequired();

            builder.ComplexProperty(prop => prop.Email, accion =>
            {
                accion.Property(e => e.Valor).HasColumnName("Email").HasMaxLength(254);
            }); // Configura la propiedad compleja Email para que su valor se almacene en una columna llamada "Email" con una longitud máxima de 254 caracteres.
        }
    }
}
