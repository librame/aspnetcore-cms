#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;
using System;

namespace Librame.AspNetCore.Content.Api.StoreTypes
{
    using Extensions.Content.Stores;

    /// <summary>
    /// 内容角色类型。
    /// </summary>
    /// <typeparam name="TCategory">指定的角色类型。</typeparam>
    public class ContentCategoryType<TCategory> : ObjectGraphType<TCategory>
        where TCategory : class
    {
        /// <summary>
        /// 构造一个 <see cref="ContentCategoryType{TRole}"/> 实例。
        /// </summary>
        public ContentCategoryType()
        {
            Field(f => nameof(ContentCategory<int, Guid>.Name), nullable: true);
        }

    }
}
