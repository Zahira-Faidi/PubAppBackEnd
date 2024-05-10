using System.Collections.Generic;
using IdentityServer4.Models;

namespace IS.Settings
{
    public class IdentityServerSettings
    {
        public IReadOnlyCollection<ApiScope>? ApiScopes { get; init; }
        public IReadOnlyCollection<ApiResource>? ApiResources { get; init; }
        public IReadOnlyCollection<Client>? Clients { get; init; }

        // Include user claims in IdentityResources
        public IReadOnlyCollection<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", "User role(s)", new List<string> { "role" }),
            };
    }
}
