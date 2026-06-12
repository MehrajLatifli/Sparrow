using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using Sparrow.Application.Cache.RedisCachePatterns.Abstract.Music;
using Sparrow.Application.Cache.RedisCachePatterns.Abstract.User;
using Sparrow.Application.Cache.RedisCachePatterns.Concrete.Music;
using Sparrow.Application.Cache.RedisCachePatterns.Concrete.User;
using Sparrow.Application.Mapper.DTO.Music.AlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.ArtistAlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.ArtistDTO;
using Sparrow.Application.Mapper.DTO.Music.MusicAlbumDTO;
using Sparrow.Application.Mapper.DTO.Music.MusicDTO;
using Sparrow.Application.Mapper.DTO.Music.RadioDTO;
using Sparrow.Application.Mapper.DTO.User.AuthDTO;
using Sparrow.Application.Mapper.DTO.User.UserDTO;
using Sparrow.Application.Repositories.Custom.MusicRepositories;
using Sparrow.Application.Repositories.Custom.UserRepositories;
using Sparrow.Application.Services.Abstract.MusicServices;
using Sparrow.Application.Services.Abstract.UserServices;
using Sparrow.Application.Services.Concrete.MusicServiceManager;
using Sparrow.Application.Services.Concrete.UserServiceManager;
using Sparrow.Persistence.Contexts.MusicDbContext;
using Sparrow.Persistence.Contexts.UserDbContext;
using Sparrow.Persistence.LogSettings.ColumnWriters;
using Sparrow.Persistence.Repositories.Concrete;
using Sparrow.Persistence.Repositories.Custom.Music;
using Sparrow.Persistence.Repositories.Custom.User;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;

