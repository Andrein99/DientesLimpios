using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Pacientes
{
    [TestClass]
    public class CasoDeUsoObtenerListadoPacientesTests
    {
        private IRepositorioPacientes repositorio;
        private CasoDeUsoObtenerListadoPacientes casoDeUso;

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioPacientes>();
            casoDeUso = new CasoDeUsoObtenerListadoPacientes(repositorio);
        }

        [TestMethod]
        public async Task Handle_RetornaPacientesPaginadosCorrectamente()
        {
            var pagina = 1; // Primera página
            var registrosPorPagina = 2; // 2 registros por página

            var filtroPacienteDTO = new FiltroPacienteDTO { Pagina = pagina, RegistrosPorPagina = registrosPorPagina }; // Filtro para la consulta

            var paciente1 = new Paciente("Felipe", new Email("felipe@ejemplo.com")); // Crear pacientes de prueba
            var paciente2 = new Paciente("Claudia", new Email("claudia@ejemplo.com")); // Crear pacientes de prueba

            IEnumerable<Paciente> pacientes = new List<Paciente> { paciente1, paciente2 }; // Lista de pacientes de prueba

            repositorio.ObtenerFiltrado(Arg.Any<FiltroPacienteDTO>()).Returns(Task.FromResult(pacientes)); // Configurar el mock para devolver los pacientes

            repositorio.ObtenerCantidadTotalRegistros().Returns(Task.FromResult(10)); // Configurar el mock para devolver el total de registros

            var request = new ConsultaObtenerListadoPacientes
            {
                Pagina = pagina,
                RegistrosPorPagina = registrosPorPagina
            }; // Crear la solicitud de consulta

            var resultado = await casoDeUso.Handle(request); // Ejecutar el caso de uso

            Assert.AreEqual(10, resultado.Total); // Verificar que el total sea correcto
            Assert.AreEqual(2, resultado.Elementos.Count); // Verificar que el número de elementos devueltos sea correcto
            Assert.AreEqual("Felipe", resultado.Elementos[0].Nombre); // Verificar que los datos de los pacientes sean correctos
            Assert.AreEqual("Claudia", resultado.Elementos[1].Nombre); // Verificar que los datos de los pacientes sean correctos
        }

        [TestMethod]
        public async Task Handle_CuandoNoHayPacientes_RetornaListaVaciaYTotalCero()
        {
            var pagina = 1; // Primera página
            var registrosPorPagina = 5; // 5 registros por página

            var filtroPacienteDTO = new FiltroPacienteDTO { Pagina = pagina, RegistrosPorPagina = registrosPorPagina }; // Filtro para la consulta

            IEnumerable<Paciente> pacientes = new List<Paciente>(); // Lista vacía de pacientes

            repositorio.ObtenerFiltrado(Arg.Any<FiltroPacienteDTO>()).Returns(Task.FromResult(pacientes)); // Configurar el mock para devolver la lista vacía

            repositorio.ObtenerCantidadTotalRegistros().Returns(Task.FromResult(0)); // Configurar el mock para devolver total de registros como 0

            var request = new ConsultaObtenerListadoPacientes
            {
                Pagina = pagina,
                RegistrosPorPagina = registrosPorPagina
            }; // Crear la solicitud de consulta

            var resultado = await casoDeUso.Handle(request); // Ejecutar el caso de uso

            Assert.AreEqual(0, resultado.Total); // Verificar que el total sea 0
            Assert.IsNotNull(resultado.Elementos); // Verificar que la lista de elementos no sea nula
            Assert.AreEqual(0, resultado.Elementos.Count); // Verificar que la lista de elementos esté vacía
        }
    }
}
