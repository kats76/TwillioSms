namespace WorkerService1.Models;
public class PacienteDTO
{
    public string? paciente { get; set; }
    public DateTime fecha { get; set; }
    public string? hora { get; set; }
    public string? motivo { get; set; }
    public string? celular { get; set; }
    public string? medico { get; set; }
    public int? intentos { get; set; }
    public string? respuesta { get; set; }
}

