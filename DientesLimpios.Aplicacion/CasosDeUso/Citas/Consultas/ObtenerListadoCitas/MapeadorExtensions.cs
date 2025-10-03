using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerListadoCitas
{
    public static class MapeadorExtensions
    {
        public static CitaListadoDTO ADto(this Cita cita)
        {
            var dto = new CitaListadoDTO
            {
                Id = cita.Id,
                Paciente = cita.Paciente!.Nombre,
                Dentista = cita.Dentista!.Nombre,
                Consultorio = cita.Consultorio!.Nombre,
                FechaInicio = cita.IntervaloDeTiempo.Inicio,
                FechaFin = cita.IntervaloDeTiempo.Fin,
                EstadoCita = cita.Estado.ToString()
            };
            return dto;
        }
    }
}
