using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer4Demo.Configuration
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
              new List<IdentityResource>
              {
                  new IdentityResources.OpenId(), // OpenId method, we support a subject id or sub value to be included
                  new IdentityResources.Profile() // Profile method as well to support profile information like given_name or family_name
              };
        public static List<TestUser> GetUsers() =>
              new List<TestUser>
              {
                  new TestUser
                  {
                      SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4", // SubjectId supported by the OpenId IdentityResource 
                      Username = "Mick",
                      Password = "MickPassword",
                      Claims = new List<Claim>
                      {
                          new Claim("given_name", "Mick"),  // given_name and family_name claims supported by the Profile IdentityResource.
                          new Claim("family_name", "Mining")
                      }
                  },
                  new TestUser
                  {
                      SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
                      Username = "Jane",
                      Password = "JanePassword",
                      Claims = new List<Claim>
                      {
                          new Claim("given_name", "Jane"),
                          new Claim("family_name", "Downing")
                      }
                  }
              };
        public static IEnumerable<Client> GetClients() =>
                new List<Client>
                {
                   new Client
                   {
                        ClientId = "company-employee", // we provide the ClientId and the ClientSecret for this client
                        ClientSecrets = new [] { new Secret("codemazesecret".Sha512()) },
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials, // AllowedGrantTypes provides the information about the flow we are going to use to get the token // heto knayenq hybrid@
                        AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "companyApi", "weatherApi" } //provide allowed scopes for the client
                    }
                };
        public static IEnumerable<ApiScope> GetApiScopes() =>
                new List<ApiScope> { new ApiScope("companyApi", "CompanyEmployee API"), new ApiScope("weatherApi", "Weather API") }; // add additional scope to support our API scope.


        public static IEnumerable<ApiResource> GetApiResources() => // we are going to support our API resource 
                new List<ApiResource>
                {
                    new ApiResource("companyApi", "CompanyEmployee API")
                    {
                        Scopes = { "companyApi" }
                    },
                    new ApiResource("weatherApi", "Weather API")
                    {
                        Scopes = { "weatherApi" }
                    }
                };
    }
}
