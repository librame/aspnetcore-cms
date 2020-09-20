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
    /// 面板单元模型（主要用于首页）。
    /// </summary>
    public class PaneUnitModel
    {
        /// <summary>
        /// 窗格模型。
        /// </summary>
        public PaneModel Pane { get; set; }

        /// <summary>
        /// 单元模型列表。
        /// </summary>
        public IReadOnlyList<UnitModel> Units { get; set; }
    }
}
