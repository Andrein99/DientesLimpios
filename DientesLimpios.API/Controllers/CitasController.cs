using DientesLimpios.API.DTOs.Citas;
using DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CancelarCita;
using DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CompletarCita;
using DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CrearCita;
using DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerDetalleCita;
using DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerListadoCitas;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/citas")]
    public class CitasController : ControllerBase
    {
        private readonly IMediator mediator;

        public CitasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CitaListadoDTO>>> Get([FromQuery] ConsultaObtenerListadoCitas consulta)
        {
            var resultado = await mediator.Send(consulta); // Enviar la consulta al mediador
            return resultado;  // Devolver la lista de citas obtenida
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CitaDetalleDTO>> Get(Guid id)
        {
            var consulta = new ConsultaObtenerDetalleCita { Id = id }; // Crear la consulta con el ID proporcionado
            var resultado = await mediator.Send(consulta); // Enviar la consulta al mediador
            return resultado; // Devolver los detalles de la cita obtenida
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearCitaDTO crearCitaDTO)
        {
            var comando = new ComandoCrearCita
            {
                PacienteId = crearCitaDTO.PacienteId,
                DentistaId = crearCitaDTO.DentistaId,
                ConsultorioId = crearCitaDTO.ConsultorioId,
                FechaInicio = crearCitaDTO.FechaInicio,
                FechaFin = crearCitaDTO.FechaFin
            }; // Mapear DTO a comando

            var resultado = await mediator.Send(comando); // Enviar el comando al mediador
            return Ok(); // Devolver una respuesta exitosa
        }

        [HttpPost("{id}/completar")]
        public async Task<IActionResult> Completar(Guid id)
        {
            var consulta = new ComandoCompletarCita { Id = id }; // Crear el comando con el ID proporcionado
            await mediator.Send(consulta); // Enviar el comando al mediador
            return Ok(); // Devolver una respuesta exitosa
        }

        [HttpPost("{id}/cancelar")]
        public async Task<IActionResult> Cancelar(Guid id)
        {
            var comando = new ComandoCancelarCita { Id = id }; // Crear el comando con el ID proporcionado
            await mediator.Send(comando); // Enviar el comando al mediador
            return Ok(); // Devolver una respuesta exitosa
        }
    }
}
