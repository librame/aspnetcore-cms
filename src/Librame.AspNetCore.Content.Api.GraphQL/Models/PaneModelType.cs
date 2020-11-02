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

namespace Librame.AspNetCore.Content.Api.Models
{
    using AspNetCore.Api.Models;

    /// <summary>
    /// 窗格模型类型。
    /// </summary>
    public class PaneModelType : CreationIdentifierModelTypeBase<PaneModel>
    {
        /// <summary>
        /// 构造一个 <see cref="PaneModelType"/>。
        /// </summary>
        public PaneModelType()
            : base()
        {
            Field(f => f.Name);
            Field(f => f.Description);
            Field(f => f.Icon);
            Field(f => f.More);

            Field(f => f.Parent, type: typeof(PaneModelType), nullable: true);
            Field(f => f.PaneClaims, type: typeof(ListGraphType<PaneClaimModelType>), nullable: true);
        }

    }
}
