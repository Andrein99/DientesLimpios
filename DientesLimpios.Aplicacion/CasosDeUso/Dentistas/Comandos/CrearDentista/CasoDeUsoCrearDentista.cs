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

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.CrearDentista
{
    public class CasoDeUsoCrearDentista : IRequestHandler<ComandoCrearDentista, Guid>
    {
        private readonly IRepositorioDentistas repositorio;
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoCrearDentista(IRepositorioDentistas repositorio, 
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<Guid> Handle(ComandoCrearDentista request)
        {
            var email = new Email(request.Email); // Crear una instancia del objeto de valor Email con el email proporcionado en el comando
            var dentista = new Dentista(request.Nombre, email); // Crear una nueva instancia de Dentista con el nombre y email proporcionados en el comando

            try
            {
                var respuesta = await repositorio.Agregar(dentista); // Agregar el nuevo dentista al repositorio
                await unidadDeTrabajo.Persistir(); // Guardar los cambios en la unidad de trabajo
                return respuesta.Id; // Devolver el Id del dentista recién creado
            }
            catch (Exception)
            {
                await unidadDeTrabajo.Reversar(); // En caso de error, revertir los cambios en la unidad de trabajo
                throw; // Volver a lanzar la excepción para que pueda ser manejada por el llamador
            }
        }
    }
}
