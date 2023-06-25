using System.ComponentModel.DataAnnotations;
using Aurora.Api.Entities;

namespace Aurora.Api.Dtos.User.Requests
{
    public class UpdateRequest
    {
        public string? Username { get; set; }

        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }

        private string? _password;

        [MinLength(6)]
        public string? Password
        {
            get => _password;
            set => _password = replaceEmptyWithNull(value);
        }

        private string? _confirmPassword;
        [Compare("Password")]
        public string? ConfirmPassword
        {
            get => _confirmPassword;
            set => _confirmPassword = replaceEmptyWithNull(value);
        }

        private string? replaceEmptyWithNull(string? value) =>
             string.IsNullOrWhiteSpace(value) ? null : value;
    }
}