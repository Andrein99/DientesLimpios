using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoCrearConsultorioTests
    {
        private IRepositorioConsultorios repositorio; // Repositorio de consultorios para interactuar con la capa de persistencia
        private IUnidadDeTrabajo unidadDeTrabajo; // Unidad de trabajo para manejar transacciones
        private CasoDeUsoCrearConsultorio casoDeUso; // Instancia del caso de uso que se va a probar

        [TestInitialize] // Atributo que indica que este método se ejecuta antes de cada test
        public void Setup() // Configurar el entorno de prueba antes de cada test
        {
            repositorio = Substitute.For<IRepositorioConsultorios>(); // Crear un sustituto (mock) del repositorio de consultorios
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>(); // Crear un sustituto (mock) de la unidad de trabajo
            casoDeUso = new CasoDeUsoCrearConsultorio(repositorio, unidadDeTrabajo); // Crear una instancia del caso de uso con los sustitutos inyectados
        }

        [TestMethod] // Atributo que indica que este método es un test
        public async Task Handle_ComandoValido_ObtenemosIdConsultorio()
        {
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" }; // Crear un comando de creación de consultorio con un nombre válido

            var consultorioCreado = new Consultorio("Consultorio A");
            repositorio.Agregar(Arg.Any<Consultorio>()).Returns(consultorioCreado); // Simular que el repositorio agrega el consultorio y devuelve el consultorio creado

            var resultado = await casoDeUso.Handle(comando); // Ejecutar el caso de uso

            await repositorio.Received(1).Agregar(Arg.Any<Consultorio>()); // Verificar que el repositorio fue llamado una vez para agregar el consultorio
            await unidadDeTrabajo.Received(1).Persistir(); // Verificar que la unidad de trabajo fue llamada una vez para persistir los cambios
            Assert.AreNotEqual(Guid.Empty, resultado); // Verificar que el Id devuelto no es vacío
        }

        [TestMethod] // Atributo que indica que este método es un test
        public async Task Handle_CuandoHayError_HacemosRollback()
        {
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" }; // Crear un comando de creación de consultorio con un nombre válido
            repositorio.Agregar(Arg.Any<Consultorio>()).Throws<Exception>(); // Simular que el repositorio lanza una excepción al agregar el consultorio

            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                var resultado = await casoDeUso.Handle(comando); // Ejecutar el caso de uso y esperar que lance una excepción
            });

            await unidadDeTrabajo.Received(1).Reversar(); // Verificar que la unidad de trabajo fue llamada una vez para revertir los cambios
        }
    }
}
