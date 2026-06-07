using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Sparrow.Application.Mapper.DTO.User.AuthDTO;
using Sparrow.Application.Services.Abstract.UserServices;
using Sparrow.Application.Validations;
using Sparrow.Domain.Entities.AuthModels;
using Sparrow.Persistence.Contexts.UserDbContext;
using Sparrow.Persistence.ServiceExtensions;
using Sparrow.WebAPI.API_Routes;
using Response = Sparrow.Domain.Entities.AuthModels.Response;

namespace Sparrow.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public readonly IMapper _mapper;
        private readonly IAuthService _authservice;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthController(IMapper mapper, IAuthService authservice, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _authservice = authservice;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.RegisterAdmin)]
        [Produces("application/json")]
        public async Task<IActionResult> RegisterAdmin([FromForm] RegisterDTO model)
        {

            await _authservice.RegisterAdmin(model, ServiceExtension.ConnectionStringAzure);


            return Ok(new Response { Status = "Success", Message = $"{model.Username} created successfully!" });
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.RegisterUser)]
        [Produces("application/json")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterDTO model)
        {

            await _authservice.RegisterUser(model, ServiceExtension.ConnectionStringAzure);


            return Ok(new Domain.Entities.AuthModels.Response { Status = "Success", Message = $"{model.Username} created successfully!" });
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.Login)]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var loginResult = await _authservice.Login(model);

            return Ok(loginResult);
        }


        [HttpPost("login-2fa")]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        public async Task<IActionResult> LoginWith2FA([FromBody] LoginDTO2FA model)
        {
            var response = await _authservice.LoginWith2FA(model);
            return Ok(response);
        }

        [HttpGet("generate-2fa-qrcode")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Generate2FAQRCode(string username)
        {
            string logoPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "minilogo1.png");

            var qrCodeImage = await _authservice.Generate2FAQRCode(username, logoPath);



            return File(qrCodeImage, "image/png");
        }







        [HttpGet("GenerateTotpCode")]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        public async Task<IActionResult> GenerateTotpCode(string username)
        {

            string digits = await _authservice.GenerateTotpCode(username);


            return Ok(new { digits = digits });
        }



        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.Logout)]
        [Produces("application/json")]
        public async Task<IActionResult> Logout()
        {
            await _authservice.Logout(User);

       

            return NoContent();
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.RefreshToken)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Post_RefreshTokenForAdmin", "Post_RefreshTokenForUser" }, CustomUserPermissions = new[] { "Create" })]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {

            return Ok(await _authservice.RefreshToken(tokenModel, User));

        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.Profile)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Get_ProfileForAdmin", "Get_ProfileForUser" }, CustomUserPermissions = new[] { "Read" })]
        [OutputCache(Duration = 10)]
        public async Task<IActionResult> Profile()
        {
            var userProfile = await _authservice.Profile(User);

            return Ok(userProfile);
        }




        [HttpPut]
        [MapToApiVersion("1.0")]
        [Route(Routes.Profile)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Put_ProfileForAdmin", "Put_ProfileForUser" }, CustomUserPermissions = new[] { "Update" })]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileDTO model)
        {
            await _authservice.UpdateProfile(model, User, ServiceExtension.ConnectionStringAzure);
            return Ok(new Response { Status = "Success", Message = $"{model.Username} updated successfully!" });


        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        [Route(Routes.ProfilePassword)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Put_PasswordForAdmin", "Put_PasswordForUser" }, CustomUserPermissions = new[] { "Update" })]
        public async Task<IActionResult> UpdateProfilePassword([FromForm] UpdatePasswordDTO model)
        {
            await _authservice.UpdateProfilePassword(model, User);
            return Ok(new Response { Status = "Success", Message = $"Old pasword updated successfully!" });


        }




        [HttpDelete]
        [MapToApiVersion("1.0")]
        [Route(Routes.DeleteProfile)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin, UserRoles.User }, CustomRolePermissions = new[] { "Delete_ProfileForAdmin", "Delete_ProfileForUser" }, CustomUserPermissions = new[] { "Delete" })]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            await _authservice.DeleteProfile(id, User);
            return Ok(new Response { Status = "Success", Message = $"Profile deleted successfully!" });


        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.User)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Get_UsersForAdmin" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ViewUsers()
        {


  


            return Ok(await _authservice.GetUsers(User));



        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.UserById)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Get_UserByIdForAdmin" }, CustomUserPermissions = new[] { "Read" })]
        public async Task<IActionResult> ViewUserByID(Guid id)
        {

           


            return Ok(await _authservice.GetUserById(id, User));



        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        [Route(Routes.UserBlockStatus)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Put_UserBlockForAdmin" }, CustomUserPermissions = new[] { "Update" })]
        public async Task<IActionResult> UpdateProfilePassword([FromForm] UpdateUserBlockStatusDTO model)
        {
            await _authservice.UpdateUserBlock(model, User);
            return Ok(new Response { Status = "Success", Message = $"The block status of the user with id {model.Id} has been changed to {model.IsBlcok}." });


        }

        [HttpDelete]
        [MapToApiVersion("1.0")]
        [Route(Routes.DeleteUser)]
        [Produces("application/json")]
        [CustomAuthorize(CustomRoles = new[] { UserRoles.Admin }, CustomRolePermissions = new[] { "Delete_UserForAdmin" }, CustomUserPermissions = new[] { "Delete" })]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _authservice.DeleteUser(id, User);
            return Ok(new Response { Status = "Success", Message = $"User deleted successfully!" });


        }


    }
}
