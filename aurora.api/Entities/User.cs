using System.Text.Json.Serialization;

namespace Aurora.Api.Entities;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public Role Role { get; set; }

    [JsonIgnore]
    public byte[]? PasswordHash { get; set; }
    [JsonIgnore]
    public byte[]? PasswordSalt { get; set; }
}