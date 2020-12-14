using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Services;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Controllers
{
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private UserDbContext _userDb;

        public ClientsController(
            UserDbContext userDb
        )
        {
            _userDb = userDb;
        }

        public List<Client> Get()
        {
            var clients = _userDb
                .UserInfos
                    .Include(b => b.Clients)
                .Single(x => x.Username == User.Identity.Name)
                .Clients
                .ToList();

            return clients.Select(x => x.ToModel()).ToList();
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(
            int id
        )
        {
            var user = await _userDb.UserInfos
                .Include(x => x.Clients)
                .SingleAsync(x => x.Username == User.Identity.Name);

            user.Clients.RemoveAll(x => x.Id == id);

            await _userDb.SaveChangesAsync();
        }
    }
}