using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sparrow.Application.Mapper.DTO.User.RolePermissionDTO
{
    public class RolePermissionDTOforCreate
    {

        [Required(ErrorMessage = "Method  is required")]
        public string Method { get; set; }

        [Required(ErrorMessage = "Method Description  is required")]
        public string MethodDescription { get; set; }

    }
}
