using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente
{
    public class CasoDeUsoCrearPaciente : IRequestHandler<ComandoCrearPaciente, Guid>
    {
        private readonly IRepositorioPacientes repositorio;
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoCrearPaciente(IRepositorioPacientes repositorio,
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<Guid> Handle(ComandoCrearPaciente request)
        {
            // Orquestamos la lógica del caso de uso aquí
            var email = new Email(request.Email); // Crear una instancia del objeto de valor Email con el email proporcionado en el comando
            var paciente = new Paciente(request.Nombre, email); // Crear una nueva instancia de Paciente con el nombre y email proporcionados en el comando

            try
            {
                var respuesta = await repositorio.Agregar(paciente); // Agregar el nuevo paciente al repositorio
                await unidadDeTrabajo.Persistir(); // Guardar los cambios en la unidad de trabajo
                return respuesta.Id; // Devolver el Id del paciente recién creado
            }
            catch (Exception)
            {
                await unidadDeTrabajo.Reversar(); // En caso de error, revertir los cambios en la unidad de trabajo
                throw; // Volver a lanzar la excepción para que pueda ser manejada por el llamador
            }
        }
    }
}
