using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Test;

namespace IdentityServer.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserInfo
    {
        public int Id { get; set; }

        //
        // Summary:
        //     Gets or sets the subject identifier.
        public string SubjectId { get; set; }
        //
        // Summary:
        //     Gets or sets the username.
        public string Username { get; set; }
        //
        // Summary:
        //     Gets or sets the password.
        public string Password { get; set; }
        //
        // Summary:
        //     Gets or sets the provider name.
        public string ProviderName { get; set; }
        //
        // Summary:
        //     Gets or sets the provider subject identifier.
        public string ProviderSubjectId { get; set; }
        //
        // Summary:
        //     Gets or sets if the user is active.
        public bool IsActive { get; set; }
        //
        // Summary:
        //     Gets or sets the claims.
        public ICollection<UserClaim> Claims { get; set; }

        public List<Role> Roles { get; set; }
        public List<Client> Clients { get; set; }
        public List<ApiResource> ApiResources { get; set; }
        public List<ApiScope> ApiScopes { get; set; }
    }
}