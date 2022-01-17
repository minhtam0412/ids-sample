// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdService
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("test.api"),
                new ApiScope("identity.api"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // MVC client using hybrid flow
                new Client
                {
                    ClientId = "webclient",
                    ClientName = "Web Client",
                    RequireConsent = false,
                    RequirePkce = false,
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { "https://localhost:5002/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "identity.api","test.api" }
                },
                // Angular
                new Client
                {
                    ClientId = "angular_spa",
                    ClientName = "Angular 4 Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new List<string> { "openid", "profile", "identity.api" ,"test.api"  },
                    RedirectUris = new List<string> { "http://localhost:4200/auth-callback","http://localhost:4200/assets/silent-refresh.html" },
                    PostLogoutRedirectUris = new List<string> { "http://localhost:4200/" },
                    AllowedCorsOrigins = new List<string> { "http://localhost:4200" },
                    AllowAccessTokensViaBrowser = true,

                },
                // Postman with secret
                new Client
                {
                    ClientId = "postman",
                    ClientName = "Postman Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new List<string> { "openid", "profile", "identity.api" ,"test.api", "offline_access"  },
                    ClientSecrets = new []{new Secret("PostmanSecret".Sha256())},
                    AbsoluteRefreshTokenLifetime = 864000,//10 days
                    AllowOfflineAccess = true
                },
                // Postman without secret
                new Client
                {
                    ClientId = "PostmanWithoutSecret",
                    ClientName = "Postman Client Without Secret",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new List<string> { "openid", "profile", "identity.api" ,"test.api", "offline_access"  },
                    AbsoluteRefreshTokenLifetime = 864000,//10 days
                    RequireClientSecret = false,
                    AllowOfflineAccess = true
                }
            };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("identity.api", "Identity API"),
            new ApiResource("test.api","Test API")
        };
    }
}