using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sparrow.Application.Mapper.DTO.User.RoleClaimDTO
{
    public class RoleClaimDTOforCreate
    {

        [Required(ErrorMessage = "Role Id is required")]
        public Guid RoleId { get; set; }

        [Required(ErrorMessage = "Role Permission Id is required")]
        public Guid RolePermissionId { get; set; }
    }
}
