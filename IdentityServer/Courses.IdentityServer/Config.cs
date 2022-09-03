// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Courses.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermission"}},
            new ApiResource("resource_basket"){Scopes={"basket_fullpermission"}},
            new ApiResource("resource_order"){Scopes={"order_fullpermission"}},
            new ApiResource("resource_fake_payment"){Scopes={"fake_payment_fullpermission"}},
            new ApiResource("resource_discount"){Scopes={"discount_fullpermission"/*, "discount_readpermission", "discount_writepermission"*/}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       //id bilgisi, subkeyword dolu olması gerekiyor. OpenId mutlak olmalı.
                       //OpenId Connect Protokolünün zorunlu kıldığı bir alan
                       new IdentityResources.OpenId(),
                       //Profil bilgileri adres bilgileri vs.
                       new IdentityResources.Profile(),
                       new IdentityResource(){ Name= "roles", DisplayName = "Roles", Description = "Kullanıcı Rolleri", UserClaims = new []{ "role"} }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {   new ApiScope("catalog_fullpermission", "Catalog API için full erişim"),
                new ApiScope("photo_stock_fullpermission", "Photo Stock API için full erişim"),
                new ApiScope("basket_fullpermission", "Basket API için full erişim"),
                new ApiScope("discount_fullpermission", "Discount API için full erişim"),
                new ApiScope("order_fullpermission", "Order API için full erişim"),
                new ApiScope("fake_payment_fullpermission", "Fake Payment API için full erişim"),
                //new ApiScope("discount_readpermission", "Discount API için full erişim"),
                //new ApiScope("discount_writepermission", "Discount API için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClient",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "catalog_fullpermission", "photo_stock_fullpermission", IdentityServerConstants.LocalApi.ScopeName }
                },

                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClientForUser",
                    AllowOfflineAccess = true, //refresh token izin veriyoruz.
                    ClientSecrets = {new Secret("secret".Sha256())},
                    //dipnot=> ResourceOwnerPasswordAndClientCredentials refresh token yoktur.
                    //Bu nedenle ResourceOwnerPassword kullanıyoruz.
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "basket_fullpermission",
                                      "discount_fullpermission",
                                      "order_fullpermission",
                                      "fake_payment_fullpermission",
                                      IdentityServerConstants.StandardScopes.Email,
                                      IdentityServerConstants.StandardScopes.OpenId,
                                      IdentityServerConstants.StandardScopes.Profile,
                                      IdentityServerConstants.LocalApi.ScopeName,
                                      IdentityServerConstants.StandardScopes.OfflineAccess, // refresh token için
                                      "roles"},
                    //1 Saat olarak tanımlıyoruz.
                    AccessTokenLifetime = 1*60*60, //default 1 saat biz bunu sn cinsinden belirtiyoruz.
                    //Tarih belirtiyoruz. Kesin bir tarih veriyoruz.
                    RefreshTokenExpiration = TokenExpiration.Absolute, //tarih mi belirticez yoksa yenileyecek mi durumunu belirtiyoruz.
                    AbsoluteRefreshTokenLifetime =(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds, // token ömrünü belirtiyoruz.
                    //tekrar kullanılabilir diyoruz.
                    RefreshTokenUsage = TokenUsage.ReUse //refresh token tekrar kullanılıp kullanılmayacağını belirtiyorum.

                    //dipnot Token süresini kısa tutmak her zaman iyidir.
                    //Access Token süresi örneğin 1 saat ver Refresh token süresini 20 gün ver.
                    //Sistemde Access Token süren dolsa bile 401 aldığında refresh token üzerinden IdentityServerdan yeni bir token al
                    //kullanıcıya hissettirmeden devam ettir.
                }
            };
    }
}