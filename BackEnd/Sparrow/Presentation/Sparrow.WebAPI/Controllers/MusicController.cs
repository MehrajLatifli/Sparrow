using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sparrow.Application.Mapper.DTO.Music.AlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.ArtistAlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.ArtistDTO;
using Sparrow.Application.Mapper.DTO.Music.MusicAlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.MusicDTO;
using Sparrow.Application.Mapper.DTO.Music.PlaylistDTO;
using Sparrow.Application.Mapper.DTO.Music.PlaylistMusicDTO;
using Sparrow.Application.Mapper.DTO.Music.RadioDTO;
using Sparrow.Application.Mapper.DTO.User.AuthDTO;
using Sparrow.Application.Services.Abstract.MusicServices;
using Sparrow.Application.Services.Abstract.UserServices;
using Sparrow.Application.Validations;
using Sparrow.Domain.Entities.AuthModels;
using Sparrow.Persistence.ServiceExtensions;
using Sparrow.WebAPI.API_Routes;
using System.Security.Claims;

namespace Sparrow.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        public readonly IMapper _mapper;
        private readonly IMusicService _musicService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MusicController(IMapper mapper, IMusicService musicService, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _musicService = musicService;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.Artist)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Post_ArtistForAdmin" }, CustomUserPermissions = new[] { "Create" })]
        public async Task<IActionResult> CreateArtist([FromForm] ArtistDTOforCreate model)
        {

            await _musicService.CreateArtist(model, User, ServiceExtension.ConnectionStringAzure);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{model.ArtistName} created successfully!" });
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.Artist)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_ArtistForAdmin", "Get_ArtistForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadArtists()
        {

            var allArtists = await _musicService.GetAllArtist(User);


            return Ok(allArtists);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.ArtistById)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_ArtistByIdForAdmin", "Get_ArtistByIdForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadArtist(Guid id)
        {

            var allArtists = await _musicService.GetByIdArtist(id,User);


            return Ok(allArtists);
        }


        [HttpPut]
        [MapToApiVersion("1.0")]
        [Route(Routes.UpdateArtist)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Put_ArtistForAdmin" }, CustomUserPermissions = new[] { "Update" })]
        public async Task<IActionResult> UpdateArtist([FromForm] ArtistDTOforUpdate model)
        {

            await _musicService.UpdateArtist(model, User, ServiceExtension.ConnectionStringAzure);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{model.ArtistName} updated successfully!" });

        }

        [HttpDelete]
        [MapToApiVersion("1.0")]
        [Route(Routes.DeleteArtist)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Delete_ArtistForAdmin" }, CustomUserPermissions = new[] { "Delete" })]
        public async Task<IActionResult> DeleteArtist(Guid id)
        {

            await _musicService.DeleteArtist(id, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{id} deleted successfully!" });
            
        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.Album)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Post_AlbumForAdmin" }, CustomUserPermissions = new[] { "Create" })]
        public async Task<IActionResult> CreateAlbum([FromForm] AlbumDTOforCreate model)
        {

            await _musicService.CreateAlbum(model, User, ServiceExtension.ConnectionStringAzure);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{model.AlbumName} created successfully!" });
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.Album)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_AlbumForAdmin", "Get_AlbumForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadAlbums()
        {

            var allAlbums = await _musicService.GetAllAlbum(User);


            return Ok(allAlbums);
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.AlbumById)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_AlbumByIdForAdmin", "Get_AlbumByIdForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadAlbum(Guid id)
        {

            var allAlbums = await _musicService.GetByIdAlbum(id, User);


            return Ok(allAlbums);
        }


        [HttpPut]
        [MapToApiVersion("1.0")]
        [Route(Routes.UpdateAlbum)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Put_AlbumForAdmin" }, CustomUserPermissions = new[] { "Update" })]
       public async Task<IActionResult> UpdateAlbum([FromForm] AlbumDTOforUpdate model)
        {

            await _musicService.UpdateAlbum(model, User, ServiceExtension.ConnectionStringAzure);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{model.AlbumName} updated successfully!" });

        } 


        [HttpDelete]
        [MapToApiVersion("1.0")]
        [Route(Routes.DeleteAlbum)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Delete_AlbumForAdmin" }, CustomUserPermissions = new[] { "Delete" })]
        public async Task<IActionResult> DeleteAlbum(Guid id)
        {

             await _musicService.DeleteAlbum(id, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{id} deleted successfully!" });

        }



        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.ArtistAlbum)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Post_ArtistAlbumForAdmin" }, CustomUserPermissions = new[] { "Create" })]
        public async Task<IActionResult> CreateArtistAlbum([FromForm] ArtistAlbumDTOforCreate model)
        {

            await _musicService.CreateArtistAlbum(model, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"ArtistAlbum created successfully!" });
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.ArtistAlbum)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_ArtistAlbumForAdmin", "Get_ArtistAlbumForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadaArtistAlbums()
        {

            var artistalbums = await _musicService.GetAllArtistAlbum(User);


            return Ok(artistalbums);
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.ArtistAlbumById)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_ArtistAlbumByIdForAdmin", "Get_ArtistAlbumByIdForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadArtistAlbum(Guid id)
        {

            var artistalbum = await _musicService.GetByIdArtistAlbum(id, User);


            return Ok(artistalbum);
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        [Route(Routes.UpdateArtistAlbum)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Put_ArtistAlbumForAdmin" }, CustomUserPermissions = new[] { "Update" })]
        public async Task<IActionResult> UpdateArtistAlbum([FromForm] ArtistAlbumDTOforUpdate model)
        {

            await _musicService.UpdateArtistAlbum(model, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{model.Id} updated successfully!" });

        }


        [HttpDelete]
        [MapToApiVersion("1.0")]
        [Route(Routes.DeleteArtistAlbum)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Delete_ArtistAlbumForAdmin" }, CustomUserPermissions = new[] { "Delete" })]
        public async Task<IActionResult> DeleteArtistAlbum(Guid id)
        {

            await _musicService.DeleteArtistAlbum(id, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{id} deleted successfully!" });

        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.Music)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Post_MusicForAdmin" }, CustomUserPermissions = new[] { "Create" })]
        public async Task<IActionResult> CreateMusic([FromForm] MusicDTOforCreate model)
        {

            await _musicService.CreateMusic(model, User, ServiceExtension.ConnectionStringAzure);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{model.MusicName} created successfully!" });
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.Music)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_MusicForAdmin", "Get_MusicForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadMusics()
        {

            var alllmusics = await _musicService.GetAllMusic(User);


            return Ok(alllmusics);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.MusicById)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_MusicByIdForAdmin", "Get_MusicByIdForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadMusic(Guid id)
        {

            var allMusics = await _musicService.GetByIdMusic(id, User);


            return Ok(allMusics);
        }



        [HttpPut]
        [MapToApiVersion("1.0")]
        [Route(Routes.UpdateMusic)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Put_MusicForAdmin" }, CustomUserPermissions = new[] { "Update" })]
        public async Task<IActionResult> UpdateMusic([FromForm] MusicDTOforUpdate model)
        {

            await _musicService.UpdateMusic(model, User, ServiceExtension.ConnectionStringAzure);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{model.MusicName} updated successfully!" });

        }


        [HttpDelete]
        [MapToApiVersion("1.0")]
        [Route(Routes.DeleteMusic)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Delete_MusicForAdmin" }, CustomUserPermissions = new[] { "Delete" })]
        public async Task<IActionResult> DeleteMusic(Guid id)
        {

            await _musicService.DeleteMusic(id, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{id} deleted successfully!" });

        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.MusicAlbum)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Post_MusicAlbumForAdmin" }, CustomUserPermissions = new[] { "Create" })]
        public async Task<IActionResult> CreateMusicAlbum([FromForm] MusicAlbumDTOforCreate model)
        {

            await _musicService.CreateMusicAlbum(model, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"MusicAlbum created successfully!" });
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.MusicAlbum)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_MusicAlbumForAdmin", "Get_MusicAlbumForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadMusicAlbums()
        {

            var musicAlbums = await _musicService.GetAllMusicAlbum(User);


            return Ok(musicAlbums);
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.MusicAlbumById)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin,UserRoles.User }, CustomRolePermissions = new[] { "Get_MusicAlbumByIdForAdmin","Get_MusicAlbumByIdForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadMusicAlbum(Guid id)
        {

            var MusicAlbum = await _musicService.GetByIdMusicAlbum(id, User);


            return Ok(MusicAlbum);
        }


        [HttpPut]
        [MapToApiVersion("1.0")]
        [Route(Routes.UpdateMusicAlbum)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Put_MusicAlbumForAdmin" }, CustomUserPermissions = new[] { "Update" })]
        public async Task<IActionResult> UpdateMusicAlbum([FromForm] MusicAlbumDTOforUpdate model)
        {

            await _musicService.UpdateMusicAlbum(model, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{model.Id} updated successfully!" });

        }

        [HttpDelete]
        [MapToApiVersion("1.0")]
        [Route(Routes.DeleteMusicAlbum)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Delete_MusicAlbumForAdmin" }, CustomUserPermissions = new[] { "Delete" })]
        public async Task<IActionResult> DeleteMusicAlbum(Guid id)
        {

            await _musicService.DeleteMusicAlbum(id, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{id} deleted successfully!" });

        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.Radio)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Post_RadioForAdmin" }, CustomUserPermissions = new[] { "Create" })]
        public async Task<IActionResult> CreateRadio([FromForm] RadioDTOforCreate model)
        {

            await _musicService.CreateRadio(model, User, ServiceExtension.ConnectionStringAzure);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{model.RadioName} created successfully!" });
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.Radio)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_RadioForAdmin", "Get_RadioForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadRadios()
        {

            var alllRadios = await _musicService.GetAllRadio(User);


            return Ok(alllRadios);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.RadioById)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_RadioByIdForAdmin", "Get_RadioByIdForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadRadio(Guid id)
        {

            var allRadios = await _musicService.GetByIdRadio(id, User);


            return Ok(allRadios);
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        [Route(Routes.UpdateRadio)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Put_RadioForAdmin" }, CustomUserPermissions = new[] { "Update" })]
        public async Task<IActionResult> UpdateRadio([FromForm] RadioDTOforUpdate model)
        {

            await _musicService.UpdateRadio(model, User, ServiceExtension.ConnectionStringAzure);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{model.RadioName} updated successfully!" });

        }

        [HttpDelete]
        [MapToApiVersion("1.0")]
        [Route(Routes.DeleteRadio)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Delete_RadioForAdmin" }, CustomUserPermissions = new[] { "Delete" })]
        public async Task<IActionResult> DeleteRadio(Guid id)
        {

            await _musicService.DeleteRadio(id, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{id} deleted successfully!" });

        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.Playlist)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Post_PlaylistForAdmin", "Post_PlaylistForUser" }, CustomUserPermissions = new[] { "Create" })]
        public async Task<IActionResult> CreatePlaylist([FromForm] PlaylistDTOforCreate model)
        {

            await _musicService.CreatePlaylist(model, User, ServiceExtension.ConnectionStringAzure);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{model.PlaylistName} created successfully!" });
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.Playlist)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_PlaylistForAdmin", "Get_PlaylistForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadPlaylists()
        {

            var alllPlaylists = await _musicService.GetAllPlaylist(User);


            return Ok(alllPlaylists);
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaylistById)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_PlaylistByIdForAdmin", "Get_PlaylistByIdForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadPlaylist(Guid id)
        {

            var allPlaylists = await _musicService.GetByIdPlaylist(id, User);


            return Ok(allPlaylists);
        }


        [HttpPut]
        [MapToApiVersion("1.0")]
        [Route(Routes.UpdatePlaylist)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Put_PlaylistForAdmin" }, CustomUserPermissions = new[] { "Update" })]
        public async Task<IActionResult> UpdatePlaylist([FromForm] PlaylistDTOforUpdate model)
        {

            await _musicService.UpdatePlaylist(model, User, ServiceExtension.ConnectionStringAzure);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{model.PlaylistName} updated successfully!" });

        }


        [HttpDelete]
        [MapToApiVersion("1.0")]
        [Route(Routes.DeletePlaylist)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Delete_PlaylistForAdmin" }, CustomUserPermissions = new[] { "Delete" })]
        public async Task<IActionResult> DeletePlaylist(Guid id)
        {

            await _musicService.DeletePlaylist(id, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{id} deleted successfully!" });

        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaylistMusic)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Post_PlaylistMusicForAdmin", "Post_PlaylistMusicForUser"}, CustomUserPermissions = new[] { "Create" })]
        public async Task<IActionResult> CreatePlaylistMusic([FromForm] PlaylistMusicDTOforCreate model)
        {

            await _musicService.CreatePlaylistMusic(model, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"PlaylistMusic created successfully!" });
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaylistMusic)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_PlaylistMusicForAdmin", "Get_PlaylistMusicForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadPlaylistMusics()
        {

            var PlaylistMusics = await _musicService.GetAllPlaylistMusic(User);


            return Ok(PlaylistMusics);
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaylistMusicById)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_PlaylistMusicByIdForAdmin", "Get_PlaylistMusicByIdForUser" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadPlaylistMusic(Guid id)
        {

            var PlaylistMusic = await _musicService.GetByIdPlaylistMusic(id, User);


            return Ok(PlaylistMusic);
        }


        [HttpPut]
        [MapToApiVersion("1.0")]
        [Route(Routes.UpdatePlaylistMusic)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Put_PlaylistMusicForAdmin", "Put_PlaylistMusicForUser" }, CustomUserPermissions = new[] { "Update" })]
        public async Task<IActionResult> UpdatePlaylistMusic([FromForm] PlaylistMusicDTOforUpdate model)
        {

            await _musicService.UpdatePlaylistMusic(model, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{model.Id} updated successfully!" });

        }


        [HttpDelete]
        [MapToApiVersion("1.0")]
        [Route(Routes.DeletePlaylistMusic)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Delete_PlaylistMusicForAdmin", "Delete_PlaylistMusicForUser" }, CustomUserPermissions = new[] { "Delete" })]
        public async Task<IActionResult> DeletePlaylistMusic(Guid id)
        {

            await _musicService.DeletePlaylistMusic(id, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{id} deleted successfully!" });

        }


    }
}
