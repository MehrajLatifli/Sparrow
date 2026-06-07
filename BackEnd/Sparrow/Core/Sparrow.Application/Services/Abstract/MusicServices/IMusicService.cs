using Sparrow.Application.Mapper.DTO.Music.ArtistDTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Sparrow.Application.Services.Abstract.MusicServices
{
    public interface IMusicService
    {
        public Task CreateArtist(ArtistDTOforCreate item, ClaimsPrincipal claimsPrincipal, string ConnectionStringAzure);
        public Task<List<ArtistDTOforGetandGetAll>> GetAllArtist(ClaimsPrincipal claimsPrincipal);
        public Task<ArtistDTOforGetandGetAll> GetByIdArtist(Guid Id, ClaimsPrincipal claimsPrincipal);
        public Task UpdateArtist(ArtistDTOforUpdate item, ClaimsPrincipal claimsPrincipal, string connectionStringAzure);
        public Task DeleteArtist(Guid Id, ClaimsPrincipal claimsPrincipal);


    }
}
