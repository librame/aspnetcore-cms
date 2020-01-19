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
    /// 门户专题主体资源。
    /// </summary>
    public class PortalSubjectBodyResource : IResource
    {
        /// <summary>
        /// 专题标识。
        /// </summary>
        public string SubjectId { get; set; }

        /// <summary>
        /// 文本散列。
        /// </summary>
        public string TextHash { get; set; }

        /// <summary>
        /// 文本。
        /// </summary>
        public string Text { get; set; }
    }
}
