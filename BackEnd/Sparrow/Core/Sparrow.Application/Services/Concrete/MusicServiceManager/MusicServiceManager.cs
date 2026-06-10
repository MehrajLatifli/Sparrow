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
using Sparrow.Application.Mapper.DTO.Music.MusicDTO;
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

        public MusicServiceManager(IConfiguration configuration, IMapper mapper, IArtistCacheService<ArtistDTOforGetandGetAll> artistCacheServiceGetandGetAll, IAlbumCacheService<AlbumDTOforGetandGetAll> albumCacheServiceGetandGetAll, IArtistAlbumCacheService<ArtistAlbumDTOforGetandGetAll> artistAlbumCacheServiceGetandGetAll, IMusicCacheService<MusicDTOforGetandGetAll> musicCacheServiceGetandGetAll, ILogger<MusicServiceManager> logger, IUserReadRepository userReadRepository, IArtistReadRepository artistReadRepository, IArtistWriteRepository artistWriteRepository, IAlbumReadRepository albumReadRepository, IAlbumWriteRepository albumWriteRepository, IMusicWriteRepository musicWriteRepository, IMusicReadRepository musicReadRepository, IMusicAlbumReadRepository musicAlbumReadRepository, IMusicAlbumWriteRepository musicAlbumWriteRepository, IPlaylistReadRepository playlistReadRepository, IPlaylistWriteRepository playlistWriteRepository, IPlaylistMusicReadRepository playlistMusicReadRepository, IPlaylistMusicWriteRepository playlistMusicWriteRepository, IPlaylistUserReadRepository playlistUserReadRepository, IPlaylistUserWriteRepository playlistUserWriteRepository, IRadioReadRepository radioReadRepository, IRadioWriteRepository radioWriteRepository, IArtistAlbumReadRepository artistAlbumReadRepository, IArtistAlbumWriteRepository artistAlbumWriteRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _artistCacheServiceGetandGetAll = artistCacheServiceGetandGetAll;
            _albumCacheServiceGetandGetAll = albumCacheServiceGetandGetAll;
            _artistAlbumCacheServiceGetandGetAll = artistAlbumCacheServiceGetandGetAll;
            _musicCacheServiceGetandGetAll = musicCacheServiceGetandGetAll;
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

        private string GetAzureConnectionString(string connectionStringAzure)
        {
            var envConnection = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_AZURE_STORAGE_CONNECTION_STRING");

            if (!string.IsNullOrWhiteSpace(envConnection))
                return envConnection;

            if (!string.IsNullOrWhiteSpace(connectionStringAzure))
                return connectionStringAzure;

            throw new InvalidOperationException("Azure Storage connection string is not configured.");
        }

        #endregion


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
                        ContentType = GetContentType(Path.GetExtension(model.MusicFile.FileName)),
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

        public Task UpdateMusic(MusicDTOforUpdate model, ClaimsPrincipal claimsPrincipal, string connectionStringAzure)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMusic(Guid Id, ClaimsPrincipal claimsPrincipal)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
