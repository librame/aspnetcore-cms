#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Content.Api.Models
{
    using AspNetCore.Api.Models;

    /// <summary>
    /// 窗格声明模型类型。
    /// </summary>
    public class PaneClaimModelType : IdentifierModelTypeBase<PaneClaimModel>
    {
        /// <summary>
        /// 构造一个 <see cref="PaneClaimModelType"/>。
        /// </summary>
        public PaneClaimModelType()
            : base()
        {
            Field(f => f.ClaimValue);

            Field(f => f.Pane, type: typeof(PaneModelType), nullable: true);
            Field(f => f.Claim, type: typeof(ClaimModelType), nullable: true);
        }

    }
}
