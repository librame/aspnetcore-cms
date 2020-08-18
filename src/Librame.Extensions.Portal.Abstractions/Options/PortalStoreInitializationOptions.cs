#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.Extensions.Portal.Options
{
    /// <summary>
    /// 门户存储初始化选项。
    /// </summary>
    public class PortalStoreInitializationOptions
    {
        /// <summary>
        /// 默认编者字典集合。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public Dictionary<string, string> DefaultEditors { get; set; }
            = new Dictionary<string, string>
            {
                { "测编", "测试编者" }
            };

        /// <summary>
        /// 默认标签列表集合。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public List<string> DefaultInternalUserNames { get; set; }
            = new List<string>
            {
                "admin"
            };

        /// <summary>
        /// 默认密码。
        /// </summary>
        public string DefaultPassword { get; set; }
            = "admin666";

    }
}
