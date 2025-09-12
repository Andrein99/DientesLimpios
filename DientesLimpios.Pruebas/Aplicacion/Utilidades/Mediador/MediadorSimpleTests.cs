using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using FluentValidation;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.Utilidades.Mediador
{
    [TestClass]
    public class MediadorSimpleTests
    {
        public class RequestFalso : IRequest<string> {
            public required string Nombre { get; set; }
        };

        public class HandlerFalso : IRequestHandler<RequestFalso, string> // Handler para RequestFalso
        {
            public Task<string> Handle(RequestFalso request)
            {
                return Task.FromResult("Respuesta correcta");
            }
        }

        public class ValidadorRequestFalso : AbstractValidator<RequestFalso> // Validador para RequestFalso
        {
            public ValidadorRequestFalso()
            {
                RuleFor(x => x.Nombre).NotEmpty(); // La propiedad Nombre no debe estar vacía
            }
        }

        [TestMethod]
        public async Task Send_LlamaMetodoHandler()
        {
            var request = new RequestFalso() { Nombre = "Nombre A" }; // Crear una instancia de la solicitud

            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>(); // Simular el caso de uso

            var serviceProvider = Substitute.For<IServiceProvider>(); // Simular el proveedor de servicios
            serviceProvider
                .GetService(typeof(IRequestHandler<RequestFalso, string>))
                .Returns(casoDeUsoMock); // Configurar el proveedor de servicios para devolver el caso de uso simulado

            var mediador = new MediadorSimple(serviceProvider); // Crear una instancia del mediador con el proveedor de servicios simulado

            var resultado = await mediador.Send(request); // Enviar la solicitud a través del mediador

            await casoDeUsoMock.Received(1).Handle(request); // Verificar que el método Handle del caso de uso fue llamado una vez con la solicitud

        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeMediador))]
        public async Task Send_SinHandlerRegistrado_LanzaExcepcioin()
        {
            var request = new RequestFalso() { Nombre = "Nombre A" }; // Crear una instancia de la solicitud

            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>(); // Simular el caso de uso

            var serviceProvider = Substitute.For<IServiceProvider>(); // Simular el proveedor de servicios
       
            var mediador = new MediadorSimple(serviceProvider); // Crear una instancia del mediador con el proveedor de servicios simulado

            var resultado = await mediador.Send(request); // Enviar la solicitud a través del mediador
        }

        [TestMethod]
        public async Task Send_ComandoNoValido_LanzaExcepcion()
        {
            var request = new RequestFalso() { Nombre = "" }; // Crear una instancia de la solicitud con datos no válidos
            var validador = new ValidadorRequestFalso(); // Crear una instancia del validador
            var serviceProvider = Substitute.For<IServiceProvider>(); // Simular el proveedor de servicios
            
            serviceProvider
                .GetService(typeof(IValidator<RequestFalso>))
                .Returns(validador); // Configurar el proveedor de servicios para devolver el validador
            
            var mediador = new MediadorSimple(serviceProvider); // Crear una instancia del mediador con el proveedor de servicios simulado
            
            await Assert.ThrowsExceptionAsync<ExcepcionDeValidacion>(async () =>
                await mediador.Send(request)); // Verificar que se lanza una ExcepcionDeValidacion al enviar la solicitud no válida
        }
    }
}
