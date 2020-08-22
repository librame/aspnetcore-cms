#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Content.Api.Models
{
    /// <summary>
    /// 标签模型。
    /// </summary>
    public class TagModel
    {
        /// <summary>
        /// 标识。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        public string CreatedTime { get; set; }

        /// <summary>
        /// 创建者。
        /// </summary>
        public string CreatedBy { get; set; }
    }
}
