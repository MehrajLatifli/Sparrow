using Microsoft.AspNetCore.Http;
using Sparrow.Application.Mapper.DTO.Music.MusicDTO;
using Sparrow.Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace Sparrow.Application.Mapper.DTO.Music.PlaylistDTO
{
    public class PlaylistDTOforGetandGetAll
    {
        public Guid Id { get; set; }


        public string PlaylistName { get; set; }

        public string PlaylistDescription { get; set; }

        public string PlaylistDatetime { get; set; }

        public string ImagePlaylist { get; set; }

        //public List<MusicDTOforGetandGetAll> Musics { get; set; }

    }
}
