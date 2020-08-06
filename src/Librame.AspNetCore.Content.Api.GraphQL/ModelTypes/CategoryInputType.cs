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
    /// 登入输入类型。
    /// </summary>
    public class CategoryInputType : ApiModelInputGraphTypeBase<CategoryApiModel>
    {
        /// <summary>
        /// 构造一个 <see cref="CategoryInputType"/> 实例。
        /// </summary>
        public CategoryInputType()
            : base()
        {
            Name = GetInputTypeName<CategoryInputType>();

            this.AddCategoryApiModelFields();
        }

    }
}
