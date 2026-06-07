using Microsoft.AspNetCore.Http;
using Sparrow.Application.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sparrow.Application.Mapper.DTO.Music.MusicDTO
{
    public class MusicDTOforCreate
    {
        [Required(ErrorMessage = "MusicName  is required")]
        public string MusicName { get; set; }

        [Required(ErrorMessage = "isPopularMusic  is required")]
        public bool isPopularMusic { get; set; }


        [Required(ErrorMessage = "ImageMusic is required")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        [FileSize(5, 10)]
        public IFormFile ImageMusic { get; set; }


        [Required(ErrorMessage = "MusicFile is required")]
        [AllowedExtensions(new string[] { ".mp3", ".wav", ".flac" })]
        [FileSize(10, 50)]
        public IFormFile MusicFile { get; set; }
    }
}
