using System.ComponentModel.DataAnnotations;

namespace Sparrow.Application.Mapper.DTO.Music.PlaylistMusicDTO
{
    public class PlaylistMusicDTOforUpdate
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "PlaylistId is required")]
        public Guid PlaylistId_forPlaylistMusic { get; set; }

        [Required(ErrorMessage = "MusicId is required")]
        public Guid MusicId_forPlaylistMusic { get; set; }
    }
}
