using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace HRelloApi;

public class IdentityConfiguration
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("Api", "Web API")
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("Api", "Web API", new []
                { JwtClaimTypes.Name})
            {
                Scopes = {"write", "read"},
                ApiSecrets = new List<Secret>
                {
                    new Secret("secret")
                }
            }
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                //IncludeJwtId = true,
                ClientId = "client",
                ClientName = "Hrello Web",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedGrantTypes = new[] { OidcConstants.GrantTypes.JwtBearer, OidcConstants.GrantTypes.Password, OidcConstants.GrantTypes.RefreshToken, OidcConstants.GrantTypes.ClientCredentials },
                RequireClientSecret = false,
                RequirePkce = true,
                /*RedirectUris =
                {
                    "http://localhost:5020/signin-oidc"
                },*/
                /*AllowedCorsOrigins =
                {
                    "http://connect/token"
                },*/
                /*PostLogoutRedirectUris =
                {
                    "http:/localhost:5020/signout-oidc"
                },*/
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    //IdentityServerConstants.StandardScopes.Email,
                    "HRello",
                    "Api"
                },
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,
                AccessTokenType = AccessTokenType.Jwt
            }
        };
}