using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Consultorio
    {
        public Guid Id { get; private set; } // Identificador único del consultorio
        public string Nombre { get; private set; } = null!; // Nombre del consultorio

        public Consultorio(string nombre) // Constructor que recibe el nombre del consultorio y valida que no sea nulo o vacío.
        {
            AplicarReglasDeNegocioNombre(nombre); // Aplica las reglas de negocio para el nombre.

            Nombre = nombre; // Asigna el nombre del consultorio.
            Id = Guid.CreateVersion7(); // Genera un nuevo identificador único para el consultorio con lógica secuencial para evitar fragmentación en la BBDD.
        }

        public void ActualizarNombre(string nombre) 
        {
            AplicarReglasDeNegocioNombre(nombre); // Aplica las reglas de negocio para el nombre.

            Nombre = nombre; // Actualiza el nombre del consultorio.
        }

        private void AplicarReglasDeNegocioNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio."); // Lanza una excepción personalizada si el nombre es nulo o vacío.
            }
        }
    }
}
