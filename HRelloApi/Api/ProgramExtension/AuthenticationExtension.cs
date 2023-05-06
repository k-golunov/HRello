using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Dal;
using Dal.Entities;
using Dal.User.Repositories;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using Logic.Managers.Identity;
using Logic.Managers.Identity.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace HRelloApi.ProgramExtension;

/// <summary>
/// 
/// </summary>
public static class AuthenticationExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddIdentitySettings(this IServiceCollection services, ConfigurationManager configuration)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        IdentityModelEventSource.ShowPII = true;
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
            /*.AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWTSettings:Audience"],
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:SecretKey"]))
                };
            })*/.AddIdentityServerAuthentication(options =>
            {
                options.Authority = "https://localhost:5020";
                options.RequireHttpsMetadata = false;
                options.ApiName = "Api";
                options.SupportedTokens = SupportedTokens.Jwt;
                //options.ClaimsIssuer = "issuer";
            })
            ;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //var handler = new HttpClientHandler();
        ///handler.ClientCertificates.Add();
        
        // добавление айдентити, тестовая
        // надо усложнить требования к паролю
        services.AddIdentity<UserDal, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
        
        // конфигурация айдентити
        services.AddIdentityServer()
            .AddAspNetIdentity<UserDal>()
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
            .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
            .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
            .AddInMemoryClients(IdentityConfiguration.Clients)
            .AddProfileService<IdentityProfileService>();
        
        services.AddScoped<RoleManager<IdentityRole>>();
        services.AddScoped<UserManager<UserDal>>();
        services.AddScoped<UserRepository>();
        services.AddTransient<IdentityHelper>();
        services.AddHttpContextAccessor();
        
        //services.AddSingleton<IProfileService, IdentityProfileService>();
        
        return services;
    }
}