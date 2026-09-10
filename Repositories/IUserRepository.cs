using UserCrud.Models;

namespace UserCrud.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<bool> EmailExistsAsync(string email, int? excludingId = null);
    Task AddAsync(User user);
    void Update(User user);
    void Remove(User user);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}
