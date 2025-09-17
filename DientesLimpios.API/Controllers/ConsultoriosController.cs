using DientesLimpios.API.DTOs.Consultorios;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/consultorios")]
    public class ConsultoriosController : ControllerBase
    {
        private readonly IMediator mediator;

        public ConsultoriosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // Aquí irán los endpoints del controlador
        [HttpGet]
        public async Task<ActionResult<List<ConsultorioListadoDTO>>> Get() // Obtener todos los consultorios
        {
            var consulta = new ConsultaObtenerListadoConsultorios(); // Crear la consulta para obtener el listado de consultorios
            var resultado = await mediator.Send(consulta); // Enviar la consulta al mediador y esperar el resultado
            return resultado; // Retornar el resultado como respuesta HTTP
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultorioDetalleDTO>> Get(Guid id) // Obtener detalle de un consultorio por ID
        {
            var consulta = new ConsultaObtenerDetalleConsultorio { Id = id }; // Crear la consulta con el ID del consultorio
            var resultado = await mediator.Send(consulta); // Enviar la consulta al mediador y esperar el resultado
            return resultado; // Retornar el resultado como respuesta HTTP
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearConsultorioDTO crearConsultorioDTO)
        {
            var comando = new ComandoCrearConsultorio { Nombre = crearConsultorioDTO.Nombre }; // Mapea el DTO al comando
            await mediator.Send(comando); // Envía el comando al mediador
            return Ok(); // Retorna una respuesta 200 OK
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActualizarConsultorioDTO actualizarConsultorioDTO)
        {
            var comando = new ComandoActualizarConsultorio
            {
                Id = id,
                Nombre = actualizarConsultorioDTO.Nombre
            }; // Mapea el DTO al comando
            await mediator.Send(comando); // Envía el comando al mediador
            return Ok(); // Retorna una respuesta 200 OK
        }
    }
}
