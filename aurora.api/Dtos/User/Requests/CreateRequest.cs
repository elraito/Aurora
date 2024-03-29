using System.ComponentModel.DataAnnotations;
using Aurora.Api.Entities;

namespace Aurora.Api.Dtos.User.Requests
{
    public class CreateRequest
    {
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = default!;

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = default!;
    }
}