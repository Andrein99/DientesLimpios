using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoObtenerListadoConsultoriosTests
    {
        private IRepositorioConsultorios repositorio;
        private CasoDeUsoObtenerListadoConsultorios casoDeUso;

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            casoDeUso = new CasoDeUsoObtenerListadoConsultorios(repositorio);
        }

        [TestMethod]
        public async Task Handle_CuandoHayConsultorios_RetornaListaDeConsultorioListadoDTO() // Prueba que verifica que el caso de uso retorna una lista de DTOs cuando hay consultorios en el repositorio
        {
            var consultorios = new List<Consultorio>
            {
                new Consultorio("Consultorio A"),
                new Consultorio("Consultorio B"),
            }; // Lista de consultorios de prueba

            repositorio.ObtenerTodos().Returns(consultorios); // Configura el mock para que devuelva la lista de consultorios

            var esperado = consultorios.Select(c => new ConsultorioListadoDTO { Id = c.Id, Nombre = c.Nombre }).ToList(); // DTO esperado

            var resultado = await casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());

            Assert.AreEqual(esperado.Count, resultado.Count);

            for (int i = 0; i < esperado.Count; i++)
            {
                Assert.AreEqual(esperado[i].Id, resultado[i].Id);
                Assert.AreEqual(esperado[i].Nombre, resultado[i].Nombre);
            }
        }

        [TestMethod]
        public async Task Handle_CuandoNoHayConsultorios_RetornaListaVacia() // Prueba que verifica que el caso de uso retorna una lista vacía cuando no hay consultorios en el repositorio
        {
            repositorio.ObtenerTodos().Returns(new List<Consultorio>()); // Configura el mock para que devuelva una lista vacía
            var resultado = await casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());
            Assert.IsNotNull(resultado);
            Assert.AreEqual(0, resultado.Count);
        }
    }
}
