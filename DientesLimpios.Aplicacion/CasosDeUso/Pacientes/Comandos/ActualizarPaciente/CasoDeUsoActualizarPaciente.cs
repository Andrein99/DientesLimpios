using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente
{
    public class CasoDeUsoActualizarPaciente : IRequestHandler<ComandoActualizarPaciente>
    {
        private readonly IRepositorioPacientes repositorio;
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoActualizarPaciente(IRepositorioPacientes repositorio,
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task Handle(ComandoActualizarPaciente request)
        {
            var paciente = await repositorio.ObtenerPorId(request.Id); // Obtener el paciente por su ID.
            if (paciente is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            paciente.ActualizarNombre(request.Nombre); // Asumiendo que el método ActualizarNombre ya maneja las validaciones necesarias.
            var email = new Email(request.Email); // Crear un nuevo objeto Email para validar el formato.
            paciente.ActualizarEmail(email); // Asumiendo que el método ActualizarEmail ya maneja las validaciones necesarias.

            try
            {
                await repositorio.Actualizar(paciente); // Actualizar el paciente en el repositorio.
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
