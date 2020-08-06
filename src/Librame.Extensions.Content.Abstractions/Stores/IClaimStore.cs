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
    /// 声明存储接口。
    /// </summary>
    /// <typeparam name="TClaim">指定的内容声明类型。</typeparam>
    public interface IClaimStore<TClaim> : IStore
        where TClaim : class
    {
        /// <summary>
        /// 声明查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TClaim}"/>。</value>
        IQueryable<TClaim> Claims { get; }


        /// <summary>
        /// 异步查找声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TClaim"/> 的异步操作。</returns>
        ValueTask<TClaim> FindClaimAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取所有声明集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IReadOnlyList{TClaim}"/> 的异步操作。</returns>
        ValueTask<IReadOnlyList<TClaim>> GetAllClaimsAsync(Func<IQueryable<TClaim>, IQueryable<TClaim>> queryFactory = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TClaim}"/> 的异步操作。</returns>
        ValueTask<IPageable<TClaim>> GetPagingClaimsAsync(int index, int size,
            Func<IQueryable<TClaim>, IQueryable<TClaim>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken, params TClaim[] claims);

        /// <summary>
        /// 尝试创建声明集合。
        /// </summary>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TClaim[] claims);

        /// <summary>
        /// 尝试更新声明集合。
        /// </summary>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryUpdate(params TClaim[] claims);

        /// <summary>
        /// 尝试删除声明集合。
        /// </summary>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryDelete(params TClaim[] claims);
    }
}
