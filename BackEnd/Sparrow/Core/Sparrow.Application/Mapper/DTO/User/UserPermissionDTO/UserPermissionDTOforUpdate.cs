using System.ComponentModel.DataAnnotations;

namespace Sparrow.Application.Mapper.DTO.User.UserPermissionDTO
{
    public class UserPermissionDTOforUpdate
    {
        [Required(ErrorMessage = "Id  is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User Access  is required")]
        public string UserAccess { get; set; }

        [Required(ErrorMessage = "User Access Description  is required")]
        public string UserAccessDescription { get; set; }
    }
}
