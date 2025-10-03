using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerDetalleCita
{
    public static class MapeadorExtensions
    {
        public static CitaDetalleDTO ADto(this Cita cita)
        {
            var dto =  new CitaDetalleDTO
            {
                Id = cita.Id,
                Paciente = cita.Paciente!.Nombre,
                Dentista = cita.Dentista!.Nombre,
                Consultorio = cita.Consultorio!.Nombre,
                FechaInicio = cita.IntervaloDeTiempo.Inicio,
                FechaFin = cita.IntervaloDeTiempo.Fin,
                EstadoCita = cita.Estado.ToString()
            }; // Mapear las propiedades de la entidad Cita al DTO
            return dto; // Retornar el DTO mapeado
        }
    }
}
