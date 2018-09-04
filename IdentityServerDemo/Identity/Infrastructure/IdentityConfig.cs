using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.Infrastructure
{
    public partial class IdentityConfig
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var resources = new List<ApiResource>
            {
                new ApiResource("API", "Test Api")
            };

            return resources;
        }

        public static IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>();

            clients.Add(new Client
            {
                ClientId = "ClientID",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("abc".Sha256(), "Secret") },
                AllowedScopes = { "API" }
            });

            return clients;
        }
    }
}
