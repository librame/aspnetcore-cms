#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Content.Api.Types
{
    using AspNetCore.Api.Types;
    using AspNetCore.Content.Api.Models;

    /// <summary>
    /// 窗格声明类型。
    /// </summary>
    public class PaneClaimType : ApiTypeBase<PaneClaimModel>
    {
        /// <summary>
        /// 构造一个窗格声明类型。
        /// </summary>
        public PaneClaimType()
            : base()
        {
            Field(f => f.Id);
            Field(f => f.ClaimValue);

            Field(f => f.Pane, type: typeof(PaneType), nullable: true);
            Field(f => f.Claim, type: typeof(UnitType), nullable: true);
        }

    }
}
