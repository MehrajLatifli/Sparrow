using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sparrow.Application.Mapper.DTO.Music.ArtistAlbumDTO
{
    public class ArtistAlbumDTOforCreate
    {
        [Required(ErrorMessage = "ArtistId  is required")]
        public Guid ArtistId_forArtistAlbum { get; set; }

        [Required(ErrorMessage = "AlbumId  is required")]
        public Guid AlbumId_forArtistAlbum { get; set; }
    }
}
