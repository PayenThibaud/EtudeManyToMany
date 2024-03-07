using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using EtudeManyToMany.API.Data;
using EtudeManyToMany.Core.Model;
using EtudeManyToMany.API.Repository;
using EtudeManyToMany.API.Helpers;
using Constants = EtudeManyToMany.API.Helpers.Constants;

namespace EcoRideAPI.Extensions
{
    public static class DependencyInjectionsExtensions
    {
        public static void InjectDependancies(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddJsonOptions(x =>
                            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.AddSwagger();

            builder.AddDatabase();

            builder.AddRepositories();

            builder.AddAuthentication();

            builder.AddAuthorization();

        }

        private static void AddSwagger(this WebApplicationBuilder builder)
        {

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EtudeManyToMany", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.Http
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                        },
                        new string[] { }
                    }
                });
            });
        }



        private static void AddDatabase(this WebApplicationBuilder builder)
        {
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        }

        private static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRepository<Utilisateur>, UtilisateurRepository>();
            builder.Services.AddScoped<IRepository<Conducteur>, ConducteurRepository>();
            builder.Services.AddScoped<IRepository<Passager>, PassagerRepository>();
            builder.Services.AddScoped<IRepository<Trajet>, TrajetRepository>();
            builder.Services.AddScoped<IRepository<Reservation>, ReservationRepository>();
        }

        private static void AddAuthentication(this WebApplicationBuilder builder)
        {
            var appSettingsSection = builder.Configuration.GetSection(nameof(AppSettings));
            builder.Services.Configure<AppSettings>(appSettingsSection); // service IOptions<Appsettings>
            AppSettings appSettings = appSettingsSection.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey!);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true, // utilisation d'une clé cryptée pour la sécurité du token
                        IssuerSigningKey = new SymmetricSecurityKey(key), // clé cryptée en elle même
                        ValidateLifetime = true, // vérification du temps d'expiration du Token
                        ValidateAudience = true, // vérification de l'audience du token
                        ValidAudience = appSettings.ValidAudience, // l'audience
                        ValidateIssuer = true, // vérification du donneur du token
                        ValidIssuer = appSettings.ValidIssuer, // le donneur
                        ClockSkew = TimeSpan.Zero // décallage possible de l'expiration du token
                    };
                });
        }

        private static void AddAuthorization(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {

                options.AddPolicy(Constants.PolicyAdmin, police =>
                {
                    police.RequireClaim(ClaimTypes.Role, Constants.RoleAdmin);
                });
                options.AddPolicy(Constants.PolicyUser, police =>
                {
                    police.RequireClaim(ClaimTypes.Role, Constants.RoleUser);
                });
            });
        }
    }
}