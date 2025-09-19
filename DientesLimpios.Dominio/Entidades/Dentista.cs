using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Dentista
    {
        public Guid Id { get; private set; } // Identificador único del dentista
        public string Nombre { get; private set; } = null!; // Nombre del dentista
        public Email Email { get; private set; } = null!; // Correo electrónico del dentista

        private Dentista() // Constructor privado para EF Core
        {
            
        }

        public Dentista(string nombre, Email email) // Constructor que recibe el nombre y correo electrónico del dentista.
        {
            if (string.IsNullOrWhiteSpace(nombre)) // Validación básica para asegurar que el nombre no esté vacío.
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio.");
            }

            if (email is null)
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio.");
            }

            Id = Guid.CreateVersion7(); // Genera un nuevo identificador único para el dentista con lógica secuencial para evitar fragmentación en la BBDD.
            Nombre = nombre; // Asigna el nombre del dentista.
            Email = email; // Asigna el correo electrónico del dentista.
        }
    }
}
