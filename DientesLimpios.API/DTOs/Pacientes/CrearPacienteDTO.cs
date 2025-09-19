using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs.Pacientes
{
    public class CrearPacienteDTO
    {
        [Required]
        [StringLength(250)]
        public required string Nombre { get; set; } // Nombre del paciente
        [Required]
        [EmailAddress]
        [StringLength(254)]
        public required string Email { get; set; } // Email del paciente
    }
}
