using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Pacientes
{
    [TestClass]
    public class CasoDeUsoCrearPacienteTests
    {
        private IRepositorioPacientes repositorio; // Repositorio de consultorios para interactuar con la capa de persistencia
        private IUnidadDeTrabajo unidadDeTrabajo; // Unidad de trabajo para manejar transacciones
        private CasoDeUsoCrearPaciente casoDeUso; // Instancia del caso de uso que se va a probar

        [TestInitialize] // Atributo que indica que este método se ejecuta antes de cada test
        public void Setup() // Configurar el entorno de prueba antes de cada test
        {
            repositorio = Substitute.For<IRepositorioPacientes>(); // Crear un sustituto (mock) del repositorio de pacientes
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>(); // Crear un sustituto (mock) de la unidad de trabajo
            casoDeUso = new CasoDeUsoCrearPaciente(repositorio, unidadDeTrabajo); // Crear una instancia del caso de uso con los sustitutos inyectados
        }

        [TestMethod] // Atributo que indica que este método es un test
        public async Task Handle_CuandoDatosValidos_CreaPacienteYPersisteYRetornaId()
        {
            var comando = new ComandoCrearPaciente { Nombre = "Paciente A", Email = "pacienteA@ejemplo.com" }; // Crear un comando de creación de paciente con un nombre y email válidos
            var pacienteCreado = new Paciente(comando.Nombre, new Email(comando.Email));
            var id = pacienteCreado.Id;

            repositorio.Agregar(Arg.Any<Paciente>()).Returns(pacienteCreado); // Simular que el repositorio agrega el paciente y devuelve el paciente creado

            var idResultado = await casoDeUso.Handle(comando); // Ejecutar el caso de uso

            Assert.AreEqual(id, idResultado); // Verificar que el Id devuelto es el esperado
            await repositorio.Received(1).Agregar(Arg.Any<Paciente>()); // Verificar que el repositorio fue llamado una vez para agregar el paciente
            await unidadDeTrabajo.Received(1).Persistir(); // Verificar que la unidad de trabajo fue llamada una vez para persistir los cambios
        }

        [TestMethod] // Atributo que indica que este método es un test
        public async Task Handle_CuandoOcurreExcepcion_ReversarYLanzaExcepcion()
        {
            var comando = new ComandoCrearPaciente { Nombre = "Paciente A", Email = "pacienteA@ejemplo.com" };
            repositorio.Agregar(Arg.Any<Paciente>()).Throws(new InvalidOperationException("Error al insertar"));

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => casoDeUso.Handle(comando)); // Ejecutar el caso de uso y esperar que lance una excepción

            await unidadDeTrabajo.Received(1).Reversar(); // Verificar que la unidad de trabajo fue llamada una vez para revertir los cambios
            await unidadDeTrabajo.DidNotReceive().Persistir(); // Verificar que la unidad de trabajo no fue llamada para persistir los cambios
        }
    }
}
