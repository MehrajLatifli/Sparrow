using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sparrow.Application.Mapper.DTO.Music.AlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.ArtistAlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.ArtistDTO;
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
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Get_ArtistForAdmin" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadArtists()
        {

            var allArtists = await _musicService.GetAllArtist(User);


            return Ok(allArtists);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.ArtistById)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Get_ArtistByIdForAdmin" }, CustomUserPermissions = new[] { "Read" })]
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
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Get_AlbumForAdmin" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadAlbums()
        {

            var allAlbums = await _musicService.GetAllAlbum(User);


            return Ok(allAlbums);
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.AlbumById)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Get_AlbumByIdForAdmin" }, CustomUserPermissions = new[] { "Read" })]
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
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Get_ArtistAlbumForAdmin" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ReadaArtistAlbums()
        {

            var artistalbums = await _musicService.GetAllArtistAlbum(User);


            return Ok(artistalbums);
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.ArtistAlbumById)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Get_ArtistAlbumByIdForAdmin" }, CustomUserPermissions = new[] { "Read" })]
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
    }
}
