using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Persistencia.Utilidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class RepositorioDentistas : Repositorio<Dentista>, IRepositorioDentistas
    {
        private readonly DientesLimpiosDbContext context;

        public RepositorioDentistas(DientesLimpiosDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Dentista>> ObtenerFiltrado(FiltroDentistaDTO filtro)
        {
            var queryable = context.Dentistas.AsQueryable(); // Obtiene el IQueryable de pacientes desde el contexto de la base de datos.

            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
            {
                queryable = queryable.Where(p => p.Nombre.Contains(filtro.Nombre)); // Filtra por nombre si se proporciona.
            }

            if (!string.IsNullOrWhiteSpace(filtro.Email))
            {
                queryable = queryable.Where(p => p.Email.Valor.Contains(filtro.Email)); // Filtra por nombre si se proporciona.
            }

            return await queryable.OrderBy(p => p.Nombre).Paginar(filtro.Pagina, filtro.RegistrosPorPagina).ToListAsync(); // Retorna la lista de pacientes ordenada por nombre.
        }
    }
}
