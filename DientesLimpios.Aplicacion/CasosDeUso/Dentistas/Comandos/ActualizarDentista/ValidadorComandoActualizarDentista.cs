using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.ActualizarDentista
{
    public class ValidadorComandoActualizarDentista : AbstractValidator<ComandoActualizarDentista>
    {
        public ValidadorComandoActualizarDentista()
        {
            RuleFor(d => d.Nombre)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(250).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}");

            RuleFor(d => d.Email)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(250).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}")
                .EmailAddress().WithMessage("El campo {PropertyName} debe ser un correo electrónico válido");
        }
    }
}
