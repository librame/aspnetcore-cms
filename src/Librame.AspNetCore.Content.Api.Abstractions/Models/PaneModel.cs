#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;

namespace Librame.AspNetCore.Content.Api.Models
{
    /// <summary>
    /// 窗格模型。
    /// </summary>
    public class PaneModel
    {
        /// <summary>
        /// 标识。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图标。
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 更多。
        /// </summary>
        public string More { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        public string CreatedTime { get; set; }

        /// <summary>
        /// 创建者。
        /// </summary>
        public string CreatedBy { get; set; }


        /// <summary>
        /// 父级模型。
        /// </summary>
        public PaneModel Parent { get; set; }

        /// <summary>
        /// 单元模型列表。
        /// </summary>
        public IReadOnlyList<UnitModel> Units { get; set; }
    }
}
