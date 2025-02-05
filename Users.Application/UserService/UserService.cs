using AutoMapper;
using Users.Core.Dto;
using Users.Core.Entities;
using Users.Core.Interfaces;

namespace Users.Application.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<long> AddAsync(UserDto userDto)
    {
        var userEntity = _mapper.Map<User>(userDto);
        return await _userRepository.AddAsync(userEntity);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

        return usersDto;
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task UpdateAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _userRepository.UpdateAsync(user);
    }
}
