using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sparrow.Application.Mapper.DTO.User.UserPermissionDTO
{
    public class UserPermissionDTOforCreate
    {
        [Required(ErrorMessage = "Id  is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User Access  is required")]
        public string UserAccess { get; set; }

        [Required(ErrorMessage = "User Access Description  is required")]
        public string UserAccessDescription { get; set; }
    }
}
