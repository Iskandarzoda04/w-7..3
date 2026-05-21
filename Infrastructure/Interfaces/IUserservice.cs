using Infrastructure.DTOs.User;

namespace Infrastructure.Interfaces;

public interface IUserservice
{
      Task<List<UserDto>> GetAllAsync();

    Task<UserDto?> GetByIdAsync(int id);

    Task<int> CreateAsync(CreateUserDto dto);

    Task<bool> UpdateAsync(int id, UpdateUserDto dto);

    Task<bool> DeleteAsync(int id);
}
