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
    /// 单元声明类型。
    /// </summary>
    public class UnitClaimType : ApiTypeBase<UnitClaimModel>
    {
        /// <summary>
        /// 构造一个单元声明类型。
        /// </summary>
        public UnitClaimType()
            : base()
        {
            Field(f => f.Id);
            Field(f => f.ClaimValue);

            Field(f => f.Unit, type: typeof(UnitType), nullable: true);
            Field(f => f.Claim, type: typeof(ClaimType), nullable: true);
        }

    }
}
