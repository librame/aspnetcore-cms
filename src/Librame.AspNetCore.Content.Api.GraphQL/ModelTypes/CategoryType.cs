#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Content.Api.ModelTypes
{
    using AspNetCore.Api;
    using AspNetCore.Content.Api.Models;

    /// <summary>
    /// 分类类型。
    /// </summary>
    public class CategoryType : ApiModelGraphTypeBase<CategoryApiModel>
    {
        /// <summary>
        /// 构造一个 <see cref="CategoryType"/> 实例。
        /// </summary>
        public CategoryType()
            : base()
        {
            this.AddCategoryApiModelFields();
        }

    }
}
