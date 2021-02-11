using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Models
{
    public class RolesCreateRequest
    {
        [Required]
        // TODO: 补充长度验证 from yuanzhiyuan@aimiaobi.com 2021-02-12 01:17:42
        public string Name { get; set; }

        // TODO: 补充模型验证 from yuanzhiyuan@aimiaobi.com 2021-02-12 01:17:24
        public string NormalizedName { get; internal set; }
    }
}
