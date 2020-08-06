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
    /// 来源存储接口。
    /// </summary>
    /// <typeparam name="TSource">指定的内容来源类型。</typeparam>
    public interface ISourceStore<TSource> : IStore
        where TSource : class
    {
        /// <summary>
        /// 来源查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TSource}"/>。</value>
        IQueryable<TSource> Sources { get; }


        /// <summary>
        /// 异步查找来源。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSource"/> 的异步操作。</returns>
        ValueTask<TSource> FindSourceAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取所有来源集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IReadOnlyList{TSource}"/> 的异步操作。</returns>
        ValueTask<IReadOnlyList<TSource>> GetAllSourcesAsync(Func<IQueryable<TSource>, IQueryable<TSource>> queryFactory = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页来源集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSource}"/> 的异步操作。</returns>
        ValueTask<IPageable<TSource>> GetPagingSourcesAsync(int index, int size,
            Func<IQueryable<TSource>, IQueryable<TSource>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建来源集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken, params TSource[] sources);

        /// <summary>
        /// 尝试创建来源集合。
        /// </summary>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TSource[] sources);

        /// <summary>
        /// 尝试更新来源集合。
        /// </summary>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryUpdate(params TSource[] sources);

        /// <summary>
        /// 尝试删除来源集合。
        /// </summary>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryDelete(params TSource[] sources);
    }
}
