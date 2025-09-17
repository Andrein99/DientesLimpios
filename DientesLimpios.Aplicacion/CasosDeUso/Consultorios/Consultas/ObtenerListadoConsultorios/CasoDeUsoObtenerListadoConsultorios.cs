using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios
{
    public class CasoDeUsoObtenerListadoConsultorios : IRequestHandler<ConsultaObtenerListadoConsultorios,
                                                                       List<ConsultorioListadoDTO>>
    {
        private readonly IRepositorioConsultorios repositorio;

        public CasoDeUsoObtenerListadoConsultorios(IRepositorioConsultorios repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<List<ConsultorioListadoDTO>> Handle(ConsultaObtenerListadoConsultorios request)
        {
            var consultorios = await repositorio.ObtenerTodos(); // Obtiene todos los consultorios desde el repositorio
            var consultoriosDTO = consultorios.Select(consultorio => consultorio.ADto()).ToList(); // Mapea cada entidad a su DTO correspondiente
            return consultoriosDTO; // Retorna la lista de DTOs de consultorios
        }
    }
}
