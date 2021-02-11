
using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Models
{

    public class UsersCreateRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(100, MinimumLength = 3)]
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        /// <value></value>
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
