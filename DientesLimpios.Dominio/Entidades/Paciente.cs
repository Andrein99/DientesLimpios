using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Paciente
    {
        public Guid Id { get; private set; } // Identificador único del paciente
        public string Nombre { get; private set; } = null!; // Nombre completo del paciente
        public Email Email { get; private set; } = null!; // Dirección de correo electrónico del paciente

        public Paciente(string nombre, Email email) // Constructor que recibe el nombre y el correo electrónico del paciente.
        {
            if (string.IsNullOrWhiteSpace(nombre)) // Validación básica para asegurar que el nombre no esté vacío.
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio.");
            }

            if (email is null)
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio.");
            }

            Id = Guid.CreateVersion7(); // Genera un nuevo identificador único para el paciente con lógica secuencial para evitar fragmentación en la BBDD.
            Nombre = nombre; // Asigna el nombre del paciente.
            Email = email; // Asigna el correo electrónico del paciente.
        }
    }
}
