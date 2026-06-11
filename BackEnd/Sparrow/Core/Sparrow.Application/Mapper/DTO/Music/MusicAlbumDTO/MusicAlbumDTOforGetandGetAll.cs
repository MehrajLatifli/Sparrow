using Sparrow.Application.Mapper.DTO.Music.AlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.MusicDTO;
using Sparrow.Domain.Entities.Models;

namespace Sparrow.Application.Mapper.DTO.Music.MusicAlbumDTO
{
    public class MusicAlbumDTOforGetandGetAll
    {

        public Guid Id { get; set; }
        //public Guid MusicId_forMusicAlbum { get; set; }
        //public Guid AlbumId_forMusicAlbum { get; set; }

        public MusicDTOforGetandGetAll Music { get; set; }

        public AlbumDTOforGetandGetAll Album { get; set; }
    }


}
