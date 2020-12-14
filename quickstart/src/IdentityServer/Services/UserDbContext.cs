using IdentityServer.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Services
{
    public class UserDbContext : ConfigurationDbContext<UserDbContext>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {
        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}