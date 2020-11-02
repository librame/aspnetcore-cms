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
    /// 单元声明模型。
    /// </summary>
    public class UnitClaimModel : AbstractIdentifierModel
    {
        /// <summary>
        /// 声明值。
        /// </summary>
        public string ClaimValue { get; set; }


        /// <summary>
        /// 单元模型。
        /// </summary>
        public UnitModel Unit { get; set; }

        /// <summary>
        /// 声明模型。
        /// </summary>
        public ClaimModel Claim { get; set; }
    }
}
