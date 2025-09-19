using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    public interface IRepositorio<T> where T : class
    {
        Task<T?> ObtenerPorId(Guid id); // Obtener una entidad por su identificador único
        Task<IEnumerable<T>> ObtenerTodos(); // Obtener todas las entidades
        Task<int> ObtenerCantidadTotalRegistros(); // Obtener la cantidad total de registros de una entidad.
        Task<T> Agregar(T entidad); // Agregar una nueva entidad
        Task Actualizar(T entidad); // Actualizar una entidad existente
        Task Borrar(T entidad); // Borrar una entidad existente
    }
}
