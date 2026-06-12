using Sparrow.Application.Mapper.DTO.Music.PlaylistDTO;
using Sparrow.Application.Mapper.DTO.User.UserDTO;

namespace Sparrow.Application.Mapper.DTO.Music.PlaylistUserDTO
{
    public class PlaylistUserDTOforGetandGetAll
    {
  
        public Guid Id { get; set; }
        public PlaylistDTOforGetandGetAll Playlist { get; set; }
        public UserDTOforGetandGetAll User { get; set; }
    }
}
