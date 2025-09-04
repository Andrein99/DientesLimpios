using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Excepciones
{
    public class ExcepcionDeReglaDeNegocio: Exception
    {
        public ExcepcionDeReglaDeNegocio(string mensaje) : base(mensaje) // Constructor que recibe un mensaje de error y lo envía a la clase base.
        {
            
        }
    }
}
