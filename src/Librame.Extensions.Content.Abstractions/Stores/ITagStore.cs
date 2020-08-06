#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Content.Stores
{
    using Data;
    using Data.Collections;
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


        /// <summary>
        /// 异步查找标签。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TTag"/> 的异步操作。</returns>
        ValueTask<TTag> FindTagAsync(CancellationToken cancellationToken,
            params object[] keyValues);

        /// <summary>
        /// 异步获取所有标签集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IReadOnlyList{TTag}"/> 的异步操作。</returns>
        ValueTask<IReadOnlyList<TTag>> GetAllTagsAsync
            (Func<IQueryable<TTag>, IQueryable<TTag>> queryFactory = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页标签集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TTag}"/> 的异步操作。</returns>
        ValueTask<IPageable<TTag>> GetPagingTagsAsync(int index, int size,
            Func<IQueryable<TTag>, IQueryable<TTag>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建标签集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TTag[] tags);

        /// <summary>
        /// 尝试创建标签集合。
        /// </summary>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TTag[] tags);

        /// <summary>
        /// 尝试更新标签集合。
        /// </summary>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryUpdate(params TTag[] tags);

        /// <summary>
        /// 尝试删除标签集合。
        /// </summary>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryDelete(params TTag[] tags);
    }
}
