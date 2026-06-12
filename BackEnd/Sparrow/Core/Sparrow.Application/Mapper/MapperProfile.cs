using AutoMapper;
using Microsoft.VisualBasic.FileIO;
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
using Sparrow.Application.Mapper.DTO.User.RoleClaimDTO;
using Sparrow.Application.Mapper.DTO.User.RoleDTO;
using Sparrow.Application.Mapper.DTO.User.RolePermissionDTO;
using Sparrow.Application.Mapper.DTO.User.UserClaimDTO;
using Sparrow.Application.Mapper.DTO.User.UserDTO;
using Sparrow.Application.Mapper.DTO.User.UserPermissionDTO;
using Sparrow.Application.Mapper.DTO.User.UserRoleDTO;
using Sparrow.Domain.Entities.AuthModels;
using Sparrow.Domain.Entities.IdentityAuth;
using Sparrow.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparrow.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region User

            CreateMap<Register, RegisterDTO>();
            CreateMap<RegisterDTO, Register>();

            CreateMap<Login, LoginDTO>();
            CreateMap<LoginDTO, Login>();

            CreateMap<LoginResponse, LoginResponseDTO>();
            CreateMap<LoginResponseDTO, LoginResponse>();

            CreateMap<Login2FA, LoginDTO2FA>();
            CreateMap<LoginDTO2FA, Login2FA>();

            CreateMap<LoginResponse2FA, LoginResponseDTO2FA>();
            CreateMap<LoginResponseDTO2FA, LoginResponse2FA>();

            CreateMap<UpdatePassword, UpdatePasswordDTO>();
            CreateMap<UpdatePasswordDTO, UpdatePassword>();

            CreateMap<UpdateProfile, UpdateProfileDTO>();
            CreateMap<UpdateProfileDTO, UpdateProfile>();

            CreateMap<UpdateUserBlockStatus, UpdateUserBlockStatusDTO>();
            CreateMap<UpdateUserBlockStatusDTO, UpdateUserBlockStatus>();

            CreateMap<User, UserDTOforCreate>();
            CreateMap<UserDTOforCreate, User>();
            CreateMap<User, UserDTOforUpdate>();
            CreateMap<UserDTOforUpdate, User>();
            CreateMap<User, UserDTOforGetandGetAll>();
            CreateMap<UserDTOforGetandGetAll, User>();

            CreateMap<Role, RoleDTOforCreate>();
            CreateMap<RoleDTOforCreate, Role>();
            CreateMap<Role, RoleDTOforUpdate>();
            CreateMap<RoleDTOforUpdate, Role>();
            CreateMap<Role, RoleDTOforGetandGetAll>();
            CreateMap<RoleDTOforGetandGetAll, Role>();

            CreateMap<UserRole, UserRoleDTOforCreate>();
            CreateMap<UserRoleDTOforCreate, UserRole>();
            CreateMap<UserRole, UserRoleDTOforUpdate>();
            CreateMap<UserRoleDTOforUpdate, UserRole>();
            CreateMap<UserRole, UserRoleDTOforGetandGetAll>();
            CreateMap<UserRoleDTOforGetandGetAll, UserRole>();

            CreateMap<RolePermission, RolePermissionDTOforCreate>();
            CreateMap<RolePermissionDTOforCreate, RolePermission>();
            CreateMap<RolePermission, RolePermissionDTOforUpdate>();
            CreateMap<RolePermissionDTOforUpdate, RolePermission>();
            CreateMap<RolePermission, RolePermissionDTOforGetandGetAll>();
            CreateMap<RolePermissionDTOforGetandGetAll, RolePermission>();

            CreateMap<UserPermission, UserPermissionDTOforCreate>();
            CreateMap<UserPermissionDTOforCreate, UserPermission>();
            CreateMap<UserPermission, UserPermissionDTOforUpdate>();
            CreateMap<UserPermissionDTOforUpdate, UserPermission>();
            CreateMap<UserPermission, UserPermissionDTOforGetandGetAll>();
            CreateMap<UserPermissionDTOforGetandGetAll, UserPermission>();

            CreateMap<RoleClaim, RoleClaimDTOforCreate>();
            CreateMap<RoleClaimDTOforCreate, RoleClaim>();
            CreateMap<RoleClaim, RoleClaimDTOforUpdate>();
            CreateMap<RoleClaimDTOforUpdate, RoleClaim>();
            CreateMap<RoleClaim, RoleClaimDTOforGetandGetAll>();
            CreateMap<RoleClaimDTOforGetandGetAll, RoleClaim>();

            CreateMap<UserClaim, UserClaimDTOforCreate>();
            CreateMap<UserClaimDTOforCreate, UserClaim>();
            CreateMap<UserClaim, UserClaimDTOforUpdate>();
            CreateMap<UserClaimDTOforUpdate, UserClaim>();
            CreateMap<UserClaim, UserClaimDTOforGetandGetAll>();
            CreateMap<UserClaimDTOforGetandGetAll, UserClaim>();

            #endregion


            #region 

            CreateMap<Artist, ArtistDTOforCreate>();
            CreateMap<ArtistDTOforCreate, Artist>();
            CreateMap<Artist, ArtistDTOforUpdate>();
            CreateMap<ArtistDTOforUpdate, Artist>();
            CreateMap<Artist, ArtistDTOforGetandGetAll>();
            CreateMap<ArtistDTOforGetandGetAll, Artist>();

            CreateMap<Album, AlbumDTOforCreate>();
            CreateMap<AlbumDTOforCreate, Album>();
            CreateMap<Album, AlbumDTOforUpdate>();
            CreateMap<AlbumDTOforUpdate, Album>();
            CreateMap<Album, AlbumDTOforGetandGetAll>();
            CreateMap<AlbumDTOforGetandGetAll, Album>();

            CreateMap<ArtistAlbum, ArtistAlbumDTOforCreate>();
            CreateMap<ArtistAlbumDTOforCreate, ArtistAlbum>();
            CreateMap<ArtistAlbum, ArtistAlbumDTOforUpdate>();
            CreateMap<ArtistAlbumDTOforUpdate, ArtistAlbum>();
            CreateMap<ArtistAlbum, ArtistAlbumDTOforGetandGetAll>();
            CreateMap<ArtistAlbumDTOforGetandGetAll, ArtistAlbum>();

            CreateMap<Music, MusicDTOforCreate>();
            CreateMap<MusicDTOforCreate, Music>();
            CreateMap<Music, MusicDTOforUpdate>();
            CreateMap<MusicDTOforUpdate, Music>();
            CreateMap<Music, MusicDTOforGetandGetAll>();
            CreateMap<MusicDTOforGetandGetAll, Music>();

            CreateMap<MusicAlbum, MusicAlbumDTOforCreate>();
            CreateMap<MusicAlbumDTOforCreate, MusicAlbum>();
            CreateMap<MusicAlbum, MusicAlbumDTOforUpdate>();
            CreateMap<MusicAlbumDTOforUpdate, MusicAlbum>();
            CreateMap<MusicAlbum, MusicAlbumDTOforGetandGetAll>();
            CreateMap<MusicAlbumDTOforGetandGetAll, MusicAlbum>();  

            CreateMap<MusicAlbum, MusicAlbumDTOforCreate>();
            CreateMap<MusicAlbumDTOforCreate, MusicAlbum>();
            CreateMap<MusicAlbum, MusicAlbumDTOforUpdate>();
            CreateMap<MusicAlbumDTOforUpdate, MusicAlbum>();
            CreateMap<MusicAlbum, MusicAlbumDTOforGetandGetAll>();
            CreateMap<MusicAlbumDTOforGetandGetAll, MusicAlbum>();  

            CreateMap<Playlist, PlaylistDTOforCreate>();
            CreateMap<PlaylistDTOforCreate, Playlist>();
            CreateMap<Playlist, PlaylistDTOforUpdate>();
            CreateMap<PlaylistDTOforUpdate, Playlist>();
            CreateMap<Playlist, PlaylistDTOforGetandGetAll>();
            CreateMap<PlaylistDTOforGetandGetAll, Playlist>();

            CreateMap<PlaylistMusic, PlaylistMusicDTOforCreate>();
            CreateMap<PlaylistMusicDTOforCreate, PlaylistMusic>();
            CreateMap<PlaylistMusic, PlaylistMusicDTOforUpdate>();
            CreateMap<PlaylistMusicDTOforUpdate, PlaylistMusic>();
            CreateMap<PlaylistMusic, PlaylistMusicDTOforGetandGetAll>();
            CreateMap<PlaylistMusicDTOforGetandGetAll, PlaylistMusic>();

            CreateMap<PlaylistUserDTOforCreate, PlaylistUser>();
            CreateMap<PlaylistUser, PlaylistUserDTOforCreate>();
            CreateMap<PlaylistUserDTOforUpdate, PlaylistUser>();
            CreateMap<PlaylistUser, PlaylistUserDTOforUpdate>();
            CreateMap<PlaylistUserDTOforGetandGetAll, PlaylistUser>();
            CreateMap<PlaylistUser, PlaylistUserDTOforGetandGetAll>();

            CreateMap<Radio, RadioDTOforCreate>();
            CreateMap<RadioDTOforCreate, Radio>();
            CreateMap<Radio, RadioDTOforUpdate>();
            CreateMap<RadioDTOforUpdate, Radio>();
            CreateMap<Radio, RadioDTOforGetandGetAll>();
            CreateMap<RadioDTOforGetandGetAll, Radio>();

            #endregion




        }
    }
}
