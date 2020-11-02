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
    /// 单元声明模型类型。
    /// </summary>
    public class UnitClaimModelType : IdentifierModelTypeBase<UnitClaimModel>
    {
        /// <summary>
        /// 构造一个 <see cref="UnitClaimModelType"/>。
        /// </summary>
        public UnitClaimModelType()
            : base()
        {
            Field(f => f.ClaimValue);

            Field(f => f.Unit, type: typeof(UnitModelType), nullable: true);
            Field(f => f.Claim, type: typeof(ClaimModelType), nullable: true);
        }

    }
}
