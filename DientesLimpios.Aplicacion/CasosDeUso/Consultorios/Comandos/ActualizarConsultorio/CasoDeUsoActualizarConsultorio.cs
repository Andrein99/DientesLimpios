using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio
{
    public class CasoDeUsoActualizarConsultorio : IRequestHandler<ComandoActualizarConsultorio>
    {
        private readonly IRepositorioConsultorios repositorio;
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoActualizarConsultorio(IRepositorioConsultorios repositorio,
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task Handle(ComandoActualizarConsultorio request)
        {
            var consultorio = await repositorio.ObtenerPorId(request.Id); // Obtiene el consultorio por su Id desde el repositorio
            if (consultorio is null)
            {
                throw new ExcepcionNoEncontrado(); // Lanza una excepción si el consultorio no existe
            }

            consultorio.ActualizarNombre(request.Nombre); // Actualiza el nombre del consultorio
            try
            {
                await repositorio.Actualizar(consultorio); // Actualiza el consultorio en el repositorio
                await unidadDeTrabajo.Persistir(); // Persiste los cambios en la unidad de trabajo
            }
            catch (Exception)
            {
                await unidadDeTrabajo.Reversar(); // Reversa los cambios en caso de error
                throw; // Vuelve a lanzar la excepción para que sea manejada por el llamador
            }
        }
    }
}
