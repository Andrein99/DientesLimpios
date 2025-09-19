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
            return await context.Pacientes.OrderBy(p => p.Nombre).Paginar(filtro.Pagina, filtro.RegistrosPorPagina).ToListAsync(); // Retorna la lista de pacientes ordenada por nombre.
        }
    }
}
