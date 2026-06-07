namespace Sparrow.Application.Mapper.DTO.User.AuthDTO
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public string Expiration { get; set; }

    }
}

