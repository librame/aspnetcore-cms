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
    /// <summary>
    /// 窗格声明模型。
    /// </summary>
    public class PaneClaimModel : AbstractIdentifierModel
    {
        /// <summary>
        /// 声明值。
        /// </summary>
        public string ClaimValue { get; set; }


        /// <summary>
        /// 父级模型。
        /// </summary>
        public PaneModel Pane { get; set; }

        /// <summary>
        /// 声明模型。
        /// </summary>
        public ClaimModel Claim { get; set; }
    }
}
