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

namespace Librame.Extensions.Content.Options
{
    using Data;
    using Data.Options;

    /// <summary>
    /// 内容表名选项。
    /// </summary>
    public class ContentTableOptions : AbstractTableOptions
    {
        /// <summary>
        /// 使用 Content 前缀（默认使用）。
        /// </summary>
        public bool UseContentPrefix { get; set; }
            = true;


        /// <summary>
        /// 内容分类表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> Category { get; set; }

        /// <summary>
        /// 内容来源表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> Source { get; set; }

        /// <summary>
        /// 内容声明表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> Claim { get; set; }

        /// <summary>
        /// 内容标签表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> Tag { get; set; }

        /// <summary>
        /// 内容单元表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> Unit { get; set; }

        /// <summary>
        /// 内容单元声明表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> UnitClaim { get; set; }

        /// <summary>
        /// 内容单元标签表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> UnitTag { get; set; }

        /// <summary>
        /// 内容单元访问计数表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> UnitVisitCount { get; set; }

        /// <summary>
        /// 内容窗格表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> Pane { get; set; }

        /// <summary>
        /// 内容窗格单元表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> PaneUnit { get; set; }
    }
}
