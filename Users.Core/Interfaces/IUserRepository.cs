using Users.Core.Entities;

namespace Users.Core.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<long> AddAsync(User user);
    Task UpdateAsync(User user);
    Task<int> DeleteAsync(int id);
}
