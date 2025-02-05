using Users.Core.Dto;

namespace Users.Core.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(int id);
    Task<long> AddAsync(UserDto userDto);
    Task UpdateAsync(UserDto userDto);
    Task<int> DeleteAsync(int id);
}
