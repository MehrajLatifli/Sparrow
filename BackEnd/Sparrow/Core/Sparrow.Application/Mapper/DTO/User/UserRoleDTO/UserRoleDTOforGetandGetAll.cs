namespace Sparrow.Application.Mapper.DTO.User.UserRoleDTO
{
    public class UserRoleDTOforGetandGetAll
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
