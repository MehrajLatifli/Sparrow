using Microsoft.AspNetCore.Http;
using Sparrow.Application.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sparrow.Application.Mapper.DTO.Music.PlaylistDTO
{
    public class PlaylistDTOforCreate
    {
        [Required(ErrorMessage = "PlaylistName is required")]
        public string PlaylistName { get; set; }

        [Required(ErrorMessage = "PlaylistDescription is required")]
        public string PlaylistDescription { get; set; }

        [Required(ErrorMessage = "PlaylistDatetime is required")]
        public DateTime PlaylistDatetime { get; set; }

        [Required(ErrorMessage = "ImagePlaylist is required")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        [FileSize(5, 10)]
        public IFormFile ImagePlaylist { get; set; }
    }
}
