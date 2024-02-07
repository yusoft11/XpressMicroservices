using CategoryMicroservices.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MS.API.Common;
using Serilog;
using System.Text;

namespace CategoryMicroservices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
            var logPath = $@"{appSettings.LogFolder}\CategoryServices-.txt";
            Log.Logger = new LoggerConfiguration()
                 .Enrich.FromLogContext()
                 .MinimumLevel.Debug()
                 .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Error)
                 .MinimumLevel.Override("Microsoft.EntityFrameworkCore.SqlServer", Serilog.Events.LogEventLevel.Error)
                 .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Error)
                 .MinimumLevel.Override("ccp.api.Models.PortalDbContext", Serilog.Events.LogEventLevel.Error)
                 .WriteTo.File(logPath, fileSizeLimitBytes: 15_000_000, rollOnFileSizeLimit: true, shared: true, rollingInterval: RollingInterval.Day, flushToDiskInterval: TimeSpan.FromSeconds(1))
                 .CreateLogger();
            builder.Host.UseSerilog();

            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IMicroServiceManager, MicroServiceManager>();
            builder.Services.AddScoped<IHttpClientUtil, HttpClientUtil>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (appSettings.LoadOnSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

    }
}



