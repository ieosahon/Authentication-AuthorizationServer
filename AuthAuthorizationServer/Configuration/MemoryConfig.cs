using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

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
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api" }
                }
            };

        public static IEnumerable<ApiResource> ApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("api", "My API")
            };

        public static IList<TestUser> TestUsers() =>

            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password"
                }
            };

        public static IEnumerable<ApiScope> ApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope("api", "My API")
            };

    }
}

