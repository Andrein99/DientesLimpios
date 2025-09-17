using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class ValidadorComandoCrearConsultorio: AbstractValidator<ComandoCrearConsultorio> // Hereda de AbstractValidator<T> donde T es el comando a validar
    {
        public ValidadorComandoCrearConsultorio()
        {
            RuleFor(p => p.Nombre) // Define una regla para la propiedad Nombre
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido.") // {PropertyName} se reemplaza por el nombre de la propiedad
                .MaximumLength(150).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}"); // {MaxLength} se reemplaza por el valor máximo definido;
        }
    }
}
