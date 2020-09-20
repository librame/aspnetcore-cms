#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;

namespace Librame.AspNetCore.Content.Api.Types
{
    using AspNetCore.Api.Types;
    using AspNetCore.Content.Api.Models;

    /// <summary>
    /// 窗格单元类型。
    /// </summary>
    public class PaneUnitType : ApiTypeBase<PaneUnitModel>
    {
        /// <summary>
        /// 构造一个窗格单元类型。
        /// </summary>
        public PaneUnitType()
            : base()
        {
            Field(f => f.Pane, type: typeof(PaneType));
            Field(f => f.Units, type: typeof(ListGraphType<UnitType>));
        }

    }
}
