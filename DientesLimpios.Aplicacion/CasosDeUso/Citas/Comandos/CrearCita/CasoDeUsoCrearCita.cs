using DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerDetalleCita;
using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CrearCita
{
    public class CasoDeUsoCrearCita : IRequestHandler<ComandoCrearCita, Guid>
    {
        private readonly IRepositorioCitas repositorio;
        private readonly IUnidadDeTrabajo unidadDeTrabajo;
        private readonly IServicioNotificaciones servicioNotificaciones;

        public CasoDeUsoCrearCita(IRepositorioCitas repositorio,
            IUnidadDeTrabajo unidadDeTrabajo, IServicioNotificaciones servicioNotificaciones)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
            this.servicioNotificaciones = servicioNotificaciones;
        }

        public async Task<Guid> Handle(ComandoCrearCita request)
        {
            var citaSeSolapa = await repositorio.ExisteCitaSolapada(request.DentistaId, request.FechaInicio, request.FechaFin);
            if (citaSeSolapa)
            {
                throw new ExcepcionDeValidacion("El dentista ya tiene una cita en el horario solicitado.");
            }

            var intervaloDeTiempo = new IntervaloDeTiempo(request.FechaInicio, request.FechaFin);
            var cita = new Cita(request.PacienteId, request.DentistaId, request.ConsultorioId, intervaloDeTiempo);

            Guid? id = null;

            try
            {
                var respuesta = await repositorio.Agregar(cita); // Agregar la cita al repositorio
                await unidadDeTrabajo.Persistir(); // Commit de la transacción
                id = respuesta.Id; // Retornar el ID de la cita creada
            }
            catch (Exception)
            {
                await unidadDeTrabajo.Reversar(); // Rollback en caso de error
                throw;
            }

            var citaDB = await repositorio.ObtenerPorId(id.Value);
            var notificacionDTO = citaDB!.ADto();
            await servicioNotificaciones.EnviarConfirmacionCita(notificacionDTO);
            return id.Value;
        }
    }
}
