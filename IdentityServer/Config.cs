﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
   public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { 
                new ApiScope("apiCreditCard", "Credit Card API")

            };

       public static IEnumerable<Client> Clients =>
    new List<Client>
    {
        new Client
        {
            ClientId = "clientCreditCard",

            // no interactive user, use the clientid/secret for authentication
            AllowedGrantTypes = GrantTypes.ClientCredentials,

            // secret for authentication
            ClientSecrets =
            {
                new Secret("secret17july".Sha256())//password
            },

            // scopes that client has access to
            AllowedScopes = { "apiCreditCard" }//login
        }
    };
    }
}