using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using IdentityServerHost.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerAspNetIdentity.Controllers
{
    public static class UserExtend
    {
        public static Expression<Func<ApplicationUser, bool>> Filter(this IQueryable<ApplicationUser> e, string keyword)
        {
            return x =>
                string.IsNullOrWhiteSpace(keyword)
                || x.Id == keyword
                || x.UserName.Contains(keyword)
                || x.Email.Contains(keyword)
                || x.PhoneNumber.Contains(keyword);
        }
    }

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
        private Expression<Func<ApplicationUser, bool>> DefaultFilter(string keyword)
        {
            return x =>
                string.IsNullOrWhiteSpace(keyword)
                || x.Id == keyword
                || x.UserName.Contains(keyword)
                || x.Email.Contains(keyword)
                || x.PhoneNumber.Contains(keyword);
        }

        [HttpGet]
        public List<ApplicationUser> Get(
            [MinLength(2)] string keyword
            , [Required, Range(1, 1000)] int page = 1
            , [Required, Range(1, 1000)] int limit = 10
        )
        {
            var users = _userManager.Users
                .Where(DefaultFilter(keyword))
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToList();

            return users;
        }

        [HttpGet("[controller]")]
        public int Count(
            [MinLength(2)] string keyword
        )
        {
            return _userManager.Users.Count(DefaultFilter(keyword));
        }

        [HttpPost]
        public void Create(ApplicationUser user, string password)
        {
            _userManager.CreateAsync(user, password);
        }
    }
}