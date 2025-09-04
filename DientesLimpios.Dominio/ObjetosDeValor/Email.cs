using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.ObjetosDeValor
{
    public record Email // Objeto de valor para representar un correo electrónico. Se usa 'record' para que sea inmutable.
    {
        public string Valor { get; } = null!; // Propiedad que almacena el valor del correo electrónico.

        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) // Validación básica para asegurar que el correo electrónico no esté vacío.
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio.");
            }
            if (!email.Contains("@") || !email.Contains(".")) // Validación básica del formato del correo electrónico.
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} no es válido.");
            }

            Valor = email; // Asigna el valor del correo electrónico.
        }
    }
}
