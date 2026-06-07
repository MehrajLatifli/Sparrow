using System.ComponentModel.DataAnnotations;

namespace Sparrow.Application.Mapper.DTO.Music.PlaylistUserDTO
{
    public class PlaylistUserDTOforUpdate
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId_forPlaylistUser { get; set; }

        [Required(ErrorMessage = "PlaylistId is required")]
        public Guid PlaylistId_forPlaylistUser { get; set; }
    }
}
