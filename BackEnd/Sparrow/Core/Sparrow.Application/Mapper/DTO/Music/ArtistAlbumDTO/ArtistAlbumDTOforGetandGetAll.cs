using Sparrow.Application.Mapper.DTO.Music.AlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.ArtistDTO;

namespace Sparrow.Application.Mapper.DTO.Music.ArtistAlbumDTO
{
    public class ArtistAlbumDTOforGetandGetAll
    {
  
        public Guid Id { get; set; }

        
        //public Guid ArtistId_forArtistAlbum { get; set; }

     
        //public Guid AlbumId_forArtistAlbum { get; set; }

        public ArtistDTOforGetandGetAll Artist { get; set; }
        public AlbumDTOforGetandGetAll Album { get; set; } 
    }
}
