using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas
{
    public class CasoDeUsoObtenerListadoDentistas : IRequestHandler<ConsultaObtenerListadoDentistas,
                                                                        PaginadoDTO<DentistaListadoDTO>>
    {
        private readonly IRepositorioDentistas repositorio;

        public CasoDeUsoObtenerListadoDentistas(IRepositorioDentistas repositorio)
        {
            this.repositorio = repositorio;
        }
        public async Task<PaginadoDTO<DentistaListadoDTO>> Handle(ConsultaObtenerListadoDentistas request)
        {
            var dentistas = await repositorio.ObtenerFiltrado(request); // Obtiene todos los dentistas desde el repositorio
            var totalDentistas = await repositorio.ObtenerCantidadTotalRegistros(); // Obtiene la cantidad total de dentistas
            var dentistasDTO = dentistas.Select(dentista => dentista.ADto()).ToList(); // Mapea las entidades a DTOs

            var paginadoDTO = new PaginadoDTO<DentistaListadoDTO>
            {
                Elementos = dentistasDTO,
                Total = totalDentistas
            };

            return paginadoDTO;
        }
    }
}
