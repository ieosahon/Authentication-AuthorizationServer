using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthAuthorizationServer.Configuration
{
    public class MemoryConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),// user/subject Id
                new IdentityResources.Profile(),// given and family names of the user
            };
        public static IEnumerable<Client> Clients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("osahonSecret".Sha512())
                    },
                    AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, "jobsApi.scope" }
                }
               /* ,
                
                new Client
                {
                    ClientId = "second-client",
                    ClientSecrets =
                    {
                        new Secret("ighodaroSecret".Sha512())
                    },
                    AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId}
                }*/
            };

        public static IEnumerable<ApiResource> ApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("jobsApi", "Jobs API")
                {
                    Scopes = { "jobsApi.scope" }
                }
            };

        public static List<TestUser> TestUsers() =>

            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "bob1",
                    Password = "password1",
                    Claims = new List<Claim>
                    {
                        new Claim ("given_name", "osahon1"),
                        new Claim ("family_name", "ighodaro1")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob2",
                    Password = "password2",
                    Claims = new List<Claim>
                    {
                        new Claim ("given_name", "osahon2"),
                        new Claim ("family_name", "ighodaro2")
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope("jobsApi.scope", "Jobs API")
            };

    }
}

