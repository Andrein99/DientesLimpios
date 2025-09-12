using DientesLimpios.Aplicacion.Excepciones;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    public class MediadorSimple : IMediator
    {
        private readonly IServiceProvider serviceProvider;

        public MediadorSimple(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var tipoValidador = typeof(IValidator<>).MakeGenericType(request.GetType()); // IValidator<TRequest>

            var validador = serviceProvider.GetService(tipoValidador); // Instancia concreta de IValidator<TRequest>
            if (validador is not null)
            {
                var metodoValidar = tipoValidador.GetMethod("ValidateAsync");
                var tareaValidar = (Task)metodoValidar!.Invoke(validador, 
                    new object[] { request, CancellationToken.None })!;

                await tareaValidar.ConfigureAwait(false);

                var resultado = tareaValidar.GetType().GetProperty("Result");
                var validationResult = (ValidationResult)resultado!.GetValue(tareaValidar)!;
            
                if (!validationResult.IsValid)
                {
                    throw new ExcepcionDeValidacion(validationResult);
                };
            }

            var tipoCasoDeUso = typeof(IRequestHandler<,>) 
                .MakeGenericType(request.GetType(), typeof(TResponse)); // IRequestHandler<TRequest, TResponse>

            var casoDeUso = serviceProvider.GetService(tipoCasoDeUso); // Instancia concreta de IRequestHandler<TRequest, TResponse>

            if (casoDeUso is null)
            {
                throw new ExcepcionDeMediador($"No se encontró un handler para {request.GetType().Name}"); // No se encontró un handler para CreateUserCommand
            }

            var metodo = tipoCasoDeUso.GetMethod("Handle")!; // Método Handle de IRequestHandler<TRequest, TResponse>
            return await (Task<TResponse>)metodo.Invoke(casoDeUso, new object[] { request })!; // Invoca al método Handle del handler concreto
        }
    }
}
