using MongoDB.Driver;
using System.Text.Json;
using WorkerService1.Models;

namespace WorkerService1.DataAccess;

public static class DataServices
{
    public static async Task<IEnumerable<PacienteDTO>> ConsultarDBCitasNotificarAsync()
    {
        var client = new MongoClient(Global.CONNECTION_STRING);
        var database = client.GetDatabase(Global.DATA_BASE_NAME);
        var collection = database.GetCollection<PacienteDTO>(Global.COLLECTION_NAME);

        var result = await collection.Find(c => c.fecha >= DateTime.Now && c.fecha <= DateTime.Now.AddDays(1)).ToListAsync();
        return result;
    }

    public static async Task<IEnumerable<PacienteDTO>> ConsultarApiCitasNotificarAsync()
    {
        var client = new MongoClient(Global.CONNECTION_STRING);
        var database = client.GetDatabase(Global.DATA_BASE_NAME);
        var collection = database.GetCollection<PacienteDTO>(Global.COLLECTION_NAME);

        string jsonData = await File.ReadAllTextAsync("datos.json");
        IEnumerable<PacienteDTO> datosArchivo = JsonSerializer.Deserialize<IEnumerable<PacienteDTO>>(jsonData);

        // Guardar los datos en MongoDB
        if (datosArchivo.Any())
        {
            await collection.InsertManyAsync(datosArchivo);
            Console.WriteLine("Datos guardados en la base de datos.");
            var result = await collection.Find(c => c.fecha >= DateTime.Now && c.fecha <= DateTime.Now.AddDays(1)).ToListAsync();
            return result;
        }
        else
        {
            Console.WriteLine("No se encontraron datos en la api con la fecha de hoy");
            return new List<PacienteDTO>();
        }
    }

    public static async Task UpdateAsync(string id, PacienteDTO updatePaciente)
    {
        var client = new MongoClient(Global.CONNECTION_STRING);
        var database = client.GetDatabase(Global.DATA_BASE_NAME);
        var collection = database.GetCollection<PacienteDTO>(Global.COLLECTION_NAME);

        await collection.ReplaceOneAsync(x => x.paciente == id, updatePaciente);
    }
}
