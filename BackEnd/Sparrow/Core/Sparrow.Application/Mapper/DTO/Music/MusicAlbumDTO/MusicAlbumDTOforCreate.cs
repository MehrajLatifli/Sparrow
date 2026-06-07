using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sparrow.Application.Mapper.DTO.Music.MusicAlbumDTO
{
    public class MusicAlbumDTOforCreate
    {
        [Required(ErrorMessage = "MusicId  is required")]
        public Guid MusicId_forMusicAlbum { get; set; }
        
        [Required(ErrorMessage = "AlbumId  is required")]
        public Guid AlbumId_forMusicAlbum { get; set; }
    }


}
