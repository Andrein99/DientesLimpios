using DientesLimpios.API.DTOs.Dentistas;
using DientesLimpios.API.Utilidades;
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.ActualizarDentista;
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.BorrarDentista;
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.CrearDentista;
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerDetalleDentista;
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/dentistas")]
    public class DentistasController : ControllerBase
    {
        private readonly IMediator mediator;

        public DentistasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<DentistaListadoDTO>>> Get([FromQuery] ConsultaObtenerListadoDentistas consulta) // Endpoint para obtener el listado de dentistas con paginación
        {
            var resultado = await mediator.Send(consulta); // Enviamos la consulta al mediador para que la maneje
            HttpContext.InsertarPaginacionEnCabecera(resultado.Total); // Insertamos la paginación en las cabeceras de la respuesta
            return resultado.Elementos; // Retornamos el listado de dentistas
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DentistaDetalleDTO>> Get(Guid id) // Endpoint para obtener el detalle de un dentista por su ID
        {
            var consulta = new ConsultaObtenerDetalleDentista { Id = id }; // Creamos la consulta con el ID del dentista
            var resultado = await mediator.Send(consulta); // Enviamos la consulta al mediador para que la maneje
            return resultado; // Retornamos el detalle del dentista
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearDentistaDTO crearDentistaDTO)
        {
            var comando = new ComandoCrearDentista { Nombre = crearDentistaDTO.Nombre, Email = crearDentistaDTO.Email }; // Mapeamos el DTO al comando
            await mediator.Send(comando); // Enviamos el comando al mediador para que lo maneje
            return Ok(); // Retornamos un 200 OK si la creación fue exitosa
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActualizarDentistaDTO actualizarDentistaDTO) // Endpoint para actualizar un dentista existente
        {
            var comando = new ComandoActualizarDentista { Id = id, Nombre = actualizarDentistaDTO.Nombre, Email = actualizarDentistaDTO.Email }; // Mapeamos el DTO al comando
            await mediator.Send(comando); // Enviamos el comando al mediador para que lo maneje
            return Ok(); // Retornamos un 204 No Content si la actualización fue exitosa
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) // Endpoint para borrar un dentista por su ID
        {
            var comando = new ComandoBorrarDentista { Id = id }; // Creamos el comando con el ID del dentista a borrar
            await mediator.Send(comando); // Enviamos el comando al mediador para que lo maneje
            return Ok(); // Retornamos un 200 OK si el borrado fue exitoso
        }
    }
}
