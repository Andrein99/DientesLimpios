using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class CasoDeUsoCrearConsultorio : IRequestHandler<ComandoCrearConsultorio, Guid>
    {
        private readonly IRepositorioConsultorios repositorio; // Repositorio de consultorios para interactuar con la capa de persistencia
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoCrearConsultorio(IRepositorioConsultorios repositorio,
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio; // Inyectamos el repositorio de consultorios a través del constructor
            this.unidadDeTrabajo = unidadDeTrabajo; // Inyectamos la unidad de trabajo a través del constructor
        }

        public async Task<Guid> Handle(ComandoCrearConsultorio comando)
        {
            // Orquestamos la lógica del caso de uso aquí
            var consultorio = new Consultorio(comando.Nombre); // Crear una nueva instancia de Consultorio con el nombre proporcionado en el comando
            
            try
            {
                var respuesta = await repositorio.Agregar(consultorio); // Agregar el nuevo consultorio al repositorio
                await unidadDeTrabajo.Persistir(); // Guardar los cambios en la unidad de trabajo
                return respuesta.Id; // Devolver el Id del consultorio recién creado
            }
            catch (Exception)
            {
                await unidadDeTrabajo.Reversar(); // En caso de error, revertir los cambios en la unidad de trabajo
                throw; // Volver a lanzar la excepción para que pueda ser manejada por el llamador
            }
        }
    }
}
