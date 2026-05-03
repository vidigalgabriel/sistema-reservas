using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SistemaReservas.Api.Models;

public class Agendamento
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string ResourceId { get; set; } = null!; // ID do Barbeiro

    public string ClienteNome { get; set; } = null!;
    
    public DateTime DataHora { get; set; }

    public string Status { get; set; } = "Pendente"; // Pendente, Confirmado, Cancelado
}