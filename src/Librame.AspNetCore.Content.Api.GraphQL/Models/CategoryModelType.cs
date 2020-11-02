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
    /// 类别模型类型。
    /// </summary>
    public class CategoryModelType : CreationIdentifierModelTypeBase<CategoryModel>
    {
        /// <summary>
        /// 构造一个 <see cref="CategoryModelType"/>。
        /// </summary>
        public CategoryModelType()
            : base()
        {
            Field(f => f.Name);
            Field(f => f.Description);

            Field(f => f.Parent, type: typeof(CategoryModelType), nullable: true);
        }

    }
}
