using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IdentityServerAspNetIdentity.Models
{
    public class DefualtQueryRequest
    {
        /// <summary>
        /// 关键词
        /// </summary>
        [StringLength(200, MinimumLength = 2)]
        public string Keyword { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        [Required, Range(1, 1000)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// 分页大小
        /// </summary>
        [Required, Range(1, 1000)]
        public int Limit { get; set; } = 10;

        /// <summary>
        /// 使用 Page 和 Limit 计算出需要 跳过的文档数量
        /// </summary>
        /// <returns></returns>
        [BindNever]
        public int Skip => (Page - 1) * Limit;
    }
}