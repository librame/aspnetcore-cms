#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.Extensions.Portal.Options
{
    using Data;
    using Data.Options;

    /// <summary>
    /// 门户表名选项。
    /// </summary>
    public class PortalTableOptions : AbstractTableOptions
    {
        /// <summary>
        /// 使用 Portal 前缀（默认使用）。
        /// </summary>
        public bool UsePortalPrefix { get; set; }
            = true;


        /// <summary>
        /// 编者表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> Editor { get; set; }

        /// <summary>
        /// 内置用户表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> InternalUser { get; set; }
    }
}
