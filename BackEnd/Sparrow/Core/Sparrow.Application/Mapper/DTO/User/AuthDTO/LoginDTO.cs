using System.ComponentModel.DataAnnotations;

namespace Sparrow.Application.Mapper.DTO.User.AuthDTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "User Password is required")]
        public string Password { get; set; }
    }
}

