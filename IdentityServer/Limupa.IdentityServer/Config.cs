// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace Limupa.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
           {
                   new ApiResource("ResourceCatalog"){Scopes={"CatalogFullPermission"} },
                   new ApiResource("ResourceDiscount"){Scopes={ "DiscountFullPermission" } },
                   new ApiResource("ResourceOrder"){Scopes={"OrderFullPermission"}},
                   new ApiResource("ResourceCargo"){Scopes={"CargoFullPermission"}},
                   new ApiResource("ResourceBasket"){Scopes={"BasketFullPermission"}},
                   new ApiResource("ResourcePayment"){Scopes={"PaymentFullPermisson"}},
                   new ApiResource("ResourceOcelot"){Scopes={"OcelotFullPermisson"}},
                   new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
           };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
                  new IdentityResources.OpenId(),
                  new IdentityResources.Profile(),
                  new IdentityResources.Email()
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
                    new ApiScope("CatalogFullPermission","Full authority for catalog operations"),
                    new ApiScope("DiscountFullPermission","Full authority for discount operations"),
                    new ApiScope("OrderFullPermission","Full authority for order operations"),
                    new ApiScope("CargoFullPermission","Full authority for cargo operations"),
                    new ApiScope("BasketFullPermission","Full authority for basket operations"),
                    new ApiScope("PaymentFullPermisson","Full authority for payment operations"),
                    new ApiScope("OcelotFullPermisson","Full authority for ocelot operations"),
                    new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<Client> Clients => new Client[]
        {

                    //Member
                    new Client
                    {
                       ClientId="LimupaMemberId",
                       ClientName="Limupa Member User",
                       AllowedGrantTypes=GrantTypes.ClientCredentials,
                       ClientSecrets={new Secret("deneme".Sha256()) },
                       AllowedScopes={"CatalogFullPermission","OcelotFullPermisson","DiscountFullPermission",
                                      "CommentFullPermission","PaymentFullPermisson","BasketFullPermission",
                       IdentityServerConstants.LocalApi.ScopeName}
                    },

                    //Visitor
                    new Client
                    {
                       ClientId="LimupaVisitorId",
                       ClientName="Limupa Visitor User",
                       AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                       ClientSecrets={new Secret("deneme".Sha256()) },
                       AllowedScopes={"CatalogFullPermission","OcelotFullPermisson","DiscountFullPermission",
                                      "BasketFullPermission","PaymentFullPermisson","CargoFullPermission","OrderFullPermission",
                       IdentityServerConstants.LocalApi.ScopeName,
                       IdentityServerConstants.StandardScopes.Email,
                       IdentityServerConstants.StandardScopes.OpenId,
                       IdentityServerConstants.StandardScopes.Profile, }
                    },


                    ////Manager
                    //new Client
                    //{
                    //    ClientId="LimupaManagerId",
                    //    ClientName="Limupa Manager User",
                    //    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    //    ClientSecrets={new Secret("deneme".Sha256()) },
                    //    AllowedScopes={"CatalogFullPermission", "CatalogReadPermission", "CargoFullPermisson", "OcelotFullPermisson","CommentFullPermission",
                    //    IdentityServerConstants.LocalApi.ScopeName,
                    //    IdentityServerConstants.StandardScopes.Email,
                    //    IdentityServerConstants.StandardScopes.OpenId,
                    //    IdentityServerConstants.StandardScopes.Profile}
                    //},

                    // Admin
                    new Client
                    {
                        ClientId="LimupaAdminId",
                        ClientName="Limupa Admin User",
                        AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                        ClientSecrets={new Secret("deneme".Sha256()) },
                        AllowedScopes={"CatalogFullPermission","DiscountFullPermission","OrderFullPermission","CargoFullPermission",
                                       "BasketFullPermission","PaymentFullPermisson","OcelotFullPermisson",

                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                        }

                    }
        };
    }
}
























