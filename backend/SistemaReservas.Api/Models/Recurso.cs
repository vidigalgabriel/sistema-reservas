using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SistemaReservas.Api.Models;

public class Recurso
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Nome")]
    public string Nome { get; set; } = null!;

    public string Especialidade { get; set; } = null!;

    public bool Ativo { get; set; } = true;
}