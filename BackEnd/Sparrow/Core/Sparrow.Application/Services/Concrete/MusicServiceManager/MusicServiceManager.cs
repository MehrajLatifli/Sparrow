using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sparrow.Application.Cache.RedisCachePatterns.Abstract.Music;
using Sparrow.Application.Exception;
using Sparrow.Application.Mapper.DTO.Music.AlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.ArtistAlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.ArtistDTO;
using Sparrow.Application.Mapper.DTO.Music.MusicAlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.MusicDTO;
using Sparrow.Application.Mapper.DTO.Music.PlaylistDTO;
using Sparrow.Application.Mapper.DTO.Music.PlaylistMusicDTO;
using Sparrow.Application.Mapper.DTO.Music.PlaylistUserDTO;
using Sparrow.Application.Mapper.DTO.Music.RadioDTO;
using Sparrow.Application.Mapper.DTO.User.AuthDTO;
using Sparrow.Application.Mapper.DTO.User.UserDTO;
using Sparrow.Application.Repositories.Custom.MusicRepositories;
using Sparrow.Application.Repositories.Custom.UserRepositories;
using Sparrow.Application.Services.Abstract.MusicServices;
using Sparrow.Application.Services.Abstract.UserServices;
using Sparrow.Application.Services.Concrete.UserServiceManager;
using Sparrow.Domain.Entities.IdentityAuth;
using Sparrow.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Sparrow.Application.Services.Concrete.MusicServiceManager
{
    public class MusicServiceManager : IMusicService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        private readonly IArtistCacheService<ArtistDTOforGetandGetAll> _artistCacheServiceGetandGetAll;

        private readonly IAlbumCacheService<AlbumDTOforGetandGetAll> _albumCacheServiceGetandGetAll;

        private readonly IArtistAlbumCacheService<ArtistAlbumDTOforGetandGetAll> _artistAlbumCacheServiceGetandGetAll;

        private readonly IMusicCacheService<MusicDTOforGetandGetAll> _musicCacheServiceGetandGetAll;

        private readonly IMusicAlbumCacheService<MusicAlbumDTOforGetandGetAll> _musicAlbumCacheServiceGetandGetAll;

        private readonly IRadioCacheService<RadioDTOforGetandGetAll> _radioCacheServiceGetandGetAll;

        private readonly IPlaylistCacheService<PlaylistDTOforGetandGetAll> _playlistCacheServiceGetandGetAll;

        private readonly IPlaylistUserCacheService<PlaylistUserDTOforGetandGetAll> _playlistUserCacheServiceGetandGetAll;

        private readonly IPlaylistMusicCacheService<PlaylistMusicDTOforGetandGetAll> _playlistMusicCacheServiceGetandGetAll;


        private readonly ILogger<MusicServiceManager> _logger;

        private readonly IUserReadRepository _userReadRepository;
        private readonly IArtistReadRepository _artistReadRepository;
        private readonly IArtistWriteRepository _artistWriteRepository;
        private readonly IAlbumReadRepository _albumReadRepository;
        private readonly IAlbumWriteRepository _albumWriteRepository;
        private readonly IMusicWriteRepository _musicWriteRepository;
        private readonly IMusicReadRepository _musicReadRepository;
        private readonly IMusicAlbumReadRepository _musicAlbumReadRepository;
        private readonly IMusicAlbumWriteRepository _musicAlbumWriteRepository;
        private readonly IPlaylistReadRepository _playlistReadRepository;
        private readonly IPlaylistWriteRepository _playlistWriteRepository;
        private readonly IPlaylistMusicReadRepository _playlistMusicReadRepository;
        private readonly IPlaylistMusicWriteRepository _playlistMusicWriteRepository;
        private readonly IPlaylistUserReadRepository _playlistUserReadRepository;
        private readonly IPlaylistUserWriteRepository _playlistUserWriteRepository;
        private readonly IRadioReadRepository _radioReadRepository;
        private readonly IRadioWriteRepository _radioWriteRepository;
        private readonly IArtistAlbumReadRepository _artistAlbumReadRepository;
        private readonly IArtistAlbumWriteRepository _artistAlbumWriteRepository;

        public MusicServiceManager(IConfiguration configuration, IMapper mapper, IArtistCacheService<ArtistDTOforGetandGetAll> artistCacheServiceGetandGetAll, IAlbumCacheService<AlbumDTOforGetandGetAll> albumCacheServiceGetandGetAll, IArtistAlbumCacheService<ArtistAlbumDTOforGetandGetAll> artistAlbumCacheServiceGetandGetAll, IMusicCacheService<MusicDTOforGetandGetAll> musicCacheServiceGetandGetAll, IMusicAlbumCacheService<MusicAlbumDTOforGetandGetAll> musicAlbumCacheServiceGetandGetAll, IRadioCacheService<RadioDTOforGetandGetAll> radioCacheServiceGetandGetAll, IPlaylistCacheService<PlaylistDTOforGetandGetAll> playlistCacheServiceGetandGetAll, IPlaylistUserCacheService<PlaylistUserDTOforGetandGetAll> playlistUserCacheServiceGetandGetAll, IPlaylistMusicCacheService<PlaylistMusicDTOforGetandGetAll> playlistMusicCacheServiceGetandGetAll, ILogger<MusicServiceManager> logger, IUserReadRepository userReadRepository, IArtistReadRepository artistReadRepository, IArtistWriteRepository artistWriteRepository, IAlbumReadRepository albumReadRepository, IAlbumWriteRepository albumWriteRepository, IMusicWriteRepository musicWriteRepository, IMusicReadRepository musicReadRepository, IMusicAlbumReadRepository musicAlbumReadRepository, IMusicAlbumWriteRepository musicAlbumWriteRepository, IPlaylistReadRepository playlistReadRepository, IPlaylistWriteRepository playlistWriteRepository, IPlaylistMusicReadRepository playlistMusicReadRepository, IPlaylistMusicWriteRepository playlistMusicWriteRepository, IPlaylistUserReadRepository playlistUserReadRepository, IPlaylistUserWriteRepository playlistUserWriteRepository, IRadioReadRepository radioReadRepository, IRadioWriteRepository radioWriteRepository, IArtistAlbumReadRepository artistAlbumReadRepository, IArtistAlbumWriteRepository artistAlbumWriteRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _artistCacheServiceGetandGetAll = artistCacheServiceGetandGetAll;
            _albumCacheServiceGetandGetAll = albumCacheServiceGetandGetAll;
            _artistAlbumCacheServiceGetandGetAll = artistAlbumCacheServiceGetandGetAll;
            _musicCacheServiceGetandGetAll = musicCacheServiceGetandGetAll;
            _musicAlbumCacheServiceGetandGetAll = musicAlbumCacheServiceGetandGetAll;
            _radioCacheServiceGetandGetAll = radioCacheServiceGetandGetAll;
            _playlistCacheServiceGetandGetAll = playlistCacheServiceGetandGetAll;
            _playlistUserCacheServiceGetandGetAll = playlistUserCacheServiceGetandGetAll;
            _playlistMusicCacheServiceGetandGetAll = playlistMusicCacheServiceGetandGetAll;
            _logger = logger;
            _userReadRepository = userReadRepository;
            _artistReadRepository = artistReadRepository;
            _artistWriteRepository = artistWriteRepository;
            _albumReadRepository = albumReadRepository;
            _albumWriteRepository = albumWriteRepository;
            _musicWriteRepository = musicWriteRepository;
            _musicReadRepository = musicReadRepository;
            _musicAlbumReadRepository = musicAlbumReadRepository;
            _musicAlbumWriteRepository = musicAlbumWriteRepository;
            _playlistReadRepository = playlistReadRepository;
            _playlistWriteRepository = playlistWriteRepository;
            _playlistMusicReadRepository = playlistMusicReadRepository;
            _playlistMusicWriteRepository = playlistMusicWriteRepository;
            _playlistUserReadRepository = playlistUserReadRepository;
            _playlistUserWriteRepository = playlistUserWriteRepository;
            _radioReadRepository = radioReadRepository;
            _radioWriteRepository = radioWriteRepository;
            _artistAlbumReadRepository = artistAlbumReadRepository;
            _artistAlbumWriteRepository = artistAlbumWriteRepository;
        }













        #region Artist 

        public async Task CreateArtist(ArtistDTOforCreate model, ClaimsPrincipal claimsPrincipal, string connectionStringAzure)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    string connectionString = GetAzureConnectionString(connectionStringAzure);
                    string containerName = "artist-images";
                    string userFolder = $"{model.ArtistName}/";
                    string blobName = $"{userFolder}{model.ArtistName}_{Guid.NewGuid()}{Path.GetExtension(model.ImageArtist.FileName)}";

                    var blobHttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = GetContentType(Path.GetExtension(model.ImageArtist.FileName)),
                        ContentDisposition = "inline"
                    };

                    var blobServiceClient = new BlobServiceClient(connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    var blobClient = containerClient.GetBlobClient(blobName);
                    using (var stream = model.ImageArtist.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
                    }

                    string imageUrl = blobClient.Uri.ToString();


                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    DateTime localTime = localZone.ToLocalTime(DateTime.UtcNow);



                    var artist = new Artist
                    {
                        Id = Guid.NewGuid(),
                        ArtistName = model.ArtistName,
                        ImageArtist = imageUrl,
                        
                    };

                    await _artistWriteRepository.AddAsync(artist);
                    var artistResult = await _artistWriteRepository.SaveAsync();

                    if (artistResult == -1)
                    {
                        await _artistCacheServiceGetandGetAll.ClearAllArtists();
                        throw new InvalidOperationException("Failed to create the artist.");

                    }
                    else
                    {


                        var artists = _artistReadRepository.GetAll();

                        var artistDTOs = _mapper.Map<List<ArtistDTOforGetandGetAll>>(artists);

                        await _artistCacheServiceGetandGetAll.SetAllArtists(artistDTOs);
                    }

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task DeleteArtist(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;


                    var artist = await _artistReadRepository.GetByIdAsync(Id);

                    if (artist == null)
                    {
                        throw new NotFoundException("You have entered an invalid Artist ID.");
                    }


                    _artistWriteRepository.Remove(artist);
                    var artistResult = await _artistWriteRepository.SaveAsync();

                    if (artistResult == -1)
                    {
                        await _artistCacheServiceGetandGetAll.ClearAllArtists();
                        throw new InvalidOperationException("Failed to delete the artist.");

                    }
                    else
                    {


                        var artists = _artistReadRepository.GetAll();

                        var artistDTOs = _mapper.Map<List<ArtistDTOforGetandGetAll>>(artists);

                        await _artistCacheServiceGetandGetAll.SetAllArtists(artistDTOs);
                    }

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<List<ArtistDTOforGetandGetAll>> GetAllArtist(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;

     



                    
                    var cachedArtists = await _artistCacheServiceGetandGetAll.GetAllArtists();

                    if (cachedArtists != null && cachedArtists.Count > 0)
                    {
                        return cachedArtists;
                    }


                    var artists = _artistReadRepository.GetAll();

                    var artistDTOs = _mapper.Map<List<ArtistDTOforGetandGetAll>>(artists);

                   

                    await _artistCacheServiceGetandGetAll.SetAllArtists(artistDTOs);

                    return artistDTOs;
                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<ArtistDTOforGetandGetAll> GetByIdArtist(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    var artist = await _artistReadRepository.GetByIdAsync(Id);

                    if (artist == null)
                    {
                        throw new NotFoundException("You have entered an invalid Artist ID.");
                    }
                    var artistDTO = _mapper.Map<ArtistDTOforGetandGetAll>(artist);

                    return artistDTO;

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }


        public async Task UpdateArtist(ArtistDTOforUpdate model, ClaimsPrincipal claimsPrincipal, string connectionStringAzure)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    string connectionString = GetAzureConnectionString(connectionStringAzure);
                    string containerName = "artist-images";
                    string userFolder = $"{model.ArtistName}/";
                    string blobName = $"{userFolder}{model.ArtistName}_{Guid.NewGuid()}{Path.GetExtension(model.ImageArtist.FileName)}";

                    var blobHttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = GetContentType(Path.GetExtension(model.ImageArtist.FileName)),
                        ContentDisposition = "inline"
                    };

                    var blobServiceClient = new BlobServiceClient(connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    var blobClient = containerClient.GetBlobClient(blobName);
                    using (var stream = model.ImageArtist.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
                    }

                    string imageUrl = blobClient.Uri.ToString();


                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    DateTime localTime = localZone.ToLocalTime(DateTime.UtcNow);



                    var artist = await _artistReadRepository.GetByIdAsync(model.Id);

                    if (artist == null)
                    {
                        throw new NotFoundException("You have entered an invalid Artist ID.");
                    }

                    artist.Id = model.Id;
                    artist.ArtistName = model.ArtistName;
                    artist.ImageArtist = imageUrl;

                    _artistWriteRepository.Update(artist);
                    var artistResult = await _artistWriteRepository.SaveAsync();

                    if (artistResult == -1)
                    {
                        await _artistCacheServiceGetandGetAll.ClearAllArtists();
                        throw new InvalidOperationException("Failed to create the artist.");

                    }
                    else
                    {


                        var artists = _artistReadRepository.GetAll();

                        var artistDTOs = _mapper.Map<List<ArtistDTOforGetandGetAll>>(artists);

                        await _artistCacheServiceGetandGetAll.SetAllArtists(artistDTOs);
                    }


                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }




        #endregion

        private string GetContentType(string extension)
        {
            return extension.ToLower() switch
            {
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
        }

        private string GetContentType2(string extension)
        {
            return extension.ToLower() switch
            {
                ".mp3" => "audio/mpeg",
                ".wav" => "audio/wav",
                ".flac" => "audio/flac",
                _ => "application/octet-stream"
            };
        }

        private string GetAzureConnectionString(string connectionStringAzure)
        {
            var envConnection = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_AZURE_STORAGE_CONNECTION_STRING");

            if (!string.IsNullOrWhiteSpace(envConnection))
                return envConnection;

            if (!string.IsNullOrWhiteSpace(connectionStringAzure))
                return connectionStringAzure;

            throw new InvalidOperationException("Azure Storage connection string is not configured.");
        }

        #region Album

        public async Task CreateAlbum(AlbumDTOforCreate model, ClaimsPrincipal claimsPrincipal, string ConnectionStringAzure)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    string connectionString = GetAzureConnectionString(ConnectionStringAzure);
                    string containerName = "album-images";
                    string userFolder = $"{model.AlbumName}/";
                    string blobName = $"{userFolder}{model.AlbumName}_{Guid.NewGuid()}{Path.GetExtension(model.ImageAlbum.FileName)}";

                    var blobHttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = GetContentType(Path.GetExtension(model.ImageAlbum.FileName)),
                        ContentDisposition = "inline"
                    };

                    var blobServiceClient = new BlobServiceClient(connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    var blobClient = containerClient.GetBlobClient(blobName);
                    using (var stream = model.ImageAlbum.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
                    }

                    string imageUrl = blobClient.Uri.ToString();


                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    DateTime localTime = localZone.ToLocalTime(DateTime.UtcNow);



                    var album = new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumName = model.AlbumName,
                        ImageAlbum = imageUrl,

                    };

                    await _albumWriteRepository.AddAsync(album);
                    var AlbumResult = await _albumWriteRepository.SaveAsync();

                    if (AlbumResult == -1)
                    {
                        await _albumCacheServiceGetandGetAll.ClearAllAlbums();
                        throw new InvalidOperationException("Failed to create the Album.");

                    }
                    else
                    {


                        var Albums = _albumReadRepository.GetAll();

                        var AlbumDTOs = _mapper.Map<List<AlbumDTOforGetandGetAll>>(Albums);

                        await _albumCacheServiceGetandGetAll.SetAllAlbums(AlbumDTOs);
                    }

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<List<AlbumDTOforGetandGetAll>> GetAllAlbum(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;






                    var cachedAlbums = await _albumCacheServiceGetandGetAll.GetAllAlbums();

                    if (cachedAlbums != null && cachedAlbums.Count > 0)
                    {
                        return cachedAlbums;
                    }


                    var Albums = _albumReadRepository.GetAll();

                    var AlbumDTOs = _mapper.Map<List<AlbumDTOforGetandGetAll>>(Albums);



                    await _albumCacheServiceGetandGetAll.SetAllAlbums(AlbumDTOs);

                    return AlbumDTOs;
                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<AlbumDTOforGetandGetAll> GetByIdAlbum(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    var album = await _albumReadRepository.GetByIdAsync(Id);

                    if (album == null)
                    {
                        throw new NotFoundException("You have entered an invalid Album ID.");
                    }
                    var AlbumDTO = _mapper.Map<AlbumDTOforGetandGetAll>(album);

                    return AlbumDTO;

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task UpdateAlbum(AlbumDTOforUpdate model, ClaimsPrincipal claimsPrincipal, string connectionStringAzure)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    string connectionString = GetAzureConnectionString(connectionStringAzure);
                    string containerName = "album-images";
                    string userFolder = $"{model.AlbumName}/";
                    string blobName = $"{userFolder}{model.AlbumName}_{Guid.NewGuid()}{Path.GetExtension(model.ImageAlbum.FileName)}";

                    var blobHttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = GetContentType(Path.GetExtension(model.ImageAlbum.FileName)),
                        ContentDisposition = "inline"
                    };

                    var blobServiceClient = new BlobServiceClient(connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    var blobClient = containerClient.GetBlobClient(blobName);
                    using (var stream = model.ImageAlbum.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
                    }

                    string imageUrl = blobClient.Uri.ToString();


                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    DateTime localTime = localZone.ToLocalTime(DateTime.UtcNow);



                    var album = await _albumReadRepository.GetByIdAsync(model.Id);

                    if (album == null)
                    {
                        throw new NotFoundException("You have entered an invalid Album ID.");
                    }

                    album.Id = model.Id;
                    album.AlbumName = model.AlbumName;
                    album.ImageAlbum = imageUrl;

                    _albumWriteRepository.Update(album);
                    var albumResult = await _albumWriteRepository.SaveAsync();

                    if (albumResult == -1)
                    {
                        await _albumCacheServiceGetandGetAll.ClearAllAlbums();
                        throw new InvalidOperationException("Failed to update the Album.");

                    }
                    else
                    {


                        var albums = _albumReadRepository.GetAll();

                        var albumDTOs = _mapper.Map<List<AlbumDTOforGetandGetAll>>(albums);

                        await _albumCacheServiceGetandGetAll.SetAllAlbums(albumDTOs);
                    }


                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }


        public async Task DeleteAlbum(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;


                    var Album = await _albumReadRepository.GetByIdAsync(Id);

                    if (Album == null)
                    {
                        throw new NotFoundException("You have entered an invalid Album ID.");
                    }


                    _albumWriteRepository.Remove(Album);
                    var AlbumResult = await _albumWriteRepository.SaveAsync();

                    if (AlbumResult == -1)
                    {
                        await _albumCacheServiceGetandGetAll.ClearAllAlbums();
                        throw new InvalidOperationException("Failed to delete the Album.");

                    }
                    else
                    {


                        var Albums = _albumReadRepository.GetAll();

                        var AlbumDTOs = _mapper.Map<List<AlbumDTOforGetandGetAll>>(Albums);

                        await _albumCacheServiceGetandGetAll.SetAllAlbums(AlbumDTOs);
                    }

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        #endregion


        #region ArtistAlbum

        public async Task CreateArtistAlbum(ArtistAlbumDTOforCreate model, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    var artistAlbum = new ArtistAlbum
                    {
                        Id = Guid.NewGuid(),
                        ArtistId_forArtistAlbum = model.ArtistId_forArtistAlbum,
                        AlbumId_forArtistAlbum = model.AlbumId_forArtistAlbum

                    };


                    var isArtist = _artistReadRepository
                        .GetAll()
                        .Any(x =>
                            x.Id == model.ArtistId_forArtistAlbum); 
                    
                    
                    var isAlbum = _albumReadRepository
                        .GetAll()
                        .Any(x =>
                            x.Id == model.AlbumId_forArtistAlbum);




                    if (isArtist == false || isAlbum == false)
                    {
                        throw new NotFoundException("You have entered an invalid Album ID or Artist ID.");
                    }

                    await _artistAlbumWriteRepository.AddAsync(artistAlbum);
                    var ArtistAlbumResult = await _artistAlbumWriteRepository.SaveAsync();

                    if (ArtistAlbumResult == -1)
                    {
                        await _artistAlbumCacheServiceGetandGetAll.ClearAllArtistAlbums();
                        throw new InvalidOperationException("Failed to create the ArtistAlbum.");

                    }
                    else
                    {


                        var artistAlbums = _artistAlbumReadRepository.GetAll();

                        var ArtistAlbumDTOs = _mapper.Map<List<ArtistAlbumDTOforGetandGetAll>>(artistAlbums);


                        await _artistAlbumCacheServiceGetandGetAll.SetAllArtistAlbums(ArtistAlbumDTOs);
                    }
                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<List<ArtistAlbumDTOforGetandGetAll>> GetAllArtistAlbum(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    var cachedArtistAlbums = await _artistAlbumCacheServiceGetandGetAll.GetAllArtistAlbums();

                    if (cachedArtistAlbums != null && cachedArtistAlbums.Count > 0)
                    {

                        return cachedArtistAlbums;
                    }
                     var artistAlbums = _artistAlbumReadRepository.GetAll().ToList();




                    var result = artistAlbums.Select(x => new ArtistAlbumDTOforGetandGetAll
                    {
                        Id = x.Id,
                        //ArtistId_forArtistAlbum = x.ArtistId_forArtistAlbum,
                        //AlbumId_forArtistAlbum = x.AlbumId_forArtistAlbum,

                        Artist = _artistReadRepository.GetAll().Where(a => a.Id == x.ArtistId_forArtistAlbum).Select(a => new ArtistDTOforGetandGetAll
                        {
                         Id = a.Id,
                         ArtistName = a.ArtistName,
                         ImageArtist = a.ImageArtist
                        }).FirstOrDefault(),

                        Album = _albumReadRepository.GetAll().Where(a => a.Id == x.AlbumId_forArtistAlbum).Select(a => new AlbumDTOforGetandGetAll
                        {
                         Id = a.Id,
                         AlbumName = a.AlbumName,
                         ImageAlbum = a.ImageAlbum
                        }).FirstOrDefault()})
                        
                        .ToList();



                    var artistAlbumsDTO = _mapper.Map<List<ArtistAlbumDTOforGetandGetAll>>(result);



                    await _artistAlbumCacheServiceGetandGetAll.SetAllArtistAlbums(artistAlbumsDTO);

                    return artistAlbumsDTO;
                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<ArtistAlbumDTOforGetandGetAll> GetByIdArtistAlbum(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;


                    var artistalbum = await _artistAlbumReadRepository.GetByIdAsync(Id);

                    if (artistalbum == null)
                    {
                        throw new NotFoundException("You have entered an invalid ArtistAlbum ID.");
                    }

                    var artistAlbum = await _artistAlbumReadRepository.GetByIdAsync(Id);

                    var artist = _artistReadRepository.GetAll()
                         .Where(a => a.Id == artistAlbum.ArtistId_forArtistAlbum)
                         .Select(a => new ArtistDTOforGetandGetAll
                         {
                             Id = a.Id,
                             ArtistName = a.ArtistName,
                             ImageArtist = a.ImageArtist
                         })
                         .FirstOrDefault();

                    var album = _albumReadRepository.GetAll()
                        .Where(a => a.Id == artistAlbum.AlbumId_forArtistAlbum)
                        .Select(a => new AlbumDTOforGetandGetAll
                        {
                            Id = a.Id,
                            AlbumName = a.AlbumName,
                            ImageAlbum = a.ImageAlbum
                        })
                        .FirstOrDefault();

                    var result = new ArtistAlbumDTOforGetandGetAll
                    {
                        Id = artistAlbum.Id,

                        Artist = artist,
                        Album = album
                    };


                    var ArtistAlbumDTO = _mapper.Map<ArtistAlbumDTOforGetandGetAll>(result);

                    return ArtistAlbumDTO;

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task UpdateArtistAlbum(ArtistAlbumDTOforUpdate model, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;


                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    DateTime localTime = localZone.ToLocalTime(DateTime.UtcNow);



                    var artistAlbum   = await _artistAlbumReadRepository.GetByIdAsync(model.Id);

                    if (artistAlbum == null)
                    {
                        throw new NotFoundException("You have entered an invalid ArtistAlbum ID."); 
                        
                    }


                    var isArtist = _artistReadRepository
                    .GetAll()
                    .Any(x =>
                        x.Id == model.ArtistId_forArtistAlbum);


                    var isAlbum = _albumReadRepository
                        .GetAll()
                        .Any(x =>
                            x.Id == model.AlbumId_forArtistAlbum);




                    if (isArtist == false || isAlbum == false)
                    {
                        throw new NotFoundException("You have entered an invalid Album ID or Artist ID.");
                    }



                    artistAlbum.Id = model.Id;
                    artistAlbum.AlbumId_forArtistAlbum = model.AlbumId_forArtistAlbum;
                    artistAlbum.ArtistId_forArtistAlbum = model.ArtistId_forArtistAlbum;

                    _artistAlbumWriteRepository.Update(artistAlbum);
                    var artistAlbumResult = await _artistAlbumWriteRepository.SaveAsync();

                    if (artistAlbumResult == -1)
                    {
                        await _artistAlbumCacheServiceGetandGetAll.ClearAllArtistAlbums();
                        throw new InvalidOperationException("Failed to update the ArtistAlbum.");

                    }
                    else
                    {


                        var artistAlbums     = _artistAlbumReadRepository.GetAll();


                        var result = artistAlbums.Select(x => new ArtistAlbumDTOforGetandGetAll
                        {
                            Id = x.Id,
                            //ArtistId_forArtistAlbum = x.ArtistId_forArtistAlbum,
                            //AlbumId_forArtistAlbum = x.AlbumId_forArtistAlbum,

                            Artist = _artistReadRepository.GetAll().Where(a => a.Id == x.ArtistId_forArtistAlbum).Select(a => new ArtistDTOforGetandGetAll
                            {
                                Id = a.Id,
                                ArtistName = a.ArtistName,
                                ImageArtist = a.ImageArtist
                            }).FirstOrDefault(),

                            Album = _albumReadRepository.GetAll().Where(a => a.Id == x.AlbumId_forArtistAlbum).Select(a => new AlbumDTOforGetandGetAll
                            {
                                Id = a.Id,
                                AlbumName = a.AlbumName,
                                ImageAlbum = a.ImageAlbum
                            }).FirstOrDefault()
                        }).ToList();

                        var artistAlbumDTOs = _mapper.Map<List<ArtistAlbumDTOforGetandGetAll>>(result);

                        await _artistAlbumCacheServiceGetandGetAll.SetAllArtistAlbums(artistAlbumDTOs);
                    }


                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task DeleteArtistAlbum(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;


                    var ArtistAlbum = await _artistAlbumReadRepository.GetByIdAsync(Id);

                    if (ArtistAlbum == null)
                    {
                        throw new NotFoundException("You have entered an invalid ArtistAlbum ID.");
                    }


                    _artistAlbumWriteRepository.Remove(ArtistAlbum);
                    var ArtistAlbumResult = await _artistAlbumWriteRepository.SaveAsync();

                    if (ArtistAlbumResult == -1)
                    {
                        await _artistAlbumCacheServiceGetandGetAll.ClearAllArtistAlbums();
                        throw new InvalidOperationException("Failed to delete the ArtistAlbum.");

                    }
                    else
                    {


                        var ArtistAlbums = _artistAlbumReadRepository.GetAll();

                        var result = ArtistAlbums.Select(x => new ArtistAlbumDTOforGetandGetAll
                        {
                            Id = x.Id,
                            //ArtistId_forArtistAlbum = x.ArtistId_forArtistAlbum,
                            //AlbumId_forArtistAlbum = x.AlbumId_forArtistAlbum,

                            Artist = _artistReadRepository.GetAll().Where(a => a.Id == x.ArtistId_forArtistAlbum).Select(a => new ArtistDTOforGetandGetAll
                            {
                                Id = a.Id,
                                ArtistName = a.ArtistName,
                                ImageArtist = a.ImageArtist
                            }).FirstOrDefault(),

                            Album = _albumReadRepository.GetAll().Where(a => a.Id == x.AlbumId_forArtistAlbum).Select(a => new AlbumDTOforGetandGetAll
                            {
                                Id = a.Id,
                                AlbumName = a.AlbumName,
                                ImageAlbum = a.ImageAlbum
                            }).FirstOrDefault()
                        }).ToList();

                        var ArtistAlbumDTOs = _mapper.Map<List<ArtistAlbumDTOforGetandGetAll>>(result);

                        await _artistAlbumCacheServiceGetandGetAll.SetAllArtistAlbums(ArtistAlbumDTOs);
                    }

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        #endregion


        #region Music

        public async Task CreateMusic(MusicDTOforCreate model, ClaimsPrincipal claimsPrincipal, string connectionStringAzure)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    string connectionString = GetAzureConnectionString(connectionStringAzure);


                    string containerName = "music-images";
                    string userFolder = $"{model.MusicName}/";
                    string blobName = $"{userFolder}{model.MusicName}_{Guid.NewGuid()}{Path.GetExtension(model.ImageMusic.FileName)}";

                    var blobHttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = GetContentType(Path.GetExtension(model.ImageMusic.FileName)),
                        ContentDisposition = "inline"
                    };

                    var blobServiceClient = new BlobServiceClient(connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    var blobClient = containerClient.GetBlobClient(blobName);
                    using (var stream = model.ImageMusic.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
                    }

                    string imageUrl = blobClient.Uri.ToString();




                    string containerName2 = "music-files";
                    string userFolder2 = $"{model.MusicName}/";
                    string blobName2 = $"{userFolder2}{model.MusicName}_{Guid.NewGuid()}{Path.GetExtension(model.MusicFile.FileName)}";

                    var blobHttpHeaders2 = new BlobHttpHeaders
                    {
                        ContentType = GetContentType2(Path.GetExtension(model.MusicFile.FileName)),
                        ContentDisposition = "inline"
                    };

                    var blobServiceClient2 = new BlobServiceClient(connectionString);
                    var containerClient2 = blobServiceClient2.GetBlobContainerClient(containerName2);
                    await containerClient2.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    var blobClient2 = containerClient2.GetBlobClient(blobName2);
                    using (var stream = model.MusicFile.OpenReadStream())
                    {
                        await blobClient2.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders2 });
                    }

                    string musicUrl = blobClient2.Uri.ToString();








                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    DateTime localTime = localZone.ToLocalTime(DateTime.UtcNow);




                    var music = new Music
                    {
                        Id = Guid.NewGuid(),
                        MusicName = model.MusicName,
                        isPopularMusic = model.isPopularMusic,
                        ImageMusic = imageUrl,
                        MusicFile = musicUrl
                    };




                    await _musicWriteRepository.AddAsync(music);
                    var musicResult = await _musicWriteRepository.SaveAsync();

                    if (musicResult == -1)
                    {
                        await _musicCacheServiceGetandGetAll.ClearAllMusics();
                        throw new InvalidOperationException("Failed to create the music.");
                    }
                    else
                    {


                        var musics = _musicReadRepository.GetAll();

                        var musicDTOs = _mapper.Map<List<MusicDTOforGetandGetAll>>(musics);

                        await _musicCacheServiceGetandGetAll.SetAllMusics(musicDTOs);
                    }

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<List<MusicDTOforGetandGetAll>> GetAllMusic(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;






                    var cachedMusics = await _musicCacheServiceGetandGetAll.GetAllMusics();

                    if (cachedMusics != null && cachedMusics.Count > 0)
                    {
                        return cachedMusics;
                    }


                    var musics = _musicReadRepository.GetAll();

                    var musicDTOs = _mapper.Map<List<MusicDTOforGetandGetAll>>(musics);



                    await _musicCacheServiceGetandGetAll.SetAllMusics(musicDTOs);

                    return musicDTOs;
                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<MusicDTOforGetandGetAll> GetByIdMusic(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    var music = await _musicReadRepository.GetByIdAsync(Id);

                    if (music == null)
                    {
                        throw new NotFoundException("You have entered an invalid Music ID.");
                    }
                    var musicDTO = _mapper.Map<MusicDTOforGetandGetAll>(music);

                    return musicDTO;

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
            ;
        }

        public async Task UpdateMusic(MusicDTOforUpdate model, ClaimsPrincipal claimsPrincipal, string connectionStringAzure)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;

                    string connectionString = GetAzureConnectionString(connectionStringAzure);


                    string containerName = "music-images";
                    string userFolder = $"{model.MusicName}/";
                    string blobName = $"{userFolder}{model.MusicName}_{Guid.NewGuid()}{Path.GetExtension(model.ImageMusic.FileName)}";

                    var blobHttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = GetContentType(Path.GetExtension(model.ImageMusic.FileName)),
                        ContentDisposition = "inline"
                    };

                    var blobServiceClient = new BlobServiceClient(connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    var blobClient = containerClient.GetBlobClient(blobName);
                    using (var stream = model.ImageMusic.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
                    }

                    string imageUrl = blobClient.Uri.ToString();




                    string containerName2 = "music-files";
                    string userFolder2 = $"{model.MusicName}/";
                    string blobName2 = $"{userFolder2}{model.MusicName}_{Guid.NewGuid()}{Path.GetExtension(model.MusicFile.FileName)}";

                    var blobHttpHeaders2 = new BlobHttpHeaders
                    {
                        ContentType = GetContentType2(Path.GetExtension(model.MusicFile.FileName)),
                        ContentDisposition = "inline"
                    };

                    var blobServiceClient2 = new BlobServiceClient(connectionString);
                    var containerClient2 = blobServiceClient2.GetBlobContainerClient(containerName2);
                    await containerClient2.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    var blobClient2 = containerClient2.GetBlobClient(blobName2);
                    using (var stream = model.MusicFile.OpenReadStream())
                    {
                        await blobClient2.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders2 });
                    }

                    string musicUrl = blobClient2.Uri.ToString();


                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    DateTime localTime = localZone.ToLocalTime(DateTime.UtcNow);



                    var music = await _musicReadRepository.GetByIdAsync(model.Id);

                    if (music == null)
                    {
                        throw new NotFoundException("You have entered an invalid Music ID.");
                    }

                    music.Id = model.Id;
                    music.MusicName = model.MusicName;
                    music.ImageMusic = imageUrl;
                    music.isPopularMusic = model.isPopularMusic;
                    music.MusicFile = musicUrl;
                    

                    _musicWriteRepository.Update(music);
                    var musicResult = await _musicWriteRepository.SaveAsync();

                    if (musicResult == -1)
                    {
                        await _musicCacheServiceGetandGetAll.ClearAllMusics();
                        throw new InvalidOperationException("Failed to create the music.");

                    }
                    else
                    {


                        var Musics = _musicReadRepository.GetAll();

                        var MusicDTOs = _mapper.Map<List<MusicDTOforGetandGetAll>>(Musics);

                        await _musicCacheServiceGetandGetAll.SetAllMusics(MusicDTOs);
                    }


                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task DeleteMusic(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;


                    var music = await _musicReadRepository.GetByIdAsync(Id);

                    if (music == null)
                    {
                        throw new NotFoundException("You have entered an invalid Music ID.");
                    }



                    _musicWriteRepository.Remove(music);
                    var musicResult = await _musicWriteRepository.SaveAsync();

                    if (musicResult == -1)
                    {
                        await _musicCacheServiceGetandGetAll.ClearAllMusics();
                        throw new InvalidOperationException("Failed to delete the Music.");

                    }
                    else
                    {


                        var musics = _musicReadRepository.GetAll();

                        var musicDTOs = _mapper.Map<List<MusicDTOforGetandGetAll>>(musics);

                        await _musicCacheServiceGetandGetAll.SetAllMusics(musicDTOs);
                    }

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        #endregion


        #region MusicAlbum

        public async Task CreateMusicAlbum(MusicAlbumDTOforCreate model, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    var MusicAlbum = new MusicAlbum
                    {
                        Id = Guid.NewGuid(),
                        MusicId_forMusicAlbum = model.MusicId_forMusicAlbum,
                        AlbumId_forMusicAlbum = model.AlbumId_forMusicAlbum

                    };


                    var isMusic = _musicReadRepository
                        .GetAll()
                        .Any(x =>
                            x.Id == model.MusicId_forMusicAlbum);


                    var isAlbum = _albumReadRepository
                        .GetAll()
                        .Any(x =>
                            x.Id == model.AlbumId_forMusicAlbum);




                    if (isMusic == false || isAlbum == false)
                    {
                        throw new NotFoundException("You have entered an invalid Album ID or Music ID.");
                    }

                    await _musicAlbumWriteRepository.AddAsync(MusicAlbum);
                    var musicAlbumResult = await _musicAlbumWriteRepository.SaveAsync();

                    if (musicAlbumResult == -1)
                    {
                        await _musicAlbumCacheServiceGetandGetAll.ClearAllMusicAlbums();
                        throw new InvalidOperationException("Failed to create the MusicAlbum.");

                    }
                    else
                    {


                        var MusicAlbums = _musicAlbumReadRepository.GetAll();

                        var MusicAlbumDTOs = _mapper.Map<List<MusicAlbumDTOforGetandGetAll>>(MusicAlbums);


                        await _musicAlbumCacheServiceGetandGetAll.SetAllMusicAlbums(MusicAlbumDTOs);
                    }
                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<List<MusicAlbumDTOforGetandGetAll>> GetAllMusicAlbum(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    var cachedMusicAlbums = await _musicAlbumCacheServiceGetandGetAll.GetAllMusicAlbums();

                    if (cachedMusicAlbums != null && cachedMusicAlbums.Count > 0)
                    {

                        return cachedMusicAlbums;
                    }
                    var MusicAlbums = _musicAlbumReadRepository.GetAll().ToList();




                    var result = MusicAlbums.Select(x => new MusicAlbumDTOforGetandGetAll
                    {
                        Id = x.Id,
                        //ArtistId_forMusicAlbum = x.ArtistId_forMusicAlbum,
                        //AlbumId_forMusicAlbum = x.AlbumId_forMusicAlbum,

                         Music = _musicReadRepository.GetAll().Where(a => a.Id == x.MusicId_forMusicAlbum).Select(a => new MusicDTOforGetandGetAll
                        {
                            Id = a.Id,
                            MusicFile=a.MusicFile,
                            ImageMusic=a.ImageMusic,
                            isPopularMusic=a.isPopularMusic,
                            MusicName=a.MusicName
                        }).FirstOrDefault(),

                        Album = _albumReadRepository.GetAll().Where(a => a.Id == x.AlbumId_forMusicAlbum).Select(a => new AlbumDTOforGetandGetAll
                        {
                            Id = a.Id,
                            AlbumName = a.AlbumName,
                            ImageAlbum = a.ImageAlbum
                        }).FirstOrDefault()
                    }).ToList();



                    var MusicAlbumsDTO = _mapper.Map<List<MusicAlbumDTOforGetandGetAll>>(result);



                    await _musicAlbumCacheServiceGetandGetAll.SetAllMusicAlbums(MusicAlbumsDTO);

                    return MusicAlbumsDTO;
                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<MusicAlbumDTOforGetandGetAll> GetByIdMusicAlbum(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;


                    var musicAlbum = await _musicAlbumReadRepository.GetByIdAsync(Id);

                    if (musicAlbum == null)
                    {
                        throw new NotFoundException("You have entered an invalid MusicAlbum ID.");
                    }


                    var music = _musicReadRepository.GetAll()
                         .Where(a => a.Id == musicAlbum.MusicId_forMusicAlbum)
                         .Select(a => new MusicDTOforGetandGetAll
                         {
                             Id = a.Id,
                             MusicFile = a.MusicFile,
                             ImageMusic = a.ImageMusic,
                             isPopularMusic = a.isPopularMusic,
                             MusicName = a.MusicName
                         })
                         .FirstOrDefault();

                    var album = _albumReadRepository.GetAll()
                        .Where(a => a.Id == musicAlbum.AlbumId_forMusicAlbum)
                        .Select(a => new AlbumDTOforGetandGetAll
                        {
                            Id = a.Id,
                            AlbumName = a.AlbumName,
                            ImageAlbum = a.ImageAlbum
                        })
                        .FirstOrDefault();

                    var result = new MusicAlbumDTOforGetandGetAll
                    {
                        Id = musicAlbum.Id,

                        Music = music,
                        Album = album
                    };


                    var MusicAlbumDTO = _mapper.Map<MusicAlbumDTOforGetandGetAll>(result);

                    return MusicAlbumDTO;

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task UpdateMusicAlbum(MusicAlbumDTOforUpdate model, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;


                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    DateTime localTime = localZone.ToLocalTime(DateTime.UtcNow);



                    var musicAlbum = await _musicAlbumReadRepository.GetByIdAsync(model.Id);

                    if (musicAlbum == null)
                    {
                        throw new NotFoundException("You have entered an invalid musicAlbum ID.");

                    }


                    var isMusic = _musicReadRepository
                    .GetAll()
                    .Any(x =>
                        x.Id == model.MusicId_forMusicAlbum);


                    var isAlbum = _albumReadRepository
                        .GetAll()
                        .Any(x =>
                            x.Id == model.AlbumId_forMusicAlbum);




                    if (isMusic == false || isAlbum == false)
                    {
                        throw new NotFoundException("You have entered an invalid Album ID or Music ID.");
                    }



                    musicAlbum.Id = model.Id;
                    musicAlbum.AlbumId_forMusicAlbum = model.AlbumId_forMusicAlbum;
                    musicAlbum.MusicId_forMusicAlbum = model.MusicId_forMusicAlbum  ;

                    _musicAlbumWriteRepository.Update(musicAlbum);
                    var musicAlbumResult = await _musicAlbumWriteRepository.SaveAsync();

                    if (musicAlbumResult == -1)
                    {
                        await _musicAlbumCacheServiceGetandGetAll.ClearAllMusicAlbums();
                        throw new InvalidOperationException("Failed to update the MusicAlbum.");

                    }
                    else
                    {


                        var musicAlbums = _musicAlbumReadRepository.GetAll();


                        var result = musicAlbums.Select(x => new MusicAlbumDTOforGetandGetAll
                        {
                            Id = x.Id,


                            Music = _musicReadRepository.GetAll().Where(a => a.Id == x.MusicId_forMusicAlbum).Select(a => new MusicDTOforGetandGetAll
                            {
                                Id = a.Id,
                                ImageMusic=a.ImageMusic,
                                MusicName=a.MusicName,
                                isPopularMusic = a.isPopularMusic,
                                MusicFile = a.MusicFile
                            }).FirstOrDefault(),

                            Album = _albumReadRepository.GetAll().Where(a => a.Id == x.AlbumId_forMusicAlbum).Select(a => new AlbumDTOforGetandGetAll
                            {
                                Id = a.Id,
                                AlbumName = a.AlbumName,
                                ImageAlbum = a.ImageAlbum
                            }).FirstOrDefault()
                        }).ToList();

                        var musicAlbumDTOs = _mapper.Map<List<MusicAlbumDTOforGetandGetAll>>(result);

                        await _musicAlbumCacheServiceGetandGetAll.SetAllMusicAlbums(musicAlbumDTOs);
                    }


                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            };
        }

        public async Task DeleteMusicAlbum(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;


                    var musicAlbum = await _musicAlbumReadRepository.GetByIdAsync(Id);

                    if (musicAlbum == null)
                    {
                        throw new NotFoundException("You have entered an invalid musicAlbum ID.");
                    }


                    _musicAlbumWriteRepository.Remove(musicAlbum);
                    var musicAlbumResult = await _musicAlbumWriteRepository.SaveAsync();

                    if (musicAlbumResult == -1)
                    {
                        await _musicAlbumCacheServiceGetandGetAll.ClearAllMusicAlbums();
                        throw new InvalidOperationException("Failed to delete the musicAlbum.");

                    }
                    else
                    {


                        var musicAlbums = _musicAlbumReadRepository.GetAll();

                        var result = musicAlbums.Select(x => new MusicAlbumDTOforGetandGetAll
                        {
                            Id = x.Id,
                            //ArtistId_formusicAlbum = x.ArtistId_formusicAlbum,
                            //AlbumId_formusicAlbum = x.AlbumId_formusicAlbum,

                            Music = _musicReadRepository.GetAll().Where(a => a.Id == x.MusicId_forMusicAlbum).Select(a => new MusicDTOforGetandGetAll
                            {
                                Id = a.Id,
                                MusicName = a.MusicName,
                                MusicFile = a.MusicFile,
                                ImageMusic = a.ImageMusic,
                                isPopularMusic = a.isPopularMusic
                            }).FirstOrDefault(),

                            Album = _albumReadRepository.GetAll().Where(a => a.Id == x.AlbumId_forMusicAlbum).Select(a => new AlbumDTOforGetandGetAll
                            {
                                Id = a.Id,
                                AlbumName = a.AlbumName,
                                ImageAlbum = a.ImageAlbum
                            }).FirstOrDefault()
                        }).ToList();

                        var musicAlbumDTOs = _mapper.Map<List<MusicAlbumDTOforGetandGetAll>>(result);

                        await _musicAlbumCacheServiceGetandGetAll.SetAllMusicAlbums(musicAlbumDTOs);
                    }

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        #endregion


        #region Radio

        public async Task CreateRadio(RadioDTOforCreate model, ClaimsPrincipal claimsPrincipal, string ConnectionStringAzure)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    string connectionString = GetAzureConnectionString(ConnectionStringAzure);


                    string containerName = "radio-images";
                    string userFolder = $"{model.RadioName}/";
                    string blobName = $"{userFolder}{model.RadioName}_{Guid.NewGuid()}{Path.GetExtension(model.ImageRadio.FileName)}";

                    var blobHttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = GetContentType(Path.GetExtension(model.ImageRadio.FileName)),
                        ContentDisposition = "inline"
                    };

                    var blobServiceClient = new BlobServiceClient(connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    var blobClient = containerClient.GetBlobClient(blobName);
                    using (var stream = model.ImageRadio.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
                    }

                    string imageUrl = blobClient.Uri.ToString();




     








                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    DateTime localTime = localZone.ToLocalTime(DateTime.UtcNow);




                    var radio = new Radio
                    {
                        Id = Guid.NewGuid(),
                        RadioName = model.RadioName,
                        ImageRadio = imageUrl,
                        RadioFile = model.RadioFile,
                        RadioDescription = model.RadioDescription,
                        RadioCountry = model.RadioCountry,
                        
                    };




                    await _radioWriteRepository.AddAsync(radio);
                    var radioResult = await _radioWriteRepository.SaveAsync();

                    if (radioResult == -1)
                    {
                        await _radioCacheServiceGetandGetAll.ClearAllRadios();
                        throw new InvalidOperationException("Failed to create the Radio.");
                    }
                    else
                    {


                        var Radios = _radioReadRepository.GetAll();

                        var RadioDTOs = _mapper.Map<List<RadioDTOforGetandGetAll>>(Radios);

                        await _radioCacheServiceGetandGetAll.SetAllRadios(RadioDTOs);
                    }

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }


        public async Task<List<RadioDTOforGetandGetAll>> GetAllRadio(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;






                    var cachedRadios = await _radioCacheServiceGetandGetAll.GetAllRadios();

                    if (cachedRadios != null && cachedRadios.Count > 0)
                    {
                        return cachedRadios;
                    }


                    var Radios = _radioReadRepository.GetAll();

                    var RadioDTOs = _mapper.Map<List<RadioDTOforGetandGetAll>>(Radios);



                    await _radioCacheServiceGetandGetAll.SetAllRadios(RadioDTOs);

                    return RadioDTOs;
                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<RadioDTOforGetandGetAll> GetByIdRadio(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    var radio = await _radioReadRepository.GetByIdAsync(Id);

                    if (radio == null)
                    {
                        throw new NotFoundException("You have entered an invalid Radio ID.");
                    }
                    var RadioDTO = _mapper.Map<RadioDTOforGetandGetAll>(radio);

                    return RadioDTO;

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            };
        }

        public async Task UpdateRadio(RadioDTOforUpdate model, ClaimsPrincipal claimsPrincipal, string connectionStringAzure)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;

                    string connectionString = GetAzureConnectionString(connectionStringAzure);


                    string containerName = "radio-images";
                    string userFolder = $"{model.RadioName}/";
                    string blobName = $"{userFolder}{model.RadioName}_{Guid.NewGuid()}{Path.GetExtension(model.ImageRadio.FileName)}";

                    var blobHttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = GetContentType(Path.GetExtension(model.ImageRadio.FileName)),
                        ContentDisposition = "inline"
                    };

                    var blobServiceClient = new BlobServiceClient(connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    var blobClient = containerClient.GetBlobClient(blobName);
                    using (var stream = model.ImageRadio.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
                    }

                    string imageUrl = blobClient.Uri.ToString();




    


                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    DateTime localTime = localZone.ToLocalTime(DateTime.UtcNow);



                    var radio = await _radioReadRepository.GetByIdAsync(model.Id);

                    if (radio == null)
                    {
                        throw new NotFoundException("You have entered an invalid Radio ID.");
                    }

                    radio.Id = model.Id;
                    radio.RadioName = model.RadioName;
                    radio.ImageRadio = imageUrl;
                    radio.RadioFile = model.RadioFile;
                   radio.RadioDescription = model.RadioDescription;
                    radio.RadioCountry = model.RadioCountry;
                   


                    _radioWriteRepository.Update(radio);
                    var radioResult = await _radioWriteRepository.SaveAsync();

                    if (radioResult == -1)
                    {
                        await _radioCacheServiceGetandGetAll.ClearAllRadios();
                        throw new InvalidOperationException("Failed to create the Radio.");

                    }
                    else
                    {


                        var Radios = _radioReadRepository.GetAll();

                        var RadioDTOs = _mapper.Map<List<RadioDTOforGetandGetAll>>(Radios);

                        await _radioCacheServiceGetandGetAll.SetAllRadios(RadioDTOs);
                    }


                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task DeleteRadio(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;


                    var radio = await _radioReadRepository.GetByIdAsync(Id);

                    if (radio == null)
                    {
                        throw new NotFoundException("You have entered an invalid Radio ID.");
                    }



                    _radioWriteRepository.Remove(radio);
                    var RadioResult = await _radioWriteRepository.SaveAsync();

                    if (RadioResult == -1)
                    {
                        await _radioCacheServiceGetandGetAll.ClearAllRadios();
                        throw new InvalidOperationException("Failed to delete the Radio.");

                    }
                    else
                    {


                        var radios = _radioReadRepository.GetAll();

                        var radioDTOs = _mapper.Map<List<RadioDTOforGetandGetAll>>(radios);

                        await _radioCacheServiceGetandGetAll.SetAllRadios(radioDTOs);
                    }

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        #endregion


        #region Playlist

        public async Task CreatePlaylist(PlaylistDTOforCreate model, ClaimsPrincipal claimsPrincipal, string ConnectionStringAzure)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    string connectionString = GetAzureConnectionString(ConnectionStringAzure);


                    string containerName = "playlist-images";
                    string userFolder = $"{model.PlaylistName}/";
                    string blobName = $"{userFolder}{model.PlaylistName}_{Guid.NewGuid()}{Path.GetExtension(model.ImagePlaylist.FileName)}";

                    var blobHttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = GetContentType(Path.GetExtension(model.ImagePlaylist.FileName)),
                        ContentDisposition = "inline"
                    };

                    var blobServiceClient = new BlobServiceClient(connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    var blobClient = containerClient.GetBlobClient(blobName);
                    using (var stream = model.ImagePlaylist.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
                    }

                    string imageUrl = blobClient.Uri.ToString();




                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    DateTime localTime = localZone.ToLocalTime(DateTime.UtcNow);




                    var Playlist = new Playlist
                    {
                        Id = Guid.NewGuid(),
                        PlaylistName = model.PlaylistName,
                        ImagePlaylist = imageUrl,
                        PlaylistDescription = model.PlaylistDescription,
                        PlaylistDatetime = model.PlaylistDatetime,
                       
                    };


                    var playlistUser = new PlaylistUser
                    {
                        Id = Guid.NewGuid(),
                        PlaylistId_forPlaylistUser = Playlist.Id,
                        UserId_forPlaylistUser = _userReadRepository.GetAll().FirstOrDefault(x => x.Username == currentUser).Id
                    };



                    await _playlistWriteRepository.AddAsync(Playlist);
                    var PlaylistResult = await _playlistWriteRepository.SaveAsync();

                    if (PlaylistResult == -1)
                    {
                        await _playlistCacheServiceGetandGetAll.ClearAllPlaylists();
                        throw new InvalidOperationException("Failed to create the Playlist.");
                    }
                    else
                    {


                        var playlists = _playlistReadRepository
                                            .GetAll(false)
                                            .ToList();

                        var playlistDTOs =
                            _mapper.Map<List<PlaylistDTOforGetandGetAll>>(playlists);

                        await _playlistCacheServiceGetandGetAll.SetAllPlaylists(playlistDTOs);
                    }

                    await _playlistUserWriteRepository.AddAsync(playlistUser);
                    var playlistUserResult = await _playlistUserWriteRepository.SaveAsync();

                    if (playlistUserResult == -1)
                    {
                        await _playlistUserCacheServiceGetandGetAll.ClearAllPlaylisUsers();
                        throw new InvalidOperationException("Failed to create the PlaylistUser.");
                    }
                    else
                    {
                        var usersDict = _userReadRepository.GetAll().ToDictionary(x => x.Id);

                        var playlistsDict = _playlistReadRepository.GetAll()
                            .ToDictionary(x => x.Id);

                        var playlistUsers = _playlistUserReadRepository.GetAll().ToList();

                        var result = playlistUsers.Select(x => new PlaylistUserDTOforGetandGetAll
                        {
                            Id = x.Id,

                            User = usersDict.ContainsKey(x.UserId_forPlaylistUser)
                                ? new UserDTOforGetandGetAll
                                {
                                    Id = usersDict[x.UserId_forPlaylistUser].Id,
                                    Username = usersDict[x.UserId_forPlaylistUser].Username,
                                    Name = usersDict[x.UserId_forPlaylistUser].Name,
                                    Email = usersDict[x.UserId_forPlaylistUser].Email
                                }
                                : null,

                            Playlist = playlistsDict.ContainsKey(x.PlaylistId_forPlaylistUser)
                                ? new PlaylistDTOforGetandGetAll
                                {
                                    Id = playlistsDict[x.PlaylistId_forPlaylistUser].Id,
                                    PlaylistName = playlistsDict[x.PlaylistId_forPlaylistUser].PlaylistName,
                                    ImagePlaylist = playlistsDict[x.PlaylistId_forPlaylistUser].ImagePlaylist,
                                    PlaylistDescription = playlistsDict[x.PlaylistId_forPlaylistUser].PlaylistDescription,
                                    PlaylistDatetime = playlistsDict[x.PlaylistId_forPlaylistUser].PlaylistDatetime
                                }
                                : null
                        }).ToList();

                        var PlaylistUseDTOs = _mapper.Map<List<PlaylistUserDTOforGetandGetAll>>(result).OrderBy(x=>x.Playlist.PlaylistDatetime).ToList();

                        await _playlistUserCacheServiceGetandGetAll.SetAllPlaylisUsers(PlaylistUseDTOs);
                    }

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<List<PlaylistDTOforGetandGetAll>> GetAllPlaylist(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;

                    var user = _userReadRepository.GetAll().FirstOrDefault(x => x.Username == currentUser);




                    var cachedPlaylists = await _playlistCacheServiceGetandGetAll.GetAllPlaylists();

                    if (cachedPlaylists != null && cachedPlaylists.Count > 0)
                    {
                        return cachedPlaylists;
                    }


                    var playlists =
                   (from pu in _playlistUserReadRepository.GetAll(false)
                    where pu.UserId_forPlaylistUser == user.Id

                    join p in _playlistReadRepository.GetAll(false)
                        on pu.PlaylistId_forPlaylistUser equals p.Id

                    select p)
                   .Distinct()
                   .ToList();


                    var playlistDTOs = _mapper.Map<List<PlaylistDTOforGetandGetAll>>(playlists);



                    await _playlistCacheServiceGetandGetAll.SetAllPlaylists(playlistDTOs);

                    return playlistDTOs;
                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<PlaylistDTOforGetandGetAll> GetByIdPlaylist(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;

                    var user = _userReadRepository.GetAll().FirstOrDefault(x => x.Username == currentUser);


                    var Playlists = _playlistReadRepository.GetAll();

                    //var result = (from pu in _playlistUserReadRepository.GetAll(false)
                    //              where pu.UserId_forPlaylistUser == user.Id

                    //              join p in _playlistReadRepository.GetAll(false)
                    //                  on pu.PlaylistId_forPlaylistUser equals p.Id

                    //              select new PlaylistDTOforGetandGetAll
                    //              {
                    //                  Id = p.Id,
                    //                  PlaylistName = p.PlaylistName,
                    //                  PlaylistDescription = p.PlaylistDescription,
                    //                  PlaylistDatetime = p.PlaylistDatetime,
                    //                  ImagePlaylist = p.ImagePlaylist,

                    //                  Musics = (from pm in _playlistMusicReadRepository.GetAll(false)
                    //                            join m in _musicReadRepository.GetAll(false)
                    //                                on pm.MusicId_forPlaylistMusic equals m.Id
                    //                            where pm.PlaylistId_forPlaylistMusic == p.Id

                    //                            select new MusicDTOforGetandGetAll
                    //                            {
                    //                                MusicName = m.MusicName,
                    //                                ImageMusic = m.ImageMusic,
                    //                                MusicFile = m.MusicFile
                    //                            }).ToList()
                    //              })
                    //                .ToList();

                    var PlaylistDTOs = _mapper.Map<List<PlaylistDTOforGetandGetAll>>(Playlists);

                    var playlist = PlaylistDTOs.Where(p => p.Id == Id).FirstOrDefault();

                    if (playlist == null)
                    {
                        throw new NotFoundException("You have entered an invalid Playlist ID.");
                    }

                    return playlist;
                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task UpdatePlaylist(PlaylistDTOforUpdate model, ClaimsPrincipal claimsPrincipal, string connectionStringAzure)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;

                    string connectionString = GetAzureConnectionString(connectionStringAzure);


                    string containerName = "playlist-images";
                    string userFolder = $"{model.PlaylistName}/";
                    string blobName = $"{userFolder}{model.PlaylistName}_{Guid.NewGuid()}{Path.GetExtension(model.ImagePlaylist.FileName)}";

                    var blobHttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = GetContentType(Path.GetExtension(model.ImagePlaylist.FileName)),
                        ContentDisposition = "inline"
                    };

                    var blobServiceClient = new BlobServiceClient(connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    var blobClient = containerClient.GetBlobClient(blobName);
                    using (var stream = model.ImagePlaylist.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
                    }

                    string imageUrl = blobClient.Uri.ToString();







                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    DateTime localTime = localZone.ToLocalTime(DateTime.UtcNow);



                    var playlist = await _playlistReadRepository.GetByIdAsync(model.Id);

                    if (playlist == null)
                    {
                        throw new NotFoundException("You have entered an invalid Playlist ID.");
                    }

                    playlist.Id = model.Id;
                    playlist.PlaylistName = model.PlaylistName;
                    playlist.ImagePlaylist = imageUrl;
                    playlist.PlaylistDatetime = model.PlaylistDatetime;
                    playlist.PlaylistDescription = model.PlaylistDescription;
                    playlist.PlaylistName = model.PlaylistName;
                    




                    _playlistWriteRepository.Update(playlist);
                    var PlaylistResult = await _playlistWriteRepository.SaveAsync();

                    if (PlaylistResult == -1)
                    {
                        await _playlistCacheServiceGetandGetAll.ClearAllPlaylists();
                        throw new InvalidOperationException("Failed to create the Playlist.");

                    }
                    else
                    {


                        var Playlists = _playlistReadRepository.GetAll();

                        var PlaylistDTOs = _mapper.Map<List<PlaylistDTOforGetandGetAll>>(Playlists);

                        await _playlistCacheServiceGetandGetAll.SetAllPlaylists(PlaylistDTOs);
                    }


                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task DeletePlaylist(Guid Id, ClaimsPrincipal claimsPrincipal)
        {

            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;


                    var Playlist = await _playlistReadRepository.GetByIdAsync(Id);

                    if (Playlist == null)
                    {
                        throw new NotFoundException("You have entered an invalid Playlist ID.");
                    }



                    _playlistWriteRepository.Remove(Playlist);
                    var PlaylistResult = await _playlistWriteRepository.SaveAsync();

                    if (PlaylistResult == -1)
                    {
                        await _playlistCacheServiceGetandGetAll.ClearAllPlaylists();
                        throw new InvalidOperationException("Failed to delete the Playlist.");

                    }
                    else
                    {


                        var Playlists = _playlistReadRepository.GetAll();

                        var PlaylistDTOs = _mapper.Map<List<PlaylistDTOforGetandGetAll>>(Playlists);

                        await _playlistCacheServiceGetandGetAll.SetAllPlaylists(PlaylistDTOs);
                    }




                    var currentUserId = _userReadRepository
                        .GetAll(false)
                        .Where(u => u.Username == currentUser)
                        .Select(u => u.Id)
                        .FirstOrDefault();

                    var playlistUser = _playlistUserReadRepository
                        .GetAll(false)
                        .FirstOrDefault(x =>
                            x.PlaylistId_forPlaylistUser == Id &&
                            x.UserId_forPlaylistUser == currentUserId);


                    if (playlistUser != null)
                    {




                        _playlistUserWriteRepository.Remove(playlistUser);
                        var PlaylistUserResult = await _playlistUserWriteRepository.SaveAsync();

                        if (PlaylistUserResult == -1)
                        {
                            await _playlistCacheServiceGetandGetAll.ClearAllPlaylists();
                            throw new InvalidOperationException("Failed to delete the PlaylistUser.");

                        }
                        else
                        {


                            var PlaylistUsers = _playlistReadRepository.GetAll();

                            var PlaylistUserDTOs = _mapper.Map<List<PlaylistUserDTOforGetandGetAll>>(PlaylistUsers);

                            await _playlistUserCacheServiceGetandGetAll.SetAllPlaylisUsers(PlaylistUserDTOs);
                        }
                    }

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        #endregion


        #region PlaylistMusic

        public async Task CreatePlaylistMusic(PlaylistMusicDTOforCreate model, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    var PlaylistMusic = new PlaylistMusic
                    {
                        Id = Guid.NewGuid(),
                        MusicId_forPlaylistMusic = model.MusicId_forPlaylistMusic,
                        PlaylistId_forPlaylistMusic = model.PlaylistId_forPlaylistMusic

                    };


                    var isMusic = _musicReadRepository
                        .GetAll()
                        .Any(x =>
                            x.Id == model.MusicId_forPlaylistMusic);


                    var isPlaylist = _playlistReadRepository
                        .GetAll()
                        .Any(x =>
                            x.Id == model.PlaylistId_forPlaylistMusic);




                    if (isMusic == false || isPlaylist == false)
                    {
                        throw new NotFoundException("You have entered an invalid Playlist ID or Music ID.");
                    }

                    await _playlistMusicWriteRepository.AddAsync(PlaylistMusic);
                    var PlaylistMusicResult = await _playlistMusicWriteRepository.SaveAsync();

                    if (PlaylistMusicResult == -1)
                    {
                        await _playlistMusicCacheServiceGetandGetAll.ClearAllPlaylistMusics();
                        throw new InvalidOperationException("Failed to create the PlaylistMusic.");

                    }
                    else
                    {


                        var user = _userReadRepository
                       .GetAll(false)
                       .First(x => x.Username == currentUser);

                        var cacheData =
                        (
                            from pu in _playlistUserReadRepository.GetAll(false)
                            where pu.UserId_forPlaylistUser == user.Id

                            join p in _playlistReadRepository.GetAll(false)
                                on pu.PlaylistId_forPlaylistUser equals p.Id

                            select new PlaylistMusicDTOforGetandGetAll
                            {
                                //Id =
                                //    _playlistMusicReadRepository.GetAll(false)
                                //        .Where(pm => pm.PlaylistId_forPlaylistMusic == p.Id)
                                //        .Select(pm => pm.Id)
                                //        .FirstOrDefault(),

                                PlaylistId = p.Id,
                                PlaylistName = p.PlaylistName,
                                PlaylistDescription = p.PlaylistDescription,
                                PlaylistImage = p.ImagePlaylist,

                                Musics =
                                (
                                    from pm in _playlistMusicReadRepository.GetAll(false)

                                    join m in _musicReadRepository.GetAll(false)
                                        on pm.MusicId_forPlaylistMusic equals m.Id

                                    where pm.PlaylistId_forPlaylistMusic == p.Id

                                    select new MusicDTOforGetandGetAll
                                    {
                                        Id = m.Id,
                                        MusicName = m.MusicName,
                                        MusicFile = m.MusicFile,
                                        ImageMusic = m.ImageMusic,
                                        isPopularMusic = m.isPopularMusic
                                    }
                                ).ToList()
                            }
                        ).ToList();


                        await _playlistMusicCacheServiceGetandGetAll.SetAllPlaylistMusics(cacheData);
                    }
                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<List<PlaylistMusicDTOforGetandGetAll>> GetAllPlaylistMusic(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;



                    var cachedPlaylistMusics = await _playlistMusicCacheServiceGetandGetAll.GetAllPlaylistMusics();

                    if (cachedPlaylistMusics != null && cachedPlaylistMusics.Count > 0)
                    {

                        return cachedPlaylistMusics;
                    }
                    var PlaylistMusics = _playlistMusicReadRepository.GetAll().ToList();



                    var user = _userReadRepository.GetAll(false).FirstOrDefault(x => x.Username == currentUser);



                    if (user == null)
                        throw new UnauthorizedException("User not found.");

   
                    var cached = await _playlistMusicCacheServiceGetandGetAll
                        .GetAllPlaylistMusics();

                    if (cached != null && cached.Any())
                        return cached;



                    var result =
                    from pu in _playlistUserReadRepository.GetAll(false)
                    where pu.UserId_forPlaylistUser == user.Id

                    join p in _playlistReadRepository.GetAll(false)
                        on pu.PlaylistId_forPlaylistUser equals p.Id

                    select new PlaylistMusicDTOforGetandGetAll
                    {
                        PlaylistId = p.Id,
                        PlaylistName = p.PlaylistName,
                        PlaylistDescription = p.PlaylistDescription,
                        PlaylistImage = p.ImagePlaylist,

                        // 🔥 FIX: real PlaylistMusic Id (ilk tapılan)
                        //Id = _playlistMusicReadRepository.GetAll(false)
                        //        .Where(pm => pm.PlaylistId_forPlaylistMusic == p.Id)
                        //        .Select(pm => pm.Id)
                        //        .FirstOrDefault(),

                        Musics =
                            (from pm in _playlistMusicReadRepository.GetAll(false)
                             join m in _musicReadRepository.GetAll(false)
                                 on pm.MusicId_forPlaylistMusic equals m.Id
                             where pm.PlaylistId_forPlaylistMusic == p.Id

                             select new MusicDTOforGetandGetAll
                             {
                                 Id = m.Id,
                                 MusicName = m.MusicName,
                                 MusicFile = m.MusicFile,
                                 ImageMusic = m.ImageMusic,
                                 isPopularMusic = m.isPopularMusic
                             }).ToList()
                    };



                    var list = result.ToList();







                    var PlaylistMusicsDTO = _mapper.Map<List<PlaylistMusicDTOforGetandGetAll>>(list);



                    await _playlistMusicCacheServiceGetandGetAll.SetAllPlaylistMusics(PlaylistMusicsDTO);

                    return PlaylistMusicsDTO;
                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        public async Task<PlaylistMusicDTOforGetandGetAll> GetByIdPlaylistMusic(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
     
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                if (!_mapper.Map<List<UserDTOforGetandGetAll>>(_userReadRepository.GetAll(false)).AsEnumerable().Any(i => string.IsNullOrEmpty(i.RefreshToken) && i.Username == claimsPrincipal.Identity.Name))
                {
                    var currentUser = claimsPrincipal.Identity.Name;

                    var user = _userReadRepository
                        .GetAll(false)
                        .FirstOrDefault(x => x.Username == currentUser);

                    if (user == null)
                    {
                        throw new UnauthorizedException("User not found");
                    }

                    // 🔒 playlist ownership
                    var isOwner = _playlistUserReadRepository
                        .GetAll(false)
                        .Any(x =>
                            x.UserId_forPlaylistUser == user.Id &&
                            x.PlaylistId_forPlaylistUser == Id);

                    if (!isOwner)
                    {
                        throw new UnauthorizedException("Not owner of playlist");
                    }

                    // 🎯 playlist check (DOĞRU ENTITY)
                    var playlist = _playlistReadRepository
                        .GetAll(false)
                        .FirstOrDefault(x => x.Id == Id);

                    if (playlist == null)
                    {
                        throw new NotFoundException("Playlist not found");
                    }

                    // 🎵 MUSICS (FIXED)
                    var musics = (
                        from pm in _playlistMusicReadRepository.GetAll(false)
                        join m in _musicReadRepository.GetAll(false)
                            on pm.MusicId_forPlaylistMusic equals m.Id
                        where pm.PlaylistId_forPlaylistMusic == Id
                        select new MusicDTOforGetandGetAll
                        {
                            Id = m.Id,
                            MusicName = m.MusicName,
                            MusicFile = m.MusicFile,
                            ImageMusic = m.ImageMusic,
                            isPopularMusic = m.isPopularMusic
                        }
                    ).ToList();

                    return new PlaylistMusicDTOforGetandGetAll
                    {
                        PlaylistId = playlist.Id,
                        PlaylistName = playlist.PlaylistName,
                        PlaylistDescription = playlist.PlaylistDescription,
                        PlaylistImage = playlist.ImagePlaylist,
                        Musics = musics
                    };

                }
                else
                {
                    throw new UnauthorizedException("Current user is not authenticated.");
                }
            }
            else
            {
                throw new UnauthorizedException("Current user is not authenticated.");
            }
        }

        

        public Task UpdatePlaylistMusic(PlaylistMusicDTOforUpdate model, ClaimsPrincipal claimsPrincipal)
        {
            throw new NotImplementedException();
        }

        public Task DeletePlaylistMusic(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            throw new NotImplementedException();
        }


        #endregion


    }
}
