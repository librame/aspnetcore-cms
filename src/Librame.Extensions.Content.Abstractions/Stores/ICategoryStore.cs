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
    /// 分类存储接口。
    /// </summary>
    /// <typeparam name="TCategory">指定的内容分类类型。</typeparam>
    public interface ICategoryStore<TCategory> : IStore
        where TCategory : class
    {
        /// <summary>
        /// 分类查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TCategory}"/>。</value>
        IQueryable<TCategory> Categories { get; }


        /// <summary>
        /// 异步查找分类。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TCategory"/> 的异步操作。</returns>
        ValueTask<TCategory> FindCategoryAsync(CancellationToken cancellationToken,
            params object[] keyValues);

        /// <summary>
        /// 异步获取所有分类集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IReadOnlyList{TCategory}"/> 的异步操作。</returns>
        ValueTask<IReadOnlyList<TCategory>> GetAllCategoriesAsync
            (Func<IQueryable<TCategory>, IQueryable<TCategory>> queryFactory = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页分类集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TCategory}"/> 的异步操作。</returns>
        ValueTask<IPageable<TCategory>> GetPagingCategoriesAsync(int index, int size,
            Func<IQueryable<TCategory>, IQueryable<TCategory>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建分类集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TCategory[] categories);

        /// <summary>
        /// 尝试创建分类集合。
        /// </summary>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TCategory[] categories);

        /// <summary>
        /// 尝试更新分类集合。
        /// </summary>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryUpdate(params TCategory[] categories);

        /// <summary>
        /// 尝试删除分类集合。
        /// </summary>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryDelete(params TCategory[] categories);
    }
}
