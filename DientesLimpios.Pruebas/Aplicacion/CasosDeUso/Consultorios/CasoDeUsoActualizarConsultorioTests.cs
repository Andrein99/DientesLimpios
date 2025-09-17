using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoActualizarConsultorioTests
    {
        private IRepositorioConsultorios repositorio;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CasoDeUsoActualizarConsultorio casoDeUso;

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CasoDeUsoActualizarConsultorio(repositorio, unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioExiste_ActualizaNombreYPersiste()
        {
            var consultorio = new Consultorio("Consultorio A"); // Crear un consultorio de prueba
            var id = consultorio.Id; // Obtener el ID del consultorio
            var comando = new ComandoActualizarConsultorio { Id = id, Nombre = "Nuevo nombre" }; // Crear el comando con el nuevo nombre

            repositorio.ObtenerPorId(id).Returns(consultorio); // Configurar el repositorio para que devuelva el consultorio cuando se busque por ID

            await casoDeUso.Handle(comando); // Ejecutar el caso de uso

            await repositorio.Received(1).Actualizar(consultorio); // Verificar que el método Actualizar del repositorio fue llamado una vez
            await unidadDeTrabajo.Received(1).Persistir(); // Verificar que el método Persistir de la unidad de trabajo fue llamado una vez
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionNoEncontrado))]
        public async Task Handle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            var id = Guid.NewGuid(); // Crear un ID de prueba
            var comando = new ComandoActualizarConsultorio { Id = id, Nombre = "Nuevo nombre" }; // Crear el comando con el nuevo nombre
            repositorio.ObtenerPorId(comando.Id).ReturnsNull(); // Configurar el repositorio para que devuelva null cuando se busque por ID
            await casoDeUso.Handle(comando); // Ejecutar el caso de uso, se espera que lance una excepción
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcionAlActualizar_LlamaAReversarYLanzaExcepcion()
        {
            var consultorio = new Consultorio("Consultorio A"); // Crear un consultorio de prueba
            var id = consultorio.Id; // Obtener el ID del consultorio
            var comando = new ComandoActualizarConsultorio { Id = id, Nombre = "Consultorio B" }; // Crear el comando con el nuevo nombre
            
            repositorio.ObtenerPorId(id).Returns(consultorio); // Configurar el repositorio para que devuelva el consultorio cuando se busque por ID
            repositorio.Actualizar(consultorio).Throws(new Exception("Error al actualizar")); // Configurar el repositorio para que lance una excepción al actualizar

            await Assert.ThrowsExceptionAsync<Exception>(() => casoDeUso.Handle(comando)); // Ejecutar el caso de uso y verificar que lanza una excepción
            await unidadDeTrabajo.Received(1).Reversar(); // Verificar que el método Reversar de la unidad de trabajo fue llamado una vez
        }
    }
}
