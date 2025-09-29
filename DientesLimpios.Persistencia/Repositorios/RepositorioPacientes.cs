using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
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
    public class RepositorioPacientes : Repositorio<Paciente>, IRepositorioPacientes // Hereda de la clase genérica Repositorio y especifica la entidad Paciente
    {
        private readonly DientesLimpiosDbContext context;

        public RepositorioPacientes(DientesLimpiosDbContext context) : base(context) // Llama al constructor de la clase base con el contexto de la base de datos
        {
            this.context = context;
        }

        public async Task<IEnumerable<Paciente>> ObtenerFiltrado(FiltroPacienteDTO filtro)
        {
            var queryable = context.Pacientes.AsQueryable(); // Obtiene el IQueryable de pacientes desde el contexto de la base de datos.

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
