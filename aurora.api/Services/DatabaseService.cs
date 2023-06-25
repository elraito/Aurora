using System.Data;
using Dapper;

namespace Aurora.Api.Services;

public class DatabaseService
{
    private readonly IDbConnection _connection;

    public DatabaseService(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task InitDatabase()
    {
        await _createUsersTable();
    }

    private async Task _createUsersTable()
    {
        var sql = """
        CREATE TABLE IF NOT EXISTS 
        Users (
            Id SERIAL PRIMARY KEY,
            Username VARCHAR,
            Role INTEGER,
            PasswordHash BYTEA,
            PasswordSalt BYTEA
        );
        """;

        await _connection.ExecuteAsync(sql);
    }
}