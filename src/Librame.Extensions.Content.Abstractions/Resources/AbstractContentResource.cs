﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Content.Resources
{
    using Core.Resources;

    /// <summary>
    /// 抽象内容资源。
    /// </summary>
    public class AbstractContentResource : IResource
    {
        /// <summary>
        /// 标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 副标题。
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// 标签集合。
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 类别标识。
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 来源标识。
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 窗格标识。
        /// </summary>
        public string PaneId { get; set; }

        /// <summary>
        /// 单元标识。
        /// </summary>
        public string UnitId { get; set; }

        /// <summary>
        /// 声明标识。
        /// </summary>
        public string ClaimId { get; set; }

        /// <summary>
        /// 声明值。
        /// </summary>
        public string ClaimValue { get; set; }

        /// <summary>
        /// 引用源。
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 网站。
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// 网标。
        /// </summary>
        public string Weblogo { get; set; }

        /// <summary>
        /// 图标。
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 更多。
        /// </summary>
        public string More { get; set; }
    }
}
