﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Portal.Stores
{
    using Data;
    using Data.Collections;
    using Data.Stores;

    /// <summary>
    /// 编者存储接口。
    /// </summary>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    public interface IEditorStore<TEditor> : IStore
        where TEditor : class
    {
        /// <summary>
        /// 编者查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TEditor}"/>。</value>
        IQueryable<TEditor> Editors { get; }


        /// <summary>
        /// 异步查找编者。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditor"/> 的异步操作。</returns>
        ValueTask<TEditor> FindEditorAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取分页编者集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TEditor}"/> 的异步操作。</returns>
        ValueTask<IPageable<TEditor>> GetPagingEditorsAsync(int index, int size,
            Func<IQueryable<TEditor>, IQueryable<TEditor>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建编者集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TEditor[] editors);

        /// <summary>
        /// 尝试创建编者集合。
        /// </summary>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TEditor[] editors);

        /// <summary>
        /// 尝试更新编者集合。
        /// </summary>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryUpdate(params TEditor[] editors);

        /// <summary>
        /// 尝试删除编者集合。
        /// </summary>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryDelete(params TEditor[] editors);
    }
}
