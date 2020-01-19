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
    /// 门户标签引用资源。
    /// </summary>
    public class PortalTagReferenceResource : IResource
    {
        /// <summary>
        /// 标签标识。
        /// </summary>
        public string TagId { get; set; }

        /// <summary>
        /// 引用实体标识。
        /// </summary>
        public string ReferEntityId { get; set; }

        /// <summary>
        /// 引用标识。
        /// </summary>
        public string ReferId { get; set; }

        /// <summary>
        /// 引用 URL。
        /// </summary>
        public string ReferUrl { get; set; }
    }
}
