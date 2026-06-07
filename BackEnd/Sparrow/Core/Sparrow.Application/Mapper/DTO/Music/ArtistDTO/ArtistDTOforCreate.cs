using Microsoft.AspNetCore.Http;
using Sparrow.Application.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sparrow.Application.Mapper.DTO.Music.ArtistDTO
{
    public class ArtistDTOforCreate
    {


        [Required(ErrorMessage = "ArtistName  is required")]
        public string ArtistName { get; set; }

        [Required(ErrorMessage = "ImageArtist is required")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        [FileSize(5, 10)]
        public IFormFile ImageArtist { get; set; }
    }
}
