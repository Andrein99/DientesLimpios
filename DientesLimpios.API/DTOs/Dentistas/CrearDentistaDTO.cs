using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs.Dentistas
{
    public class CrearDentistaDTO
    {
        [Required]
        [StringLength(250)]
        public required string Nombre { get; set; } // Nombre del dentista
        [Required]
        [EmailAddress]
        [StringLength(254)]
        public required string Email { get; set; } // Email del dentista
    }
}
