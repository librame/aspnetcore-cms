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
    /// 登入输入类型。
    /// </summary>
    public class CategoryInputType : ApiInputTypeBase<CategoryModel>
    {
        /// <summary>
        /// 构造一个 <see cref="CategoryInputType"/>。
        /// </summary>
        public CategoryInputType()
            : base()
        {
            Field(f => f.Name);
            Field(f => f.Description);
            Field(f => f.Parent, nullable: true);
        }

    }
}
