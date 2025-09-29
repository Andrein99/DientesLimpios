using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.BorrarPaciente
{
    public class CasoDeUsoBorrarPaciente : IRequestHandler<ComandoBorrarPaciente>
    {
        private readonly IRepositorioPacientes repositorio;
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoBorrarPaciente(IRepositorioPacientes repositorio, 
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }
        public async Task Handle(ComandoBorrarPaciente request)
        {
            var paciente = await repositorio.ObtenerPorId(request.Id); // Obtener el paciente por su ID.
            if (paciente is null)
            {
                throw new ExcepcionNoEncontrado();
            }
            try
            {
                await repositorio.Borrar(paciente); // Borrar el paciente del repositorio.
                await unidadDeTrabajo.Persistir(); // Guardar los cambios realizados en la unidad de trabajo.
            }
            catch (Exception)
            {
                await unidadDeTrabajo.Reversar(); // Revertir los cambios en caso de error.
                throw;
            }
        }
    }
}
