using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SistemaReservas.Api.Models;

namespace SistemaReservas.Api.Services;

public class RecursosService
{
    private readonly IMongoCollection<Recurso> _recursosCollection;

    public RecursosService(IConfiguration config)
    {
     
        var mongoClient = new MongoClient(config.GetSection("MongoDbSettings:ConnectionString").Value);
        var mongoDatabase = mongoClient.GetDatabase(config.GetSection("MongoDbSettings:DatabaseName").Value);

        _recursosCollection = mongoDatabase.GetCollection<Recurso>("Recursos");
    }

    public async Task<List<Recurso>> GetAsync() =>
        await _recursosCollection.Find(_ => true).ToListAsync();

    public async Task<Recurso?> GetAsync(string id) =>
        await _recursosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Recurso novoRecurso) =>
        await _recursosCollection.InsertOneAsync(novoRecurso);

    public async Task UpdateAsync(string id, Recurso recursoAtualizado) =>
        await _recursosCollection.ReplaceOneAsync(x => x.Id == id, recursoAtualizado);

    public async Task RemoveAsync(string id) =>
        await _recursosCollection.DeleteOneAsync(x => x.Id == id);
}