namespace Sparrow.Persistence.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static string UserDbConnectionString
        {
            get
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                ConfigurationManager configurationManager = new ConfigurationManager();

                if (env != null)
                {
                    configurationManager.SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env}.json", optional: true) // Load environment specific settings
           .AddEnvironmentVariables();
                }
                else
                {
                    configurationManager.AddJsonFile($"appsettings.json", optional: true);
                }


                return configurationManager.GetConnectionString("UserDbConnectionString");
            }
        }

        public static string MusicDbConnectionString
        {
            get
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                ConfigurationManager configurationManager = new ConfigurationManager();
                if (env != null)
                {
                    configurationManager.SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env}.json", optional: true) // Load environment specific settings
           .AddEnvironmentVariables();
                }
                else
                {
                    configurationManager.AddJsonFile($"appsettings.json", optional: true);
                }


                return configurationManager.GetConnectionString("MusicDbConnectionString");
            }
        }


        public static string ConnectionStringAzure
        {
            get
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                ConfigurationManager configurationManager = new ConfigurationManager();


                if (env != null)
                {
                    configurationManager.SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{env}.json", optional: true) // Load environment specific settings
                     .AddEnvironmentVariables();
                }
                else
                {
                    configurationManager.AddJsonFile($"appsettings.json", optional: true);
                }

                return configurationManager["ConnectionAzureStorage"];
            }
        }

        public static string RedisConnectionString
        {
            get
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                ConfigurationManager configurationManager = new ConfigurationManager();

                if (env != null)
                {
                    configurationManager.SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env}.json", optional: true) // Load environment specific settings
           .AddEnvironmentVariables();
                }
                else
                {
                    configurationManager.AddJsonFile($"appsettings.json", optional: true);
                }


                return configurationManager.GetConnectionString("RedisConnection");
            }
        }

        public static void AddRedisConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetConnectionString("RedisConnection");

            if (string.IsNullOrWhiteSpace(redisConnectionString))
            {
                throw new ArgumentNullException(nameof(redisConnectionString), "Redis connection string is missing.");
            }

            var options = ConfigurationOptions.Parse(redisConnectionString);

            options.AbortOnConnectFail = false;
            options.ConnectTimeout = 3000;
            options.ResponseTimeout = 3000;
            options.SyncTimeout = 3000;

            // 🔥 SINGLETON Redis connection (correct way)
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var mux = ConnectionMultiplexer.Connect(options);

                // optional: warm-up check
                if (!mux.IsConnected)
                {
                    throw new Exception("Redis connection failed.");
                }

                return mux;
            });


            services.AddScoped<IAuthCacheService<UserDTOforGetandGetAll>, AuthCacheService<UserDTOforGetandGetAll>>();

            services.AddScoped<IAuthCacheService<UserDTOforUpdate>, AuthCacheService<UserDTOforUpdate>>();

            services.AddScoped<IAuthCacheService<UserDTOforCreate>, AuthCacheService<UserDTOforCreate>>();

            services.AddScoped<IAuthCacheService<GetUserDTOModel>, AuthCacheService<GetUserDTOModel>>();


            services.AddScoped<IArtistCacheService<ArtistDTOforGetandGetAll>, ArtistCacheService<ArtistDTOforGetandGetAll>>();


            services.AddScoped<IAlbumCacheService<AlbumDTOforGetandGetAll>, AlbumCacheService<AlbumDTOforGetandGetAll>>();


            services.AddScoped<IArtistAlbumCacheService<ArtistAlbumDTOforGetandGetAll>, ArtistAlbumCacheService<ArtistAlbumDTOforGetandGetAll>>();


            services.AddScoped<IMusicCacheService<MusicDTOforGetandGetAll>, MusicCacheService<MusicDTOforGetandGetAll>>();


            services.AddScoped<IMusicAlbumCacheService<MusicAlbumDTOforGetandGetAll>, MusicAlbumCacheService<MusicAlbumDTOforGetandGetAll>>();


            services.AddScoped<IRadioCacheService<RadioDTOforGetandGetAll>, RadioCacheService<RadioDTOforGetandGetAll>>();



        }



        public static IServiceCollection AddPersistenceServices(
          this IServiceCollection services,
          IConfiguration configuration)
        {

            services.AddDbContext<User_DbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("UserDbConnectionString")));

            services.AddDbContext<Music_DbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("MusicDbConnectionString")));




            services.AddScoped<IAuthService, AuthServiceManager>();
            services.AddScoped<IMusicService, MusicServiceManager>();


            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();

            services.AddScoped<IRoleReadRepository, RoleReadRepository>();
            services.AddScoped<IRoleWriteRepository, RoleWriteRepository>();

            services.AddScoped<IUserRoleReadRepository, UserRoleReadRepository>();
            services.AddScoped<IUserRoleWriteRepository, UserRoleWriteRepository>();

            services.AddScoped<IUserClaimReadRepository, UserClaimReadRepository>();
            services.AddScoped<IUserClaimWriteRepository, UserClaimWriteRepository>();

            services.AddScoped<IRoleClaimReadRepository, RoleClaimReadRepository>();
            services.AddScoped<IRoleClaimWriteRepository, RoleClaimWriteRepository>();

            services.AddScoped<IUserPermissionReadRepository, UserPermissionReadRepository>();
            services.AddScoped<IUserPermissionWriteRepository, UserPermissionWriteRepository>();

            services.AddScoped<IRolePermissionReadRepository, RolePermissionReadRepository>();
            services.AddScoped<IRolePermissionWriteRepository, RolePermissionWriteRepository>();




            services.AddScoped<IArtistReadRepository, ArtistReadRepository>();
            services.AddScoped<IArtistWriteRepository, ArtistWriteRepository>();

            services.AddScoped<IAlbumReadRepository, AlbumReadRepository>();
            services.AddScoped<IAlbumWriteRepository, AlbumWriteRepository>();

            services.AddScoped<IArtistAlbumReadRepository, ArtistAlbumReadRepository>();
            services.AddScoped<IArtistAlbumWriteRepository, ArtistAlbumWriteRepository>();

            services.AddScoped<IMusicReadRepository, MusicReadRepository>();
            services.AddScoped<IMusicWriteRepository, MusicWriteRepository>();

            services.AddScoped<IMusicAlbumReadRepository, MusicAlbumReadRepository>();
            services.AddScoped<IMusicAlbumWriteRepository, MusicAlbumWriteRepository>();

            services.AddScoped<IPlaylistReadRepository, PlaylistReadRepository>();
            services.AddScoped<IPlaylistWriteRepository, PlaylistWriteRepository>();

            services.AddScoped<IPlaylistMusicReadRepository, PlaylistMusicReadRepository>();
            services.AddScoped<IPlaylistMusicWriteRepository, PlaylistMusicWriteRepository>();

            services.AddScoped<IPlaylistUserReadRepository, PlaylistUserReadRepository>();
            services.AddScoped<IPlaylistUserWriteRepository, PlaylistUserWriteRepository>();

            services.AddScoped<IRadioReadRepository, RadioReadRepository>();
            services.AddScoped<IRadioWriteRepository, RadioWriteRepository>();





           


            return services;
        }






        public static IServiceCollection APIVersion(this IServiceCollection services)
        {

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;

                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }


        public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
        {
            private readonly IApiVersionDescriptionProvider _provider;

            public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
            {
                _provider = provider;
            }

            public void Configure(SwaggerGenOptions options)
            {
                foreach (var description in _provider.ApiVersionDescriptions)
                {
                    // 🔥 prevent duplicate v1/v2 crash
                    if (options.SwaggerGeneratorOptions.SwaggerDocs.ContainsKey(description.GroupName))
                        continue;

                    options.SwaggerDoc(
                        description.GroupName,
                        new OpenApiInfo
                        {
                            Title = "Sparrow API",
                            Version = description.ApiVersion.ToString()
                        });
                }
            }
        }

    
            public static IServiceCollection AddJwtAuthentication(
                this IServiceCollection services,
                IConfiguration configuration)
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidAudience = configuration["JWT:ValidateAudience"],
                        ValidIssuer = configuration["JWT:ValidateIssuer"],

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!)),

                        NameClaimType = ClaimTypes.Name
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Headers["Authorization"].ToString();

                            if (!string.IsNullOrWhiteSpace(accessToken) &&
                                accessToken.StartsWith("Bearer "))
                            {
                                context.Token = accessToken["Bearer ".Length..].Trim();
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

                return services;
            }
        

        public static IServiceCollection AddSwaggerGenServiceExtension(this IServiceCollection services)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var apiVersioningOptions = services.BuildServiceProvider().GetRequiredService<IOptions<ApiVersioningOptions>>().Value;
            var defaultApiVersion = apiVersioningOptions.DefaultApiVersion;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"Sparrow WebAPI",
                    Version = $"v{defaultApiVersion}",
                    Description = $"Environment: {env}"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter `Bearer` [space] and then your valid token in the text input below. \r\n\r\n Example: \"Bearer apikey \""
                });

                c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                });


            });

            return services;
        }

        public static Logger AddCustomSerilog(this IServiceCollection services, string LogConnection, string SeqConnection)
        {



            services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestHeaders.Add("sec-ch-ua");
                logging.ResponseHeaders.Add("Interview.API");
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;

            });


            var fileName = "log.txt";
            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var logDirectory = Path.Combine(webRootPath, "logs");
            var logFilePath = Path.Combine(logDirectory, fileName);



            Logger log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logFilePath)
                .WriteTo.PostgreSQL(LogConnection, "Logs",
                         needAutoCreateTable: true,
                         columnOptions: new Dictionary<string, ColumnWriterBase>
                         {
                             { "message", new RenderedMessageColumnWriter() },
                             { "message_template", new MessageTemplateColumnWriter() },
                             { "level", new LevelColumnWriter() },
                             { "time_stamp", new TimestampColumnWriter() },
                             { "exeptions", new ExceptionColumnWriter() },
                             { "log_event", new LogEventSerializedColumnWriter() },
                             { "user_name", new UsernameColumnWriter() },
                             { "machine_name", new MachinenameColumnWriter() },
                         })
                .WriteTo.Seq(SeqConnection, restrictedToMinimumLevel: LogEventLevel.Information)
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .CreateLogger();




            return log;
        }


        public static void AddRateLimiterServiceExtension(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                {

                    if (httpContext.Request.Path.StartsWithSegments("/api/Auth/login"))
                    {
                        return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
                            new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = 1,
                                AutoReplenishment = true,
                                Window = TimeSpan.FromSeconds(1)
                            });
                    }



                    return RateLimitPartition.GetFixedWindowLimiter(partitionKey: "default", partition =>
                        new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = int.MaxValue,
                            AutoReplenishment = true,
                            Window = TimeSpan.FromSeconds(1)
                        });
                });

                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = 429;
                    await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later... ", cancellationToken: token);
                };
            });
        }

    }
}
