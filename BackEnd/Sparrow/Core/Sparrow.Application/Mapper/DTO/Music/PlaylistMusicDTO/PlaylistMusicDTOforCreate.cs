using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sparrow.Application.Mapper.DTO.Music.PlaylistMusicDTO
{
    public class PlaylistMusicDTOforCreate
    {
        [Required(ErrorMessage = "PlaylistId is required")]
        public Guid PlaylistId_forPlaylistMusic { get; set; }

        [Required(ErrorMessage = "MusicId is required")]
        public Guid MusicId_forPlaylistMusic { get; set; }
    }
}
