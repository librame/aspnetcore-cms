#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Portal.Resources
{
    using Core.Resources;

    /// <summary>
    /// 抽象门户资源。
    /// </summary>
    public class AbstractPortalResource : IResource
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 肖像。
        /// </summary>
        public string Portrait { get; set; }

        /// <summary>
        /// 用户标识。
        /// </summary>
        public string UserId { get; set; }
    }
}
