using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sparrow.Application.Mapper.DTO.Music.PlaylistUserDTO
{
    public class PlaylistUserDTOforCreate
    {
        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId_forPlaylistUser { get; set; }

        [Required(ErrorMessage = "PlaylistId is required")]
        public Guid PlaylistId_forPlaylistUser { get; set; }
    }
}
