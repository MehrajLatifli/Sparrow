using System.ComponentModel.DataAnnotations;

namespace Sparrow.Application.Mapper.DTO.Music.MusicAlbumDTO
{
    public class MusicAlbumDTOforUpdate
    {
        [Required(ErrorMessage = "Id  is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "MusicId  is required")]
        public Guid MusicId_forMusicAlbum { get; set; }

        [Required(ErrorMessage = "AlbumId  is required")]
        public Guid AlbumId_forMusicAlbum { get; set; }
    }


}
