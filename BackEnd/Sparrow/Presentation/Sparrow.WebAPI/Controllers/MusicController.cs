using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            var allArtists =  _musicService.DeleteArtist(id, User);


            return Ok(new Application.Mapper.DTO.User.AuthDTO.Response { Status = "Success", Message = $"{id} deleted successfully!" });
            
        }
    }
}
