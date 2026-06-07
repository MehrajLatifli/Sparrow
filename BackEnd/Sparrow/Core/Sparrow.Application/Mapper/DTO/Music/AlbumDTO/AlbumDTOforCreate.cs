using Microsoft.AspNetCore.Http;
using Sparrow.Application.Validations;
using Sparrow.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sparrow.Application.Mapper.DTO.Music.AlbumDTO
{
    public class AlbumDTOforCreate
    {


        [Required(ErrorMessage = "Album Name  is required")]
        public string AlbumName { get; set; }

        [Required(ErrorMessage = "Image Album is required")]

        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        [FileSize(5, 10)]
        public IFormFile ImageAlbum { get; set; }
    }
}
