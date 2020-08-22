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
    /// 声明类型。
    /// </summary>
    public class ClaimType : ApiTypeBase<ClaimModel>
    {
        /// <summary>
        /// 构造一个声明类型。
        /// </summary>
        public ClaimType()
            : base()
        {
            Field(f => f.Id);
            Field(f => f.Name);
            Field(f => f.Description);
            Field(f => f.CreatedTime);
            Field(f => f.CreatedBy);

            Field(f => f.Category, type: typeof(CategoryType));
        }

    }
}
