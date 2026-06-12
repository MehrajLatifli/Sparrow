using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sparrow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SparrowContextMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlbumName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageAlbum = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageArtist = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Music",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isPopularMusic = table.Column<bool>(type: "bit", nullable: false),
                    ImageMusic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MusicFile = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Music", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaylistDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaylistDatetime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePlaylist = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Radio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RadioName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageRadio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RadioFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RadioDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RadioCountry = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArtistAlbum",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtistId_forArtistAlbum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlbumId_forArtistAlbum = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistAlbum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtistAlbum_Album_AlbumId_forArtistAlbum",
                        column: x => x.AlbumId_forArtistAlbum,
                        principalTable: "Album",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistAlbum_Artist_ArtistId_forArtistAlbum",
                        column: x => x.ArtistId_forArtistAlbum,
                        principalTable: "Artist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicAlbum",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicId_forMusicAlbum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlbumId_forMusicAlbum = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicAlbum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicAlbum_Album_AlbumId_forMusicAlbum",
                        column: x => x.AlbumId_forMusicAlbum,
                        principalTable: "Album",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicAlbum_Music_MusicId_forMusicAlbum",
                        column: x => x.MusicId_forMusicAlbum,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistMusic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId_forPlaylistMusic = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicId_forPlaylistMusic = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistMusic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaylistMusic_Music_MusicId_forPlaylistMusic",
                        column: x => x.MusicId_forPlaylistMusic,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistMusic_Playlist_PlaylistId_forPlaylistMusic",
                        column: x => x.PlaylistId_forPlaylistMusic,
                        principalTable: "Playlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId_forPlaylistUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId_forPlaylistUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaylistUser_Playlist_PlaylistId_forPlaylistUser",
                        column: x => x.PlaylistId_forPlaylistUser,
                        principalTable: "Playlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistAlbum_AlbumId_forArtistAlbum",
                table: "ArtistAlbum",
                column: "AlbumId_forArtistAlbum");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistAlbum_ArtistId_forArtistAlbum",
                table: "ArtistAlbum",
                column: "ArtistId_forArtistAlbum");

            migrationBuilder.CreateIndex(
                name: "IX_MusicAlbum_AlbumId_forMusicAlbum",
                table: "MusicAlbum",
                column: "AlbumId_forMusicAlbum");

            migrationBuilder.CreateIndex(
                name: "IX_MusicAlbum_MusicId_forMusicAlbum",
                table: "MusicAlbum",
                column: "MusicId_forMusicAlbum");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistMusic_MusicId_forPlaylistMusic",
                table: "PlaylistMusic",
                column: "MusicId_forPlaylistMusic");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistMusic_PlaylistId_forPlaylistMusic",
                table: "PlaylistMusic",
                column: "PlaylistId_forPlaylistMusic");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistUser_PlaylistId_forPlaylistUser",
                table: "PlaylistUser",
                column: "PlaylistId_forPlaylistUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistAlbum");

            migrationBuilder.DropTable(
                name: "MusicAlbum");

            migrationBuilder.DropTable(
                name: "PlaylistMusic");

            migrationBuilder.DropTable(
                name: "PlaylistUser");

            migrationBuilder.DropTable(
                name: "Radio");

            migrationBuilder.DropTable(
                name: "Artist");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Music");

            migrationBuilder.DropTable(
                name: "Playlist");
        }
    }
}
