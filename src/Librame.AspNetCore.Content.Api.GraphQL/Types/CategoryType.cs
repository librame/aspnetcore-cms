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
    /// 类别类型。
    /// </summary>
    public class CategoryType : ApiTypeBase<CategoryModel>
    {
        /// <summary>
        /// 构造一个类别类型。
        /// </summary>
        public CategoryType()
            : base()
        {
            Field(f => f.Id);
            Field(f => f.Name);
            Field(f => f.Description);
            Field(f => f.CreatedTime);
            Field(f => f.CreatedBy);

            Field(f => f.Parent, type: typeof(CategoryType), nullable: true);
        }

    }
}
