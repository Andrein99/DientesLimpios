using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
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
    public class CasoDeUsoBorrarConsultorioTests
    {
        private IRepositorioConsultorios repositorio;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CasoDeUsoBorrarConsultorio casoDeUso;

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CasoDeUsoBorrarConsultorio(repositorio, unidadDeTrabajo);

        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioExiste_BorraConsultorioYPersiste()
        {
            var id = Guid.NewGuid();
            var comando = new ComandoBorrarConsultorio { Id = id };
            var consultorio = new Consultorio("Consultorio A");

            repositorio.ObtenerPorId(id).Returns(consultorio);

            await casoDeUso.Handle(comando);

            await repositorio.Received(1).Borrar(consultorio);
            await unidadDeTrabajo.Received(1).Persistir();
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionNoEncontrado))]
        public async Task Handle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            var id = Guid.NewGuid();
            var comando = new ComandoBorrarConsultorio { Id = id };
            repositorio.ObtenerPorId(comando.Id).ReturnsNull();
            await casoDeUso.Handle(comando);
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcion_LlamaAReversarYLanzaExcepcion()
        {
            var id = Guid.NewGuid();
            var comando = new ComandoBorrarConsultorio { Id = id };
            var consultorio = new Consultorio("Consultorio A");

            repositorio.ObtenerPorId(id).Returns(consultorio);

            repositorio.Borrar(consultorio).Throws(new InvalidOperationException("Error al borrar"));
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => casoDeUso.Handle(comando));
            await unidadDeTrabajo.Received(1).Reversar();
        }
    }
}
