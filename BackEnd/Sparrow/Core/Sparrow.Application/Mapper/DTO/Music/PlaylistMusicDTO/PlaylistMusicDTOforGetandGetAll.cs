using Sparrow.Application.Mapper.DTO.Music.MusicDTO;
using Sparrow.Application.Mapper.DTO.Music.PlaylistDTO;

namespace Sparrow.Application.Mapper.DTO.Music.PlaylistMusicDTO
{
    public class PlaylistMusicDTOforGetandGetAll
    {

        public Guid Id { get; set; }


        public PlaylistDTOforGetandGetAll Playlist { get; set; }
        public MusicDTOforGetandGetAll Music { get; set; }
    }
}
