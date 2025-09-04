using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Contratos.Persistencia
{
    public interface IUnidadDeTrabajo
    {
        Task Persistir(); // Guardar los cambios realizados en la unidad de trabajo
        Task Reversar(); // Deshacer los cambios en caso de error
    }
}
