using Sparrow.Application.Mapper.DTO.User.RoleDTO;
using Sparrow.Application.Mapper.DTO.User.RolePermissionDTO;
using Sparrow.Application.Mapper.DTO.User.UserPermissionDTO;

namespace Sparrow.Application.Mapper.DTO.User.AuthDTO
{
    public class PermissionDTO
    {

        public List<UserPermissionDTOforGetandGetAll> UserPermissions { get; set; }

        public List<RolePermissionDTOforGetandGetAll> RolePermissions { get; set; }

        public List<RoleDTOforGetandGetAll> Roles { get; set; }




    }
}

