using MongoDB.Driver;
using Vyracare.Api.Client.Models;

namespace Vyracare.Api.Client.Services;

public class PatientService
{
    private readonly IMongoCollection<PatientModel> _collection;

    public PatientService(IMongoDatabase db)
    {
        _collection = db.GetCollection<PatientModel>("patients");
    }

    public async Task<List<PatientModel>> GetAllAsync()
    {
        return await _collection.Find(Builders<PatientModel>.Filter.Empty).ToListAsync();
    }

    public async Task<PatientModel?> GetByIdAsync(string id)
    {
        return await _collection.Find(item => item.Id == id).FirstOrDefaultAsync();
    }

    public async Task<PatientModel?> GetByCpfAsync(string cpf)
    {
        return await _collection.Find(item => item.Cpf == cpf).FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsByCpfAsync(string cpf)
    {
        return await _collection.Find(item => item.Cpf == cpf).AnyAsync();
    }

    public async Task CreateAsync(PatientModel item)
    {
        item.CreatedAt = DateTime.UtcNow;
        item.UpdatedAt = item.CreatedAt;
        await _collection.InsertOneAsync(item);
    }
}
