using MongoDB.Driver;
using Vyracare.Api.Client.Models;

namespace Vyracare.Api.Client.Services;

public class ClientService
{
    private readonly IMongoCollection<ClientModel> _collection;

    public ClientService(IMongoDatabase db)
    {
        _collection = db.GetCollection<ClientModel>("client");
    }

    public async Task<List<ClientModel>> GetAllAsync()
    {
        return await _collection.Find(Builders<ClientModel>.Filter.Empty).ToListAsync();
    }

    public async Task<ClientModel?> GetByIdAsync(string id)
    {
        return await _collection.Find(item => item.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(ClientModel item)
    {
        item.CreatedAt = DateTime.UtcNow;
        item.UpdatedAt = item.CreatedAt;
        await _collection.InsertOneAsync(item);
    }
}
