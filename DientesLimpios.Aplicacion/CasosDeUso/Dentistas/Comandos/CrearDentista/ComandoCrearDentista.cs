using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.CrearDentista
{
    public class ComandoCrearDentista : IRequest<Guid>
    {
        public required string Nombre { get; set; } // Nombre del dentista
        public required string Email { get; set; } // Email del dentista
    }
}
