#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Linq;

namespace Librame.Extensions.Content.Stores
{
    using Data.Stores;

    /// <summary>
    /// 标签存储接口。
    /// </summary>
    /// <typeparam name="TTag">指定的内容标签类型。</typeparam>
    public interface ITagStore<TTag> : IStore
        where TTag : class
    {
        /// <summary>
        /// 标签查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TTag}"/>。</value>
        IQueryable<TTag> Tags { get; }
    }
}
