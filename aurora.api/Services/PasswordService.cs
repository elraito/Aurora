using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Aurora.Api.Services;

public interface IPasswordService
{
    byte[] CreateSalt();
    byte[] CreateHash(string password, byte[] salt);
    bool ValidatePassword(string password, byte[] salt, byte[] hash);
}

public class PasswordService : IPasswordService
{
    public byte[] CreateSalt()
    {
        byte[] salt = new byte[128 / 8];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(salt);
        return salt;
    }

    public byte[] CreateHash(string password, byte[] salt) =>
     KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 30000,
            numBytesRequested: 256 / 8
        );

    public bool ValidatePassword(string password, byte[] salt, byte[] hash) =>
        CreateHash(password, salt).SequenceEqual(hash);
}