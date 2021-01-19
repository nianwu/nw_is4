using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IdentityServerHost.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerAspNetIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(
            UserManager<ApplicationUser> userManager
        )
        {
            this._userManager = userManager;
        }

        [HttpGet]
        public List<ApplicationUser> Get(
            [MinLength(2)] string keyword
            , [Required, Range(1, 1000)] int page = 1
            , [Required, Range(1, 1000)] int limit = 10
        )
        {
            var users = _userManager.Users
                .Where(x =>
                    string.IsNullOrWhiteSpace(keyword)
                    || x.Id == keyword
                    || x.UserName.Contains(keyword)
                    || x.Email.Contains(keyword)
                    || x.PhoneNumber.Contains(keyword)
                )
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToList();

            return users;
        }
    }
}