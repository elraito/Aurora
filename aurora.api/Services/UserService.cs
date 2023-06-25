using Aurora.Api.Dtos.User.Requests;
using Aurora.Api.Entities;
using Aurora.Api.Repositories;

namespace Aurora.Api.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetById(int id);
    Task<User?> Create(CreateRequest dto);
    Task<User?> Update(int id, UpdateRequest dto);
    Task<bool> Delete(int id);
}

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private IPasswordService _passwordService;

    public UserService(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<IEnumerable<User>> GetAll() =>
        await _userRepository.GetAll();

    public async Task<User?> GetById(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is null) throw new Exception("User not found");
        return user;
    }

    public async Task<User?> Create(CreateRequest dto)
    {
        var isExistsingUser = await _userRepository.GetByUsername(dto.Username!);

        if (isExistsingUser is not null) throw new AppException("User already exists");

        byte[] passwordSalt = _passwordService.CreateSalt();
        byte[] passwordHash = _passwordService.CreateHash(dto.Password!, passwordSalt);

        var user = dto.MapToEntity();

        user.PasswordSalt = passwordSalt;
        user.PasswordHash = passwordHash;

        return await _userRepository.Create(user);
    }

    public async Task<User?> Update(int id, UpdateRequest dto)
    {
        var user = await _userRepository.GetById(id);

        if (user is null) throw new Exception("User not found");

        if (!string.IsNullOrEmpty(dto.Password))
        {
            byte[] passwordSalt = _passwordService.CreateSalt();
            byte[] passwordHash = _passwordService.CreateHash(dto.Password!, passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
        }

        user.Username = dto.Username;
        user.Role = dto.Role;

        return await _userRepository.Update(user);
    }

    public async Task<bool> Delete(int id) =>
        await _userRepository.Delete(id);
}