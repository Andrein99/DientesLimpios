using DientesLimpios.API.DTOs.Pacientes;
using DientesLimpios.API.Utilidades;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.BorrarPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerDetallePaciente;
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
        public async Task<ActionResult<List<PacienteListadoDTO>>> Get([FromQuery] ConsultaObtenerListadoPacientes consulta) // Endpoint para obtener el listado de pacientes con paginación
        {
            var resultado = await mediator.Send(consulta); // Enviamos la consulta al mediador para que la maneje
            HttpContext.InsertarPaginacionEnCabecera(resultado.Total); // Insertamos la paginación en las cabeceras de la respuesta
            return resultado.Elementos; // Retornamos el listado de pacientes
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDetalleDTO>> Get(Guid id) // Endpoint para obtener el detalle de un paciente por su ID
        {
            var consulta = new ConsultaObtenerDetallePaciente { Id = id }; // Creamos la consulta con el ID del paciente
            var resultado = await mediator.Send(consulta); // Enviamos la consulta al mediador para que la maneje
            return resultado; // Retornamos el detalle del paciente
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearPacienteDTO crearPacienteDTO) // Endpoint para crear un nuevo paciente
        {
            var comando = new ComandoCrearPaciente { Nombre = crearPacienteDTO.Nombre, Email = crearPacienteDTO.Email }; // Mapeamos el DTO al comando
            await mediator.Send(comando); // Enviamos el comando al mediador para que lo maneje
            return Ok(); // Retornamos un 200 OK si la creación fue exitosa
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActualizarPacienteDTO actualizarPacienteDTO) // Endpoint para actualizar un paciente existente
        {
            var comando = new ComandoActualizarPaciente { Id = id, Nombre = actualizarPacienteDTO.Nombre, Email = actualizarPacienteDTO.Email }; // Mapeamos el DTO al comando
            await mediator.Send(comando); // Enviamos el comando al mediador para que lo maneje
            return Ok(); // Retornamos un 204 No Content si la actualización fue exitosa
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) // Endpoint para borrar un paciente por su ID
        {
            var comando = new ComandoBorrarPaciente { Id = id }; // Creamos el comando con el ID del paciente a borrar
            await mediator.Send(comando); // Enviamos el comando al mediador para que lo maneje
            return Ok(); // Retornamos un 204 No Content si el borrado fue exitoso
        }
    }
}
