using sms_test;
using WorkerService1.DataAccess;
using WorkerService1.Models;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                IEnumerable<PacienteDTO> result = await DataServices.ConsultarDBCitasNotificarAsync();

                if (!result.Any())
                    result = await DataServices.ConsultarApiCitasNotificarAsync();


                foreach (PacienteDTO paciente in result)
                {
                    if (paciente.intentos <= 3)
                    {
                        string mensaje = $"Datos {paciente.paciente} Fecha {paciente.fecha.ToString("dd-MM-yyyy")} hora {paciente.hora}";
                        (string, string) sid = SendSMS.SendMessageTwillo(mensaje, paciente);
                        if (sid.Item1 == "true")
                        {
                            paciente.intentos += 1;
                            paciente.respuesta = sid.Item2;

                            // Actualizar el número de intentos en la base de datos
                            DataServices.UpdateAsync(paciente.paciente, paciente);
                            _logger.LogInformation($"Estado del mensaje SID {sid.Item2}");

                        }
                        else
                        {
                            _logger.LogError(sid.Item1);
                        }

                    }
                }

                Thread.Sleep(Global.INTERVALO_ENVIO * 3600000);
            }
        }
    }
}