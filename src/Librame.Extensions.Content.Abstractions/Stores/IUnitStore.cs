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
    /// 单元存储接口。
    /// </summary>
    /// <typeparam name="TUnit">指定的单元类型。</typeparam>
    /// <typeparam name="TUnitClaim">指定的单元声明类型。</typeparam>
    /// <typeparam name="TUnitTag">指定的单元标签类型。</typeparam>
    /// <typeparam name="TUnitVisitCount">指定的单元访问计数。</typeparam>
    public interface IUnitStore<TUnit, TUnitClaim, TUnitTag, TUnitVisitCount> : IStore
        where TUnit : class
        where TUnitClaim : class
        where TUnitTag : class
        where TUnitVisitCount : class
    {
        /// <summary>
        /// 单元查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUnit}"/>。</value>
        IQueryable<TUnit> Units { get; }

        /// <summary>
        /// 单元声明查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUnitClaim}"/>。</value>
        IQueryable<TUnitClaim> UnitClaims { get; }

        /// <summary>
        /// 单元标签查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUnitTag}"/>。</value>
        IQueryable<TUnitTag> UnitTags { get; }

        /// <summary>
        /// 单元统计数据查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUnitVisitCount}"/>。</value>
        IQueryable<TUnitVisitCount> UnitVisitCounts { get; }


        #region Unit

        /// <summary>
        /// 异步查找单元。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TUnit"/> 的异步操作。</returns>
        ValueTask<TUnit> FindUnitAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取分页单元集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TUnit}"/> 的异步操作。</returns>
        ValueTask<IPageable<TUnit>> GetPagingUnitsAsync(int index, int size,
            Func<IQueryable<TUnit>, IQueryable<TUnit>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建单元集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="units">给定的 <typeparamref name="TUnit"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TUnit[] units);

        /// <summary>
        /// 尝试创建单元集合。
        /// </summary>
        /// <param name="units">给定的 <typeparamref name="TUnit"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TUnit[] units);

        /// <summary>
        /// 尝试更新单元集合。
        /// </summary>
        /// <param name="units">给定的 <typeparamref name="TUnit"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryUpdate(params TUnit[] units);

        /// <summary>
        /// 尝试删除单元集合。
        /// </summary>
        /// <param name="units">给定的 <typeparamref name="TUnit"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryDelete(params TUnit[] units);

        #endregion


        #region UnitClaim

        /// <summary>
        /// 异步查找单元。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TUnitClaim"/> 的异步操作。</returns>
        ValueTask<TUnitClaim> FindUnitClaimAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取分页单元集合。
        /// </summary>
        /// <param name="unitId">给定的单元标识。</param>
        /// <param name="claimId">给定的内容声明标识。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TUnitClaim}"/> 的异步操作。</returns>
        ValueTask<IReadOnlyList<TUnitClaim>> GetUnitClaimsAsync(object unitId, object claimId,
            Func<IQueryable<TUnitClaim>, IQueryable<TUnitClaim>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建单元声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="unitClaims">给定的 <typeparamref name="TUnitClaim"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TUnitClaim[] unitClaims);

        /// <summary>
        /// 尝试创建单元声明集合。
        /// </summary>
        /// <param name="unitClaims">给定的 <typeparamref name="TUnitClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TUnitClaim[] unitClaims);

        /// <summary>
        /// 尝试更新单元声明集合。
        /// </summary>
        /// <param name="unitClaims">给定的 <typeparamref name="TUnitClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryUpdate(params TUnitClaim[] unitClaims);

        /// <summary>
        /// 尝试删除单元声明集合。
        /// </summary>
        /// <param name="unitClaims">给定的 <typeparamref name="TUnitClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryDelete(params TUnitClaim[] unitClaims);

        #endregion


        #region UnitTag

        /// <summary>
        /// 异步查找单元标签。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TUnitTag"/> 的异步操作。</returns>
        ValueTask<TUnitTag> FindUnitTagAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取分页单元标签集合。
        /// </summary>
        /// <param name="unitId">给定的单元标识。</param>
        /// <param name="tagId">给定的内容标签标识。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TUnitTag}"/> 的异步操作。</returns>
        ValueTask<IReadOnlyList<TUnitTag>> GetUnitTagsAsync(object unitId, object tagId,
            Func<IQueryable<TUnitTag>, IQueryable<TUnitTag>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建单元标签集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="unitTags">给定的 <typeparamref name="TUnitTag"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TUnitTag[] unitTags);

        /// <summary>
        /// 尝试创建单元标签集合。
        /// </summary>
        /// <param name="unitTags">给定的 <typeparamref name="TUnitTag"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TUnitTag[] unitTags);

        /// <summary>
        /// 尝试更新单元标签集合。
        /// </summary>
        /// <param name="unitTags">给定的 <typeparamref name="TUnitTag"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryUpdate(params TUnitTag[] unitTags);

        ///// <summary>
        ///// 尝试删除单元标签集合。
        ///// </summary>
        ///// <param name="unitTags">给定的 <typeparamref name="TUnitTag"/> 数组。</param>
        ///// <returns>返回 <see cref="OperationResult"/>。</returns>
        //OperationResult TryDelete(params TUnitTag[] unitTags);

        #endregion


        #region UnitVisitCount

        /// <summary>
        /// 异步查找单元访问计数。
        /// </summary>
        /// <param name="unitId">给定的单元标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TUnitVisitCount"/> 的异步操作。</returns>
        ValueTask<TUnitVisitCount> FindUnitVisitCountAsync(object unitId,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建单元访问计数集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="unitVisitCounts">给定的 <typeparamref name="TUnitVisitCount"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TUnitVisitCount[] unitVisitCounts);

        /// <summary>
        /// 尝试创建单元访问计数集合。
        /// </summary>
        /// <param name="unitVisitCounts">给定的 <typeparamref name="TUnitVisitCount"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TUnitVisitCount[] unitVisitCounts);

        #endregion

    }
}
