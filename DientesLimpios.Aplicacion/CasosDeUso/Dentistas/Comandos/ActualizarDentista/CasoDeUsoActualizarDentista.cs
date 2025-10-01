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

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.ActualizarDentista
{
    public class CasoDeUsoActualizarDentista : IRequestHandler<ComandoActualizarDentista>
    {
        private readonly IRepositorioDentistas repositorio;
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoActualizarDentista(IRepositorioDentistas repositorio, 
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }
        public async Task Handle(ComandoActualizarDentista request)
        {
            var dentista = await repositorio.ObtenerPorId(request.Id); // Obtener el dentista por su ID.
            if (dentista is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            dentista.ActualizarNombre(request.Nombre); // Asumiendo que el método ActualizarNombre ya maneja las validaciones necesarias.
            var email = new Email(request.Email); // Crear un nuevo objeto Email para validar el formato.
            dentista.ActualizarEmail(email); // Asumiendo que el método ActualizarEmail ya maneja las validaciones necesarias.
            try
            {
                await repositorio.Actualizar(dentista); // Actualizar el paciente en el repositorio.
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
