using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios.Modelos;
using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class RepositorioCitas : Repositorio<Cita>, IRepositorioCitas
    {
        private readonly DientesLimpiosDbContext context;

        public RepositorioCitas(DientesLimpiosDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> ExisteCitaSolapada(Guid dentistaId, DateTime inicio, DateTime fin)
        {
            return await context.Citas
                .Where(c => c.DentistaId == dentistaId && c.Estado == Dominio.Enums.EstadoCita.Programada 
                && inicio < c.IntervaloDeTiempo.Fin && fin > c.IntervaloDeTiempo.Inicio).AnyAsync(); // Verificar si existe alguna cita que se solape con el intervalo dado
        }

        public async Task<IEnumerable<Cita>> ObtenerFiltrado(FiltroCitasDTO filtroCitasDTO)
        {
            var queryable = context.Citas // Obtener la consulta base de citas
                                .Include(c => c.Paciente)
                                .Include(c => c.Dentista) // Incluir detalles del dentista
                                .Include(c => c.Consultorio) // Incluir detalles del consultorio
                                .AsQueryable(); // Crear una consulta base para las citas

            if (filtroCitasDTO.ConsultorioId is not null)
            {
                queryable = queryable.Where(c => c.ConsultorioId == filtroCitasDTO.ConsultorioId); // Filtrar por ID de consultorio si se proporciona
            }

            if (filtroCitasDTO.DentistaId is not null)
            {
                queryable = queryable.Where(c => c.DentistaId == filtroCitasDTO.DentistaId); // Filtrar por ID de dentista si se proporciona
            }

            if (filtroCitasDTO.PacienteId is not null)
            {
                queryable = queryable.Where(c => c.PacienteId == filtroCitasDTO.PacienteId); // Filtrar por ID de paciente si se proporciona
            }

            if (filtroCitasDTO.EstadoCita is not null)
            {
                queryable = queryable.Where(c => c.Estado == filtroCitasDTO.EstadoCita); // Filtrar por estado de la cita si se proporciona
            }

            return await queryable.Where(c => c.IntervaloDeTiempo.Inicio >= filtroCitasDTO.FechaInicio // Filtrar por la fecha de inicio proporcionada
                                        && c.IntervaloDeTiempo.Fin < filtroCitasDTO.FechaFin) // Filtrar por el rango de fechas proporcionado
                                    .OrderBy(c => c.IntervaloDeTiempo.Inicio) // Ordenar por la fecha de inicio de la cita
                                    .ToListAsync(); // Ejecutar la consulta y obtener la lista de citas filtradas 
        }

        new public async Task<Cita?> ObtenerPorId(Guid id) // Obtener una cita por su ID, incluyendo detalles relacionados. Se utiliza "new" para ocultar el método base.
        {
            return await context.Citas // Acceder al DbSet de citas
                .Include(c => c.Paciente) // Incluir detalles del paciente
                .Include(c => c.Dentista) // Incluir detalles del dentista
                .Include(c => c.Consultorio) // Incluir detalles del consultorio
                .FirstOrDefaultAsync(c => c.Id == id); // Obtener la cita por su ID
        }
    }
}
