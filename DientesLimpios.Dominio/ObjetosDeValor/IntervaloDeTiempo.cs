using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.ObjetosDeValor
{
    public record IntervaloDeTiempo
    {
        public DateTime Inicio { get; }
        public DateTime Fin { get; }

        private IntervaloDeTiempo() // Constructor privado para EF Core
        {
            
        }

        public IntervaloDeTiempo(DateTime inicio, DateTime fin)
        {
            if (inicio >= fin) // Validar que la fecha de inicio no sea posterior a la fecha de fin
            {
                throw new ExcepcionDeReglaDeNegocio($"La fecha de inicio no puede ser posterior a la fecha fin.");
            }

            Inicio = inicio; // Asignar la fecha de inicio
            Fin = fin; // Asignar la fecha de fin
        }
    }
}
