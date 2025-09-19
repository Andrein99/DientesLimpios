using DientesLimpios.API.DTOs.Pacientes;
using DientesLimpios.API.Utilidades;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/pacientes")]
    public class PacientesController : ControllerBase
    {
        private readonly IMediator mediator;

        public PacientesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<PacienteListadoDTO>>> Get([FromQuery] ConsultaObtenerListadoPacientes consulta)
        {
            var resultado = await mediator.Send(consulta); // Enviamos la consulta al mediador para que la maneje
            HttpContext.InsertarPaginacionEnCabecera(resultado.Total); // Insertamos la paginación en las cabeceras de la respuesta
            return resultado.Elementos; // Retornamos el listado de pacientes
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearPacienteDTO crearPacienteDTO)
        {
            var comando = new ComandoCrearPaciente { Nombre = crearPacienteDTO.Nombre, Email = crearPacienteDTO.Email }; // Mapeamos el DTO al comando
            await mediator.Send(comando); // Enviamos el comando al mediador para que lo maneje
            return Ok(); // Retornamos un 200 OK si la creación fue exitosa
        }
    }
}
