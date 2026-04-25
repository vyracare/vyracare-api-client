using MongoDB.Driver;
using Vyracare.Api.Client.Models;

namespace Vyracare.Api.Client.Services;

public class EmployeeService
{
    private readonly IMongoCollection<EmployeeModel> _collection;

    public EmployeeService(IMongoDatabase db)
    {
        _collection = db.GetCollection<EmployeeModel>("employees");
    }

    public async Task<List<EmployeeModel>> GetAllAsync()
    {
        return await _collection.Find(Builders<EmployeeModel>.Filter.Empty).ToListAsync();
    }

    public async Task<EmployeeModel?> GetByIdAsync(string id)
    {
        return await _collection.Find(item => item.Id == id).FirstOrDefaultAsync();
    }

    public async Task<EmployeeModel?> GetByEmailAsync(string email)
    {
        return await _collection.Find(item => item.Email == email).FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _collection.Find(item => item.Email == email).AnyAsync();
    }

    public async Task CreateAsync(EmployeeModel item)
    {
        item.CreatedAt = DateTime.UtcNow;
        item.UpdatedAt = item.CreatedAt;
        await _collection.InsertOneAsync(item);
    }
}
