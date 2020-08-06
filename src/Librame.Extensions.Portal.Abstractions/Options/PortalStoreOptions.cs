#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Portal.Options
{
    using Data.Options;

    /// <summary>
    /// 门户存储选项。
    /// </summary>
    public class PortalStoreOptions : AbstractStoreOptions
    {
        /// <summary>
        /// 初始化选项。
        /// </summary>
        public PortalStoreInitializationOptions Initialization { get; set; }
            = new PortalStoreInitializationOptions();
    }
}
