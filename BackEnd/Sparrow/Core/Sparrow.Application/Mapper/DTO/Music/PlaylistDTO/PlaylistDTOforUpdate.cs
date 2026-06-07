using Microsoft.AspNetCore.Http;
using Sparrow.Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace Sparrow.Application.Mapper.DTO.Music.PlaylistDTO
{
    public class PlaylistDTOforUpdate
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "PlaylistName is required")]
        public string PlaylistName { get; set; }

        [Required(ErrorMessage = "PlaylistDescription is required")]
        public string PlaylistDescription { get; set; }

        [Required(ErrorMessage = "PlaylistDatetime is required")]
        public string PlaylistDatetime { get; set; }

        [Required(ErrorMessage = "ImagePlaylist is required")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        [FileSize(5, 10)]
        public IFormFile ImagePlaylist { get; set; }
    }
}
