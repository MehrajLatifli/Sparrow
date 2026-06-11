namespace Sparrow.WebAPI.API_Routes
{
    public struct Routes
    {
        #region Auth

        public const string Profile = "profile";
        public const string DeleteProfile = Profile + "/{id:guid}";
        public const string ProfilePassword = "profilePassword";
        public const string UserBlockStatus = "UserBlockStatus";
        public const string RegisterAdmin = "registerAdmin";
        public const string RegisterUser = "registerUser";
        public const string Login = "login";
        public const string Logout = "logout";
        public const string User = "user";
        public const string UserById = User + "/{id:guid}";
        public const string DeleteUser = User + "/{id:guid}";
        public const string RefreshToken = "refreshtoken";

        #endregion


        #region Artist

        public const string Artist = "artist";
        public const string ArtistById = Artist + "/{id:guid}";
        public const string UpdateArtist = Artist;
        public const string DeleteArtist = Artist + "/{id:guid}";
        #endregion

        #region Album

        public const string Album = "album";
        public const string AlbumById = Album + "/{id:guid}";
        public const string UpdateAlbum = Album;
        public const string DeleteAlbum = Album + "/{id:guid}";
        #endregion

        #region ArtistAlbum

        public const string ArtistAlbum = "artistAlbum";
        public const string ArtistAlbumById = ArtistAlbum + "/{id:guid}";
        public const string UpdateArtistAlbum = ArtistAlbum;
        public const string DeleteArtistAlbum = ArtistAlbum + "/{id:guid}";
        #endregion

        #region Music

        public const string Music = "music";
        public const string MusicById = Music + "/{id:guid}";
        public const string UpdateMusic = Music;
        public const string DeleteMusic = Music + "/{id:guid}";
        #endregion

        #region MusicAlbum

        public const string MusicAlbum = "musicAlbum";
        public const string MusicAlbumById = MusicAlbum + "/{id:guid}";
        public const string UpdateMusicAlbum = MusicAlbum;
        public const string DeleteMusicAlbum = MusicAlbum + "/{id:guid}";
        #endregion
    }
}
