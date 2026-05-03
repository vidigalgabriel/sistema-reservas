using MongoDB.Driver;
using SistemaReservas.Api.Models;

namespace SistemaReservas.Api.Services;

public class AgendamentosService
{
    private readonly IMongoCollection<Agendamento> _agendamentosCollection;

    public AgendamentosService(IConfiguration config)
    {
        var mongoClient = new MongoClient(config.GetSection("MongoDbSettings:ConnectionString").Value);
        var mongoDatabase = mongoClient.GetDatabase(config.GetSection("MongoDbSettings:DatabaseName").Value);
        _agendamentosCollection = mongoDatabase.GetCollection<Agendamento>("Agendamentos");
    }

    public async Task<List<Agendamento>> GetAsync() => 
        await _agendamentosCollection.Find(_ => true).ToListAsync();

    public async Task<Agendamento?> GetAsync(string id) =>
        await _agendamentosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Agendamento novo) => 
        await _agendamentosCollection.InsertOneAsync(novo);

    public async Task UpdateAsync(string id, Agendamento atualizado) =>
        await _agendamentosCollection.ReplaceOneAsync(x => x.Id == id, atualizado);

    public async Task RemoveAsync(string id) =>
        await _agendamentosCollection.DeleteOneAsync(x => x.Id == id);

    public async Task<List<Agendamento>> GetByResourceAsync(string resourceId) =>
        await _agendamentosCollection.Find(x => x.ResourceId == resourceId).ToListAsync();
}