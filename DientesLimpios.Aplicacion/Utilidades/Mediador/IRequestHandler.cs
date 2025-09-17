using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    public interface IRequestHandler<TRequest, TResponse>  // Con respuesta
        where TRequest: IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request); // Con respuesta
    }

    public interface IRequestHandler<TRequest> // Sin respuesta
        where TRequest : IRequest
    {
        Task Handle(TRequest request); // Sin respuesta
    }
}
