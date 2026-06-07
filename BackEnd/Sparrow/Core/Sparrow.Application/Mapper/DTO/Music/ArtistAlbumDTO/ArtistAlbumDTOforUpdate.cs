using System.ComponentModel.DataAnnotations;

namespace Sparrow.Application.Mapper.DTO.Music.ArtistAlbumDTO
{
    public class ArtistAlbumDTOforUpdate
    {
        [Required(ErrorMessage = "Id  is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ArtistId  is required")]
        public Guid ArtistId_forArtistAlbum { get; set; }

        [Required(ErrorMessage = "AlbumId  is required")]
        public Guid AlbumId_forArtistAlbum { get; set; }
    }
}
