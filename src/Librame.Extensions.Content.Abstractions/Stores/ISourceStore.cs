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
    /// 来源存储接口。
    /// </summary>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    public interface ISourceStore<TSource> : IStore
        where TSource : class
    {
        /// <summary>
        /// 来源查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TSource}"/>。</value>
        IQueryable<TSource> Sources { get; }
    }
}
