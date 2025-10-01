using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerDetalleDentista
{
    public class CasoDeUsoObtenerDetalleDentista : IRequestHandler<ConsultaObtenerDetalleDentista, DentistaDetalleDTO>
    {
        private readonly IRepositorioDentistas repositorio;

        public CasoDeUsoObtenerDetalleDentista(IRepositorioDentistas repositorio)
        {
            this.repositorio = repositorio;
        }
        public async Task<DentistaDetalleDTO> Handle(ConsultaObtenerDetalleDentista request)
        {
            var dentista = await repositorio.ObtenerPorId(request.Id);
            if (dentista is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            return dentista.ADto();
        }
    }
}
