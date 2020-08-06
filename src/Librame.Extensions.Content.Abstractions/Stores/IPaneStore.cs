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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Content.Stores
{
    using Data;
    using Data.Collections;
    using Data.Stores;

    /// <summary>
    /// 窗格存储接口。
    /// </summary>
    /// <typeparam name="TPane">指定的内容窗格类型。</typeparam>
    /// <typeparam name="TPaneUnit">指定的内容窗格单元类型。</typeparam>
    public interface IPaneStore<TPane, TPaneUnit> : IStore
        where TPane : class
        where TPaneUnit : class
    {
        /// <summary>
        /// 窗格查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TPane}"/>。</value>
        IQueryable<TPane> Panes { get; }

        /// <summary>
        /// 窗格单元查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TPaneUnit}"/>。</value>
        IQueryable<TPaneUnit> PaneUnits { get; }


        #region Pane

        /// <summary>
        /// 异步查找窗格。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TPane"/> 的异步操作。</returns>
        ValueTask<TPane> FindPaneAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取分页窗格集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TPane}"/> 的异步操作。</returns>
        ValueTask<IPageable<TPane>> GetPagingPanesAsync(int index, int size,
            Func<IQueryable<TPane>, IQueryable<TPane>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建窗格集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TPane[] panes);

        /// <summary>
        /// 尝试创建窗格集合。
        /// </summary>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TPane[] panes);

        /// <summary>
        /// 尝试更新窗格集合。
        /// </summary>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryUpdate(params TPane[] panes);

        /// <summary>
        /// 尝试删除窗格集合。
        /// </summary>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryDelete(params TPane[] panes);

        #endregion


        #region PaneUnit

        /// <summary>
        /// 异步查找窗格单元。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TPaneUnit"/> 的异步操作。</returns>
        ValueTask<TPaneUnit> FindPaneUnitAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取分页窗格单元集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TPaneUnit}"/> 的异步操作。</returns>
        ValueTask<IPageable<TPaneUnit>> GetPagingPaneUnitsAsync(int index, int size,
            Func<IQueryable<TPaneUnit>, IQueryable<TPaneUnit>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建窗格单元集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="paneUnits">给定的 <typeparamref name="TPaneUnit"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TPaneUnit[] paneUnits);

        /// <summary>
        /// 尝试创建窗格单元集合。
        /// </summary>
        /// <param name="paneUnits">给定的 <typeparamref name="TPaneUnit"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TPaneUnit[] paneUnits);

        /// <summary>
        /// 尝试更新窗格单元集合。
        /// </summary>
        /// <param name="paneUnits">给定的 <typeparamref name="TPaneUnit"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryUpdate(params TPaneUnit[] paneUnits);

        /// <summary>
        /// 尝试删除窗格单元集合。
        /// </summary>
        /// <param name="paneUnits">给定的 <typeparamref name="TPaneUnit"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryDelete(params TPaneUnit[] paneUnits);

        #endregion

    }
}
