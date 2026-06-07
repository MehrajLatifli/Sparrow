using Microsoft.AspNetCore.Http;
using Sparrow.Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace Sparrow.Application.Mapper.DTO.Music.AlbumDTO
{
    public class AlbumDTOforUpdate
    {
        [Required(ErrorMessage = "Id  is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Album Name  is required")]
        public string AlbumName { get; set; }

        [Required(ErrorMessage = "Image Album is required")]

        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        [FileSize(5, 10)]
        public IFormFile ImageAlbum { get; set; }
    }
}
