using System.ComponentModel.DataAnnotations;

namespace Sparrow.Application.Mapper.DTO.User.AuthDTO
{
    public class UpdateUserBlockStatusDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User IsBlcok is required")]
        public bool? IsBlcok { get; set; }
    }
}

