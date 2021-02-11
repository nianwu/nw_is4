using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Models;
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

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> CreateAsync(
            [FromBody, Required] UsersCreateRequest userRequest
        )
        {
            var user = new ApplicationUser
            {
                UserName = userRequest.UserName ?? userRequest.Email ?? userRequest.PhoneNumber,
                Email = userRequest.Email,
                PhoneNumber = userRequest.PhoneNumber
            };

            var createResult = await _userManager.CreateAsync(user, userRequest.Password);

            // TODO: 成功检查 from yuanzhiyuan@aimiaobi.com 2021-02-12 01:21:18
            // TODO: 抛出返回结果中的异常 from yuanzhiyuan@aimiaobi.com 2021-02-12 01:21:25

            return user.Id;
        }

        [HttpDelete("{id}")]
        [HttpPost("{id}/[action]")]
        public async Task<ActionResult> DeleteAsync(
            [FromRoute, Required] string id
        )
        {
            var user = _userManager.Users
                // TODO: project from yuanzhiyuan@aimiaobi.com 2021-02-12 00:58:02
                .FirstOrDefault(DefaultFilter(id: id));

            if (user == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);

            // TODO: 成功检查 from yuanzhiyuan@aimiaobi.com 2021-02-12 01:21:18
            // TODO: 抛出返回结果中的异常 from yuanzhiyuan@aimiaobi.com 2021-02-12 01:21:25

            return Ok();
        }

        /// <summary>
        /// 默认的 keyword 过滤器
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private Expression<Func<ApplicationUser, bool>> DefaultFilter(
            string keyword = null
            , string id = null
        )
        {
            return x =>
                string.IsNullOrWhiteSpace(keyword)
                    || x.Id == keyword
                    || x.UserName.Contains(keyword)
                    || x.Email.Contains(keyword)
                    || x.PhoneNumber.Contains(keyword)
                    || x.NormalizedEmail.Contains(keyword)
                    || x.NormalizedUserName.Contains(keyword)
                && string.IsNullOrWhiteSpace(id)
                    || x.Id == id
                ;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ApplicationUser> Get(
            [FromQuery, Required] DefualtQueryRequest request
            , string id = null
        )
        {
            var users = _userManager.Users
                .Where(DefaultFilter(request.Keyword, id))
                .Skip(request.Skip)
                .Take(request.Limit)
                .ToList();

            return users;
        }

        /// <summary>
        /// 获取用户数量
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public long Count(
            [MinLength(2)] string keyword
        )
        {
            var count = _userManager.Users
                .LongCount(DefaultFilter(keyword));

            return count;
        }

        [Obsolete("研发中")]
        [HttpPost("{id}")]
        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}