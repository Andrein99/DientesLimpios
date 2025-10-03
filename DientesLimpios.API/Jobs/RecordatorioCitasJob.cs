
using DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.EnviarRecordatorioCitas;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.API.Jobs
{
    public class RecordatorioCitasJob : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly TimeZoneInfo zonaHorariaCO = TimeZoneInfo.FindSystemTimeZoneById("America/Bogota");

        public RecordatorioCitasJob(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) // Loop infinito hasta que se cancele el token
            {
                var ahora = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaHorariaCO); // Hora actual en Colombia

                if (ahora.Hour == 8) // Si son las 8 AM
                {
                    using var scope = scopeFactory.CreateScope(); // Crear un nuevo scope para obtener servicios con alcance
                    var mediador = scope.ServiceProvider.GetRequiredService<IMediator>(); // Obtener el mediador desde el scope
                    await mediador.Send(new ComandoEnviarRecordatorioCitas()); // Enviar el comando para enviar recordatorios de citas
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Esperar una hora antes de la siguiente iteración
            }
        }
    }
}
