using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Cita
    {
        public Guid Id { get; private set; } // Identificador único de la cita
        public Guid PacienteId { get; private set; } // Identificador del paciente asociado a la cita
        public Guid DentistaId { get; private set; } // Identificador del dentista asociado a la cita
        public Guid ConsultorioId { get; private set; } // Identificador del consultorio donde se realizará la cita
        public EstadoCita Estado { get; private set; } // Estado actual de la cita (programada, completada, cancelada, etc.)
        public IntervaloDeTiempo IntervaloDeTiempo { get; private set; } // Intervalo de tiempo de la cita
        public Paciente? Paciente { get; private set; } // Información del paciente asociado a la cita
        public Dentista? Dentista { get; private set; } // Información del dentista asociado a la cita
        public Consultorio? Consultorio { get; private set; } // Información del consultorio donde se realizará la cita

        public Cita(Guid pacienteId, Guid dentistaId, Guid consultorioId, IntervaloDeTiempo intervaloDeTiempo)
        {
            if (intervaloDeTiempo.Inicio < DateTime.UtcNow) // Validar que la fecha de inicio no sea anterior a la fecha actual
            {
                throw new ExcepcionDeReglaDeNegocio($"La fecha de inicio no puede ser anterior a la fecha actual.");
            }

            PacienteId = pacienteId; // Asignar el identificador del paciente
            DentistaId = dentistaId; // Asignar el identificador del dentista
            ConsultorioId = consultorioId; // Asignar el identificador del consultorio
            IntervaloDeTiempo = intervaloDeTiempo; // Asignar el intervalo de tiempo de la cita
            Estado = EstadoCita.Programada; // Inicializar el estado de la cita como "Programada"
            Id = Guid.CreateVersion7(); // Generar un nuevo identificador único para la cita
        }

        public void Cancelar() // Método para cancelar la cita
        {
            if (Estado != EstadoCita.Programada) // Validar que la cita esté en estado "Programada" antes de permitir la cancelación
            {
                throw new ExcepcionDeReglaDeNegocio("Sólo se pueden cancelar citas que estén programadas.");
            }

            Estado = EstadoCita.Cancelada; // Cambiar el estado de la cita a "Cancelada"
        }

        public void Completada() // Método para completar la cita
        {
            if (Estado != EstadoCita.Programada) // Validar que la cita esté en estado "Programada" antes de permitir la cancelación
            {
                throw new ExcepcionDeReglaDeNegocio("Sólo se pueden completar citas que estén programadas."); 
            }

            Estado = EstadoCita.Completada; // Cambiar el estado de la cita a "Completada"
        }
    }
}
