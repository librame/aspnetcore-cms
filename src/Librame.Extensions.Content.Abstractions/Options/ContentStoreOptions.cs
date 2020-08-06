#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Content.Options
{
    using Data.Options;

    /// <summary>
    /// 内容存储选项。
    /// </summary>
    public class ContentStoreOptions : AbstractStoreOptions
    {
        /// <summary>
        /// 初始化选项。
        /// </summary>
        public ContentStoreInitializationOptions Initialization { get; set; }
            = new ContentStoreInitializationOptions();
    }
}
