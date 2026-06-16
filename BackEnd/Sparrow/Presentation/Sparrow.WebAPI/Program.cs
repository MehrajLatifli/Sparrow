
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using Sparrow.Application.Mapper;
using Sparrow.Persistence.ServiceExtensions;
using Sparrow.WebAPI.Middlewares;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using static Sparrow.Persistence.ServiceExtensions.ServiceExtension;

namespace Sparrow.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                              .SetIsOriginAllowed(hostName => true);

                });
            });



            builder.Host.UseSerilog(builder.Services.AddCustomSerilog(builder.Configuration.GetConnectionString("LogConnection"), builder.Configuration["Seq:SeqConnection"]));


            builder.Services.APIVersion();
            builder.Services.AddSwaggerGenServiceExtension();
            builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();



            builder.Services.AddRateLimiterServiceExtension();


            builder.Services.AddJwtAuthentication(builder.Configuration);


            builder.Services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.AddBlobServiceClient(builder.Configuration["local-1:blob"]);
                clientBuilder.AddQueueServiceClient(builder.Configuration["local-1:queue"]);
            });

            builder.Services.AddAzureClients(clientBuilder =>
            {
                var conn = builder.Configuration["ConnectionAzureStorage"];

                if (string.IsNullOrEmpty(conn))
                    throw new Exception("Azure connection string is missing!");

                clientBuilder.AddBlobServiceClient(conn);
                clientBuilder.AddQueueServiceClient(conn);
            });

            builder.Services.AddTransient<ExceptionMiddleware>();

            builder.Services.AddHttpClient();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());
            builder.Services.AddRedisConfiguration(builder.Configuration);


            builder.Services.AddPersistenceServices(builder.Configuration);



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });
            }

            app.UseHttpsRedirection();


            app.UseCors("AllowAllPolicy");

            app.UseRateLimiter();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllers();

            app.MapControllerRoute(name: "default", pattern: "{Interview}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
