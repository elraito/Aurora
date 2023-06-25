using System.Data;
using Aurora.Api.Entities;
using Dapper;

namespace Aurora.Api.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetById(int id);
    Task<User?> GetByUsername(string username);
    Task<User?> Create(User user);
    Task<User?> Update(User user);
    Task<bool> Delete(int id);
}

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<User>> GetAll() =>
        await _dbConnection.QueryAsync<User>("SELECT * FROM users");

    public async Task<User?> GetById(int id)
    {
        var sql = "SELECT * FROM users WHERE Id = @id";
        return await _dbConnection.QuerySingleOrDefaultAsync<User>(sql, new { id });
    }

    public async Task<User?> GetByUsername(string username)
    {
        var sql = "SELECT * FROM users WHERE Username = @username";
        return await _dbConnection.QuerySingleOrDefaultAsync<User>(sql, new { username });
    }

    public async Task<User?> Create(User user)
    {
        var sql = """
            INSERT INTO Users (Username, Role, PasswordHash, PasswordSalt)
            VALUES (@Username, @Role, @PasswordHash, @PasswordSalt)
            RETURNING *
            """;

        return await _dbConnection.QuerySingleOrDefaultAsync<User>(sql, user);
    }

    public async Task<User?> Update(User user)
    {
        var sql = """
            UPDATE Users
            SET Username = @Username,
            Role = @Role,
            PasswordHash = @PasswordHash,
            PasswordSalt = @PasswordSalt
            WHERE Id = @Id
            RETURNING *
            """;

        return await _dbConnection.QuerySingleOrDefaultAsync<User>(sql, user);
    }

    public async Task<bool> Delete(int id)
    {
        var sql = """
        DELETE FROM users
        WHERE Id = @id
        RETURNING true AS success
        """;

        return await _dbConnection.ExecuteScalarAsync<bool>(sql, new { id });
    }
}