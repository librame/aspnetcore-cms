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
    /// 门户来源资源。
    /// </summary>
    public class PortalSourceResource : IResource
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
        /// 标志。
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 链接。
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 备注。
        /// </summary>
        public string Descr { get; set; }
    }
}
