namespace Sparrow.Application.Mapper.DTO.User.RolePermissionDTO
{
    public class RolePermissionDTOforGetandGetAll
    {
        public Guid Id { get; set; }

        public string Method { get; set; }

        public string MethodDescription { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
