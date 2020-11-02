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
    /// 声明模型类型。
    /// </summary>
    public class ClaimModelType : CreationIdentifierModelTypeBase<ClaimModel>
    {
        /// <summary>
        /// 构造一个 <see cref="ClaimModelType"/>。
        /// </summary>
        public ClaimModelType()
            : base()
        {
            Field(f => f.Name);
            Field(f => f.Description);

            Field(f => f.Category, type: typeof(CategoryModelType), nullable: true);
        }

    }
}
