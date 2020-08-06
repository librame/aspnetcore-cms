#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Portal.Builders
{
    using Data.Builders;
    using Portal.Options;

    /// <summary>
    /// 门户构建器选项。
    /// </summary>
    public class PortalBuilderOptions : AbstractDataBuilderOptions<PortalStoreOptions, PortalTableOptions>
    {
        /// <summary>
        /// 支持内置用户。
        /// </summary>
        public bool SupportInternalUser { get; set; }
    }
}
