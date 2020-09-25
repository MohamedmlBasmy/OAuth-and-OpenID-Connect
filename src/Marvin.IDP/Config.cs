// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Marvin.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(), // it ensures that the userIdentitfier ex: SubjectId can be requested
                new IdentityResources.Profile(), // this maps to profile related claims like for example given_name, family_name
                new IdentityResources.Address(),
                new IdentityResource("roles", "Your Role (s)", new List<string>{"role"})
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(name: "imagegalleryapi",displayName: "Image Gallary API")
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource{ 
                    Name = "imagegalleryapi",
                    DisplayName = "Image Gallery API",
                    Scopes = new List<string> { "imagegalleryapi"},
                    UserClaims  = new List<string> {"role"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                     ClientName = "Image Gallary",
                     ClientId="imagegallary",
                     AllowedGrantTypes = GrantTypes.Code,
                     RequirePkce = true,
                     AllowedScopes =
                     {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.Address,
                            "roles",
                            "imagegalleryapi"
                     },
                     ClientSecrets = {new Secret("secret".Sha256())},
                     PostLogoutRedirectUris= new List<string>
                     {
                         "https://localhost:44389/signout-callback-oidc"
                     },
                     RedirectUris = new List<string>
                     {
                         //host address of the web application(client)
                         "https://localhost:44389/signin-oidc"
                     }
                }
            };
    }
}