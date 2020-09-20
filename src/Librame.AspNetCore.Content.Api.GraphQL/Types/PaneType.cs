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
    /// 窗格类型。
    /// </summary>
    public class PaneType : ApiTypeBase<PaneModel>
    {
        /// <summary>
        /// 构造一个窗格类型。
        /// </summary>
        public PaneType()
            : base()
        {
            Field(f => f.Id);
            Field(f => f.Name);
            Field(f => f.Description);
            Field(f => f.Icon);
            Field(f => f.More);
            Field(f => f.CreatedTime);
            Field(f => f.CreatedBy);

            Field(f => f.Parent, type: typeof(PaneType), nullable: true);
            Field(f => f.PaneClaims, type: typeof(ListGraphType<PaneClaimType>), nullable: true);
        }

    }
}
