using Sparrow.Application.Mapper.DTO.Music.MusicDTO;

namespace Sparrow.Application.Mapper.DTO.Music.PlaylistMusicDTO
{
    public class PlaylistMusicDTOforGetandGetAll
    {
       
        public Guid Id { get; set; }


        //public Guid PlaylistId_forPlaylistMusic { get; set; }


        //public Guid MusicId_forPlaylistMusic { get; set; }

        public Guid PlaylistId { get; set; }
        public string PlaylistName { get; set; }
        public string PlaylistDescription { get; set; }
        public string PlaylistImage { get; set; }

        public List<MusicDTOforGetandGetAll> Musics { get; set; }
    }
}
