using System.ComponentModel.DataAnnotations;

namespace Sparrow.Application.Mapper.DTO.User.AuthDTO
{
    public class UpdatePasswordDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Old password is required")]
        public string? OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        public string? NewPassword { get; set; }


    }
}

