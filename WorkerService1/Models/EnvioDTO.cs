namespace WorkerService1.Models;
public class EnvioDTO
{
    public PacienteDTO Paciente { get; set; }
    public string EstadoEnvio { get; set; }
    public int Intento { get; set; }
}

