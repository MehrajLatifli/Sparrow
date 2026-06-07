using Microsoft.AspNetCore.Http;

namespace Sparrow.Application.Mapper.DTO.Music.AlbumDTO
{
    public class AlbumDTOforGetandGetAll
    {
        public Guid Id { get; set; }

        public string AlbumName { get; set; }


        public string ImageAlbum { get; set; }
    }
}
