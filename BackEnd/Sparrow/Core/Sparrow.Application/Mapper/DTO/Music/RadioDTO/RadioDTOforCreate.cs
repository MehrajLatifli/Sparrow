using Microsoft.AspNetCore.Http;
using Sparrow.Application.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sparrow.Application.Mapper.DTO.Music.RadioDTO
{
    public class RadioDTOforCreate
    {

        [Required(ErrorMessage = "RadioName is required")]
        public string RadioName { get; set; }

        [Required(ErrorMessage = "ImageRadio is required")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        [FileSize(5, 10)]
        public IFormFile ImageRadio { get; set; }

        [Required(ErrorMessage = "RadioFile is required")]
        [HttpHttpsUrl]
        public string RadioFile { get; set; }

        [Required(ErrorMessage = "RadioDescription is required")]
        public string RadioDescription { get; set; }

        [Required(ErrorMessage = "RadioCountry is required")]
        public string RadioCountry { get; set; }
    }
}
