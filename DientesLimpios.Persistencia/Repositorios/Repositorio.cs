using DientesLimpios.Aplicacion.Contratos.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : class // Implementación genérica del repositorio para cualquier entidad
    {
        private readonly DientesLimpiosDbContext context;

        public Repositorio(DientesLimpiosDbContext context) // Constructor que recibe el contexto de la base de datos
        {
            this.context = context;
        }

        public Task Actualizar(T entidad) // Actualizar una entidad existente
        {
            context.Update(entidad);
            return Task.CompletedTask;
        }

        public Task<T> Agregar(T entidad) // Agregar una nueva entidad
        {
            context.Add(entidad);
            return Task.FromResult(entidad);
        }

        public Task Borrar(T entidad) // Borrar una entidad existente
        {
            context.Remove(entidad);
            return Task.CompletedTask;
        }

        public async Task<T?> ObtenerPorId(Guid id) // Obtener una entidad por su identificador único
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ObtenerTodos() // Obtener todas las entidades
        {
            return await context.Set<T>().ToListAsync();
        }
    }
}
