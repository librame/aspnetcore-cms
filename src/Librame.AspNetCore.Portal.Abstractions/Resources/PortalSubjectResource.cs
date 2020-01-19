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
    /// 门户专题资源。
    /// </summary>
    public class PortalSubjectResource : IResource
    {
        /// <summary>
        /// 分类标识。
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 发布时间。
        /// </summary>
        public string PublishTime { get; set; }

        /// <summary>
        /// 发布链接。
        /// </summary>
        public string PublishLink { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 副标题。
        /// </summary>
        public string Subtitle { get; set; }
    }
}
