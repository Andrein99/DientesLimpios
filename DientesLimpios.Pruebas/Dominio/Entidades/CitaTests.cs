using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    [TestClass]
    public class CitaTests
    {
        private Guid _pacienteId = Guid.NewGuid(); // Generar un nuevo identificador único para el paciente
        private Guid _dentistaId = Guid.NewGuid(); // Generar un nuevo identificador único para el dentista
        private Guid _consultorioId = Guid.NewGuid(); // Generar un nuevo identificador único para el consultorio
        private IntervaloDeTiempo _intervalo = new IntervaloDeTiempo(DateTime.UtcNow.AddDays(1),
                                DateTime.UtcNow.AddDays(2)); // Crear un intervalo de tiempo válido para la cita

        [TestMethod]
        public void Constructor_CitaValida_EstadoEsProgramada()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo); // Crear una nueva cita válida

            Assert.AreEqual(_pacienteId, cita.PacienteId); // Verificar que el ID del paciente sea correcto
            Assert.AreEqual(_dentistaId, cita.DentistaId); // Verificar que el ID del dentista sea correcto
            Assert.AreEqual(_consultorioId, cita.ConsultorioId); // Verificar que el ID del consultorio sea correcto
            Assert.AreEqual(_intervalo, cita.IntervaloDeTiempo); // Verificar que el intervalo de tiempo sea correcto
            Assert.AreEqual(EstadoCita.Programada, cita.Estado); // Verificar que el estado inicial de la cita sea "Programada"
            Assert.AreNotEqual(Guid.Empty, cita.Id); // Verificar que se haya generado un ID único para la cita
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_FechaInicioAnteriorAFechaActual_LanzaExcepcion()
        {
            var intervaloInvalido = new IntervaloDeTiempo(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(1)); // Crear un intervalo de tiempo inválido (inicio en el pasado)
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, intervaloInvalido); // Intentar crear una nueva cita con el intervalo inválido
        }

        [TestMethod]
        public void Cancelar_CitaProgramada_CambiaEstadoACancelada()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Cancelar();
            Assert.AreEqual(EstadoCita.Cancelada, cita.Estado);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Cancelar_CitaNoProgramada_LanzaExcepcion()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Cancelar();
            cita.Cancelar();
        }

        [TestMethod]
        public void Completar_CitaProgramada_CambiaEstadoACompletada()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Completar();
            Assert.AreEqual(EstadoCita.Completada, cita.Estado);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Completar_CitaCancelada_LanzaExcepcion()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Cancelar();
            cita.Completar();
        }
    }
}
