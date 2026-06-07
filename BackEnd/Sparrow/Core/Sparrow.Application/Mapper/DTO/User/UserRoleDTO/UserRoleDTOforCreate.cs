using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sparrow.Application.Mapper.DTO.User.UserRoleDTO
{
    public class UserRoleDTOforCreate
    {

        [Required(ErrorMessage = "UserId  is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "RoleId  is required")]
        public Guid RoleId { get; set; }

        [Required(ErrorMessage = "Created Date  is required")]
        public DateTime? CreatedDate { get; set; }

    }
}
