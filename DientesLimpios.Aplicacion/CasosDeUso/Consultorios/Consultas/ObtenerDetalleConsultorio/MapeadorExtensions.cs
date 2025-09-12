using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio
{
    public static class MapeadorExtensions
    {
        public static ConsultorioDetalleDTO ADto(this Consultorio consultorio) // Método de extensión para mapear una entidad Consultorio a un DTO ConsultorioDetalleDTO.
        {
            var dto = new ConsultorioDetalleDTO
            {
                Id = consultorio.Id,
                Nombre = consultorio.Nombre
            };
            return dto; // Retorna el DTO mapeado desde la entidad Consultorio.
        }
    }
}
