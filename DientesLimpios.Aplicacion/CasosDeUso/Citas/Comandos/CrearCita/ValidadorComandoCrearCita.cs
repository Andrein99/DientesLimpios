using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CrearCita
{
    public class ValidadorComandoCrearCita : AbstractValidator<ComandoCrearCita>
    {
        public ValidadorComandoCrearCita()
        {
            RuleFor(c => c.FechaInicio)
                .GreaterThan(c => c.FechaFin).WithMessage("La fecha de fin debe ser posterior a la fecha de inicio")
                .GreaterThan(DateTime.UtcNow).WithMessage("La fecha de inicio no puede estar en el pasado");
            
        }
    }
}
