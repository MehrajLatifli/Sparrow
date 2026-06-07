CREATE DATABASE [Music_Db]


USE [Music_Db]


CREATE TABLE [Music]
(
[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
MusicName NVARCHAR(max) NOT NULL,
isPopularMusic bit NOT NULL,
ImageMusic NVARCHAR(max) NOT NULL,
MusicFile NVARCHAR(max) NOT NULL,
)


CREATE TABLE [Playlist]
(
[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
PlaylistName NVARCHAR(max) NOT NULL,
PlaylistDescription NVARCHAR(max) NOT NULL,
PlaylistDatetime NVARCHAR(max) NOT NULL,
ImagePlaylist NVARCHAR(max) NOT NULL,
)


CREATE TABLE [PlaylistUser]
(
[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
UserId_forPlaylistUser  uniqueidentifier NOT NULL,

PlaylistId_forPlaylistUser UNIQUEIDENTIFIER NOT NULL,

Constraint FK_PlaylistId_forPlaylistUser Foreign key (PlaylistId_forPlaylistUser) References Playlist (Id) On Delete NO ACTION On Update NO ACTION,
);


CREATE TABLE [Artist]
(
[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
ArtistName NVARCHAR(max) NOT NULL,
ImageArtist NVARCHAR(max) NOT NULL,
);


CREATE TABLE [Album]
(
[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
AlbumName NVARCHAR(max) NOT NULL,
ImageAlbum NVARCHAR(max) NOT NULL,
);


CREATE TABLE [Radio]
(
[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
RadioName NVARCHAR(max) NOT NULL,
ImageRadio NVARCHAR(max) NOT NULL,
RadioFile NVARCHAR(max) NOT NULL,
RadioDescription NVARCHAR(max) NOT NULL,
RadioCountry NVARCHAR(max) NOT NULL,
);


CREATE TABLE [PlaylistMusic]
(
[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),

PlaylistId_forPlaylistMusic UNIQUEIDENTIFIER NOT NULL,
MusicId_forPlaylistMusic UNIQUEIDENTIFIER NOT NULL,

Constraint FK_PlaylistId_forPlaylistMusic Foreign key (PlaylistId_forPlaylistMusic) References Playlist (Id) On Delete NO ACTION On Update NO ACTION,
Constraint FK_MusicId_forPlaylistMusic Foreign key (MusicId_forPlaylistMusic) References Music (Id) On Delete NO ACTION On Update NO ACTION
);


CREATE TABLE [ArtistAlbum]
(
[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),

ArtistId_forArtistAlbum UNIQUEIDENTIFIER NOT NULL,
AlbumId_forArtistAlbum UNIQUEIDENTIFIER NOT NULL,

Constraint FK_ArtistId_forArtistAlbum Foreign key (ArtistId_forArtistAlbum) References Artist (Id) On Delete NO ACTION On Update NO ACTION,
Constraint FK_AlbumId_forArtistAlbum Foreign key (AlbumId_forArtistAlbum) References Album (Id) On Delete NO ACTION On Update NO ACTION
);


CREATE TABLE [MusicAlbum]
(
[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),

MusicId_forMusicAlbum UNIQUEIDENTIFIER NOT NULL,
AlbumId_forMusicAlbum UNIQUEIDENTIFIER NOT NULL,

Constraint FK_MusicId_forMusicAlbum Foreign key (MusicId_forMusicAlbum) References Music (Id) On Delete NO ACTION On Update NO ACTION,
Constraint FK_AlbumId_forMusicAlbum Foreign key (AlbumId_forMusicAlbum) References Album (Id) On Delete NO ACTION On Update NO ACTION
);



