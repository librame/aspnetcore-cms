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
    /// 门户分类资源。
    /// </summary>
    public class PortalCategoryResource : IResource
    {
        /// <summary>
        /// 父标识。
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 说明。
        /// </summary>
        public string Descr { get; set; }
    }
}
