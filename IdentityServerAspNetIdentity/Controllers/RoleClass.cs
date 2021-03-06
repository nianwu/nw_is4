using System;
using System.Collections;
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
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private RoleManager<IdentityRole> _roleManager;

        public RolesController(
            RoleManager<IdentityRole> roles
        )
        {
            _roleManager = roles;
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="roleRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> CreateAsync(
            [FromBody, Required] RolesCreateRequest roleRequest
        )
        {
            var role = new IdentityRole
            {
                Name = roleRequest.Name,
                NormalizedName = roleRequest.NormalizedName
            };

            var createResult = await _roleManager.CreateAsync(role);

            // TODO: 成功检查 from yuanzhiyuan@aimiaobi.com 2021-02-12 01:21:18
            // TODO: 抛出返回结果中的异常 from yuanzhiyuan@aimiaobi.com 2021-02-12 01:21:25

            return role.Id;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [HttpPost("{id}/[action]")]
        public async Task<ActionResult> DeleteAsync(
            [FromRoute, Required] string id
        )
        {
            var role = _roleManager.Roles
                // TODO: project from yuanzhiyuan@aimiaobi.com 2021-02-12 00:58:02
                .FirstOrDefault(DefaultFilter(id: id));

            if (role == default)
            {
                return NotFound();
            }

            var deleteResult = await _roleManager.DeleteAsync(role);

            // TODO: 成功检查 from yuanzhiyuan@aimiaobi.com 2021-02-12 01:21:18
            // TODO: 抛出返回结果中的异常 from yuanzhiyuan@aimiaobi.com 2021-02-12 01:21:25

            return Ok();
        }

        private Expression<Func<IdentityRole, bool>> DefaultFilter(
            string keyword = null
            , string id = null
        )
        {
            return x =>
                string.IsNullOrWhiteSpace(keyword)
                    || x.Id == keyword
                    || x.Name.Contains(keyword)
                    || x.NormalizedName.Contains(keyword)
                && string.IsNullOrWhiteSpace(id)
                    || x.Id == id
                ;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="queryRequest"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public List<IdentityRole> Get(
            [FromQuery, Required] DefualtQueryRequest queryRequest
            , [FromQuery] string id
        )
        {
            var result = _roleManager.Roles
                .Where(DefaultFilter(queryRequest.Keyword, id))
                .Skip(queryRequest.Skip)
                .Take(queryRequest.Limit)
                .ToList();

            return result;
        }


        /// <summary>
        /// 获取角色数量
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public long Count(
            string keyword = null
        )
        {
            var result = _roleManager.Roles
                .LongCount(DefaultFilter(keyword));

            return result;
        }

        [Obsolete("研发中")]
        [HttpPost("{id}")]
        public ActionResult Update(
           [FromRoute, Required] string id
        )
        {
            throw new NotImplementedException();
        }
    }
}