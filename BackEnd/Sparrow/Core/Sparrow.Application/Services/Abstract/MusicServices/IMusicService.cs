using Sparrow.Application.Mapper.DTO.Music.AlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.ArtistAlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.ArtistDTO;
using Sparrow.Application.Mapper.DTO.Music.MusicAlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.MusicDTO;
using Sparrow.Application.Mapper.DTO.Music.RadioDTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Sparrow.Application.Services.Abstract.MusicServices
{
    public interface IMusicService
    {
        public Task CreateArtist(ArtistDTOforCreate model, ClaimsPrincipal claimsPrincipal, string ConnectionStringAzure);
        public Task<List<ArtistDTOforGetandGetAll>> GetAllArtist(ClaimsPrincipal claimsPrincipal);
        public Task<ArtistDTOforGetandGetAll> GetByIdArtist(Guid Id, ClaimsPrincipal claimsPrincipal);
        public Task UpdateArtist(ArtistDTOforUpdate model, ClaimsPrincipal claimsPrincipal, string connectionStringAzure);
        public Task DeleteArtist(Guid Id, ClaimsPrincipal claimsPrincipal);


        public Task CreateAlbum(AlbumDTOforCreate model, ClaimsPrincipal claimsPrincipal, string ConnectionStringAzure);
        public Task<List<AlbumDTOforGetandGetAll>> GetAllAlbum(ClaimsPrincipal claimsPrincipal);
        public Task<AlbumDTOforGetandGetAll> GetByIdAlbum(Guid Id, ClaimsPrincipal claimsPrincipal);
        public Task UpdateAlbum(AlbumDTOforUpdate model, ClaimsPrincipal claimsPrincipal, string connectionStringAzure);
        public Task DeleteAlbum(Guid Id, ClaimsPrincipal claimsPrincipal);


        public Task CreateArtistAlbum(ArtistAlbumDTOforCreate model, ClaimsPrincipal claimsPrincipal);
        public Task<List<ArtistAlbumDTOforGetandGetAll>> GetAllArtistAlbum(ClaimsPrincipal claimsPrincipal);
        public Task<ArtistAlbumDTOforGetandGetAll> GetByIdArtistAlbum(Guid Id, ClaimsPrincipal claimsPrincipal);
        public Task UpdateArtistAlbum(ArtistAlbumDTOforUpdate model, ClaimsPrincipal claimsPrincipal);
        public Task DeleteArtistAlbum(Guid Id, ClaimsPrincipal claimsPrincipal);


        public Task CreateMusic(MusicDTOforCreate model, ClaimsPrincipal claimsPrincipal, string ConnectionStringAzure);
        public Task<List<MusicDTOforGetandGetAll>> GetAllMusic(ClaimsPrincipal claimsPrincipal);
        public Task<MusicDTOforGetandGetAll> GetByIdMusic(Guid Id, ClaimsPrincipal claimsPrincipal);
        public Task UpdateMusic(MusicDTOforUpdate model, ClaimsPrincipal claimsPrincipal, string connectionStringAzure);
        public Task DeleteMusic(Guid Id, ClaimsPrincipal claimsPrincipal);


        public Task CreateMusicAlbum(MusicAlbumDTOforCreate model, ClaimsPrincipal claimsPrincipal);
        public Task<List<MusicAlbumDTOforGetandGetAll>> GetAllMusicAlbum(ClaimsPrincipal claimsPrincipal);
        public Task<MusicAlbumDTOforGetandGetAll> GetByIdMusicAlbum(Guid Id, ClaimsPrincipal claimsPrincipal);
        public Task UpdateMusicAlbum(MusicAlbumDTOforUpdate model, ClaimsPrincipal claimsPrincipal);
        public Task DeleteMusicAlbum(Guid Id, ClaimsPrincipal claimsPrincipal);


        public Task CreateRadio(RadioDTOforCreate model, ClaimsPrincipal claimsPrincipal, string ConnectionStringAzure);
        public Task<List<RadioDTOforGetandGetAll>> GetAllRadio(ClaimsPrincipal claimsPrincipal);
        public Task<RadioDTOforGetandGetAll> GetByIdRadio(Guid Id, ClaimsPrincipal claimsPrincipal);
        public Task UpdateRadio(RadioDTOforUpdate model, ClaimsPrincipal claimsPrincipal, string connectionStringAzure);
        public Task DeleteRadio(Guid Id, ClaimsPrincipal claimsPrincipal);



    }
}
