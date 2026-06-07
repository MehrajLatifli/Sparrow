using Microsoft.AspNetCore.Http;
using Sparrow.Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace Sparrow.Application.Mapper.DTO.Music.ArtistDTO
{
    public class ArtistDTOforUpdate
    {
        [Required(ErrorMessage = "Id  is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ArtistName  is required")]
        public string ArtistName { get; set; }

        [Required(ErrorMessage = "ImageArtist is required")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        [FileSize(5, 10)]
        public IFormFile ImageArtist { get; set; }

    }
}
