#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Portal
{
    using Extensions.Core;

    /// <summary>
    /// 门户窗格资源。
    /// </summary>
    public class PortalPaneResource : IResource
    {
        /// <summary>
        /// 分类标识。
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路径。
        /// </summary>
        public string Path { get; set; }
    }
}
