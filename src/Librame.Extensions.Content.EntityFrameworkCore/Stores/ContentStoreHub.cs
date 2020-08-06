#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Content.Stores
{
    using Content.Accessors;
    using Data;
    using Data.Accessors;
    using Data.Collections;
    using Data.Stores;

    /// <summary>
    /// 内容存储中心。
    /// </summary>
    public class ContentStoreHub : ContentStoreHub<ContentDbContextAccessor>
    {
        /// <summary>
        /// 构造一个内容存储中心。
        /// </summary>
        /// <param name="initializer">给定的 <see cref="IStoreInitializer"/>。</param>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public ContentStoreHub(IStoreInitializer initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
        }

    }


    /// <summary>
    /// 内容存储中心。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class ContentStoreHub<TAccessor> : ContentStoreHub<TAccessor, Guid, int, Guid>
        where TAccessor : ContentDbContextAccessor
    {
        /// <summary>
        /// 构造一个内容存储中心。
        /// </summary>
        /// <param name="initializer">给定的 <see cref="IStoreInitializer"/>。</param>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public ContentStoreHub(IStoreInitializer initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
        }

    }


    /// <summary>
    /// 内容存储中心。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public class ContentStoreHub<TAccessor, TGenId, TIncremId, TPublishedBy>
        : ContentStoreHub<TAccessor,
            ContentCategory<TIncremId, TPublishedBy>,
            ContentSource<TIncremId, TPublishedBy>,
            ContentClaim<TIncremId, TPublishedBy>,
            ContentTag<TIncremId, TPublishedBy>,
            ContentUnit<TGenId, TIncremId, TIncremId, TPublishedBy>,
            ContentUnitClaim<TIncremId, TGenId, TIncremId, TPublishedBy>,
            ContentUnitTag<TIncremId, TGenId, TIncremId>,
            ContentUnitVisitCount<TGenId>,
            ContentPane<TIncremId, TPublishedBy>,
            ContentPaneUnit<TIncremId, TIncremId, TGenId, TPublishedBy>,
            TGenId, TIncremId, TPublishedBy>
        where TAccessor : ContentDbContextAccessor<TGenId, TIncremId, TPublishedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
        /// <summary>
        /// 构造一个内容存储中心。
        /// </summary>
        /// <param name="initializer">给定的 <see cref="IStoreInitializer"/>。</param>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public ContentStoreHub(IStoreInitializer initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
        }

    }


    /// <summary>
    /// 内容存储中心。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TCategory">指定的内容分类类型。</typeparam>
    /// <typeparam name="TSource">指定的内容来源类型。</typeparam>
    /// <typeparam name="TClaim">指定的内容声明类型。</typeparam>
    /// <typeparam name="TTag">指定的内容标签类型。</typeparam>
    /// <typeparam name="TUnit">指定的内容单元类型。</typeparam>
    /// <typeparam name="TUnitClaim">指定的内容单元声明类型。</typeparam>
    /// <typeparam name="TUnitTag">指定的内容单元标签类型。</typeparam>
    /// <typeparam name="TUnitVisitCount">指定的内容单元统计数据类型。</typeparam>
    /// <typeparam name="TPane">指定的内容窗格类型。</typeparam>
    /// <typeparam name="TPaneUnit">指定的内容窗格单元类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public class ContentStoreHub<TAccessor, TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit, TGenId, TIncremId, TPublishedBy>
        : DataStoreHub<TAccessor, TGenId, TIncremId, TPublishedBy>,
        IContentStoreHub<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit>
        where TAccessor : ContentDbContextAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit, TGenId, TIncremId, TPublishedBy>
        where TCategory : ContentCategory<TIncremId, TPublishedBy>
        where TSource : ContentSource<TIncremId, TPublishedBy>
        where TClaim : ContentClaim<TIncremId, TPublishedBy>
        where TTag : ContentTag<TIncremId, TPublishedBy>
        where TUnit : ContentUnit<TGenId, TIncremId, TIncremId, TPublishedBy>
        where TUnitClaim : ContentUnitClaim<TIncremId, TGenId, TIncremId, TPublishedBy>
        where TUnitTag : ContentUnitTag<TIncremId, TGenId, TIncremId>
        where TUnitVisitCount : ContentUnitVisitCount<TGenId>
        where TPane : ContentPane<TIncremId, TPublishedBy>
        where TPaneUnit : ContentPaneUnit<TIncremId, TIncremId, TGenId, TPublishedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
        /// <summary>
        /// 构造一个内容存储中心。
        /// </summary>
        /// <param name="initializer">给定的 <see cref="IStoreInitializer"/>。</param>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public ContentStoreHub(IStoreInitializer initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
        }


        /// <summary>
        /// 内容分类查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TCategory}"/>。</value>
        public IQueryable<TCategory> Categories
            => Accessor.Categories;

        /// <summary>
        /// 内容来源查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TSource}"/>。</value>
        public IQueryable<TSource> Sources
            => Accessor.Sources;

        /// <summary>
        /// 内容声明查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TClaim}"/>。</value>
        public IQueryable<TClaim> Claims
            => Accessor.Claims;

        /// <summary>
        /// 内容标签查询。
        /// </summary>
        public IQueryable<TTag> Tags
            => Accessor.Tags;

        /// <summary>
        /// 单元查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUnit}"/>。</value>
        public IQueryable<TUnit> Units
            => Accessor.Units;

        /// <summary>
        /// 单元声明查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUnitClaim}"/>。</value>
        public IQueryable<TUnitClaim> UnitClaims
            => Accessor.UnitClaims;

        /// <summary>
        /// 单元标签查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUnitTag}"/>。</value>
        public IQueryable<TUnitTag> UnitTags
            => Accessor.UnitTags;

        /// <summary>
        /// 单元访问计数查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUnitVisitCount}"/>。</value>
        public IQueryable<TUnitVisitCount> UnitVisitCounts
            => Accessor.UnitVisitCounts;

        /// <summary>
        /// 单元窗格查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TPane}"/>。</value>
        public IQueryable<TPane> Panes
            => Accessor.Panes;

        /// <summary>
        /// 单元窗格单元查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TPaneUnit}"/>。</value>
        public IQueryable<TPaneUnit> PaneUnits
            => Accessor.PaneUnits;


        #region Category

        /// <summary>
        /// 异步查找分类。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TCategory"/> 的异步操作。</returns>
        public virtual ValueTask<TCategory> FindCategoryAsync(CancellationToken cancellationToken,
            params object[] keyValues)
            => Accessor.Categories.FindAsync(keyValues, cancellationToken);

        /// <summary>
        /// 异步获取所有分页分类集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IReadOnlyList{TCategory}"/> 的异步操作。</returns>
        public virtual ValueTask<IReadOnlyList<TCategory>> GetAllCategoriesAsync
            (Func<IQueryable<TCategory>, IQueryable<TCategory>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Categories.AsNoTracking()) ?? Categories.AsNoTracking();
            return cancellationToken.RunFactoryOrCancellationValueAsync(() => (IReadOnlyList<TCategory>)query.ToList());
        }

        /// <summary>
        /// 异步获取分页分类集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TCategory}"/> 的异步操作。</returns>
        public virtual ValueTask<IPageable<TCategory>> GetPagingCategoriesAsync(int index, int size,
            Func<IQueryable<TCategory>, IQueryable<TCategory>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Categories.AsNoTracking()) ?? Categories.AsNoTracking();
            return query.AsPagingByIndexAsync(q => q.OrderByDescending(k => k.Rank),
                index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建分类集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        public virtual Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TCategory[] categories)
            => Accessor.Categories.TryCreateAsync(cancellationToken, categories);

        /// <summary>
        /// 尝试创建分类集合。
        /// </summary>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryCreate(params TCategory[] categories)
            => Accessor.Categories.TryCreate(categories);

        /// <summary>
        /// 尝试更新分类集合。
        /// </summary>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryUpdate(params TCategory[] categories)
            => Accessor.Categories.TryUpdate(categories);

        /// <summary>
        /// 尝试删除分类集合。
        /// </summary>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryDelete(params TCategory[] categories)
            => Accessor.Categories.TryLogicDelete(categories);

        #endregion


        #region Source

        /// <summary>
        /// 异步查找来源。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSource"/> 的异步操作。</returns>
        public virtual ValueTask<TSource> FindSourceAsync(CancellationToken cancellationToken, params object[] keyValues)
            => Accessor.Sources.FindAsync(keyValues, cancellationToken);

        /// <summary>
        /// 异步获取所有分页来源集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IReadOnlyList{TSource}"/> 的异步操作。</returns>
        public virtual ValueTask<IReadOnlyList<TSource>> GetAllSourcesAsync(
            Func<IQueryable<TSource>, IQueryable<TSource>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Sources.AsNoTracking()) ?? Sources.AsNoTracking();
            return cancellationToken.RunFactoryOrCancellationValueAsync(() => (IReadOnlyList<TSource>)query.ToList());
        }

        /// <summary>
        /// 异步获取分页来源集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSource}"/> 的异步操作。</returns>
        public virtual ValueTask<IPageable<TSource>> GetPagingSourcesAsync(int index, int size,
            Func<IQueryable<TSource>, IQueryable<TSource>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Sources.AsNoTracking()) ?? Sources.AsNoTracking();
            return query.AsPagingByIndexAsync(q => q.OrderByDescending(k => k.Rank),
                index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建来源集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        public virtual Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TSource[] sources)
            => Accessor.Sources.TryCreateAsync(cancellationToken, sources);

        /// <summary>
        /// 尝试创建来源集合。
        /// </summary>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryCreate(params TSource[] sources)
            => Accessor.Sources.TryCreate(sources);

        /// <summary>
        /// 尝试更新来源集合。
        /// </summary>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryUpdate(params TSource[] sources)
            => Accessor.Sources.TryUpdate(sources);

        /// <summary>
        /// 尝试删除来源集合。
        /// </summary>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryDelete(params TSource[] sources)
            => Accessor.Sources.TryLogicDelete(sources);

        #endregion


        #region Claim

        /// <summary>
        /// 异步查找声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TClaim"/> 的异步操作。</returns>
        public virtual ValueTask<TClaim> FindClaimAsync(CancellationToken cancellationToken, params object[] keyValues)
            => Accessor.Claims.FindAsync(keyValues, cancellationToken);

        /// <summary>
        /// 异步获取所有分页声明集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IReadOnlyList{TClaim}"/> 的异步操作。</returns>
        public virtual ValueTask<IReadOnlyList<TClaim>> GetAllClaimsAsync(
            Func<IQueryable<TClaim>, IQueryable<TClaim>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Claims.AsNoTracking()) ?? Claims.AsNoTracking();
            return cancellationToken.RunFactoryOrCancellationValueAsync(() => (IReadOnlyList<TClaim>)query.ToList());
        }

        /// <summary>
        /// 异步获取分页声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TClaim}"/> 的异步操作。</returns>
        public virtual ValueTask<IPageable<TClaim>> GetPagingClaimsAsync(int index, int size,
            Func<IQueryable<TClaim>, IQueryable<TClaim>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Claims.AsNoTracking()) ?? Claims.AsNoTracking();
            return query.AsPagingByIndexAsync(q => q.OrderByDescending(k => k.Rank),
                index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        public virtual Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TClaim[] claims)
            => Accessor.Claims.TryCreateAsync(cancellationToken, claims);

        /// <summary>
        /// 尝试创建声明集合。
        /// </summary>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryCreate(params TClaim[] claims)
            => Accessor.Claims.TryCreate(claims);

        /// <summary>
        /// 尝试更新声明集合。
        /// </summary>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryUpdate(params TClaim[] claims)
            => Accessor.Claims.TryUpdate(claims);

        /// <summary>
        /// 尝试删除声明集合。
        /// </summary>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryDelete(params TClaim[] claims)
            => Accessor.Claims.TryLogicDelete(claims);

        #endregion


        #region Tag

        /// <summary>
        /// 异步查找标签。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TTag"/> 的异步操作。</returns>
        public virtual ValueTask<TTag> FindTagAsync(CancellationToken cancellationToken, params object[] keyValues)
            => Accessor.Tags.FindAsync(keyValues, cancellationToken);

        /// <summary>
        /// 异步获取所有分页标签集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IReadOnlyList{TTag}"/> 的异步操作。</returns>
        public virtual ValueTask<IReadOnlyList<TTag>> GetAllTagsAsync(
            Func<IQueryable<TTag>, IQueryable<TTag>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Tags.AsNoTracking()) ?? Tags.AsNoTracking();
            return cancellationToken.RunFactoryOrCancellationValueAsync(() => (IReadOnlyList<TTag>)query.ToList());
        }

        /// <summary>
        /// 异步获取分页标签集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TTag}"/> 的异步操作。</returns>
        public virtual ValueTask<IPageable<TTag>> GetPagingTagsAsync(int index, int size,
            Func<IQueryable<TTag>, IQueryable<TTag>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Tags.AsNoTracking()) ?? Tags.AsNoTracking();
            return query.AsPagingByIndexAsync(q => q.OrderByDescending(k => k.Rank),
                index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建标签集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        public virtual Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TTag[] tags)
            => Accessor.Tags.TryCreateAsync(cancellationToken, tags);

        /// <summary>
        /// 尝试创建标签集合。
        /// </summary>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryCreate(params TTag[] tags)
            => Accessor.Tags.TryCreate(tags);

        /// <summary>
        /// 尝试更新标签集合。
        /// </summary>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryUpdate(params TTag[] tags)
            => Accessor.Tags.TryUpdate(tags);

        /// <summary>
        /// 尝试删除标签集合。
        /// </summary>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryDelete(params TTag[] tags)
            => Accessor.Tags.TryLogicDelete(tags);

        #endregion


        #region Unit

        /// <summary>
        /// 异步查找单元。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TUnit"/> 的异步操作。</returns>
        public virtual ValueTask<TUnit> FindUnitAsync(CancellationToken cancellationToken, params object[] keyValues)
            => Accessor.Units.FindAsync(keyValues, cancellationToken);

        /// <summary>
        /// 异步获取分页单元集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TUnit}"/> 的异步操作。</returns>
        public virtual ValueTask<IPageable<TUnit>> GetPagingUnitsAsync(int index, int size,
            Func<IQueryable<TUnit>, IQueryable<TUnit>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Units.AsNoTracking()) ?? Units.AsNoTracking();
            return query.AsPagingByIndexAsync(q => q.OrderByDescending(k => k.Rank),
                index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建单元集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="units">给定的 <typeparamref name="TUnit"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        public virtual Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TUnit[] units)
            => Accessor.Units.TryCreateAsync(cancellationToken, units);

        /// <summary>
        /// 尝试创建单元集合。
        /// </summary>
        /// <param name="units">给定的 <typeparamref name="TUnit"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryCreate(params TUnit[] units)
            => Accessor.Units.TryCreate(units);

        /// <summary>
        /// 尝试更新单元集合。
        /// </summary>
        /// <param name="units">给定的 <typeparamref name="TUnit"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryUpdate(params TUnit[] units)
            => Accessor.Units.TryUpdate(units);

        /// <summary>
        /// 尝试删除单元集合。
        /// </summary>
        /// <param name="units">给定的 <typeparamref name="TUnit"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryDelete(params TUnit[] units)
            => Accessor.Units.TryLogicDelete(units);

        #endregion


        #region UnitClaim

        /// <summary>
        /// 异步查找单元。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TUnitClaim"/> 的异步操作。</returns>
        public virtual ValueTask<TUnitClaim> FindUnitClaimAsync(CancellationToken cancellationToken,
            params object[] keyValues)
            => Accessor.UnitClaims.FindAsync(keyValues, cancellationToken);

        /// <summary>
        /// 异步获取分页单元集合。
        /// </summary>
        /// <param name="unitId">给定的单元 <typeparamref name="TGenId"/>。</param>
        /// <param name="claimId">给定的内容声明 <typeparamref name="TIncremId"/>。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TUnitClaim}"/> 的异步操作。</returns>
        public virtual ValueTask<IReadOnlyList<TUnitClaim>> GetUnitClaimsAsync(object unitId, object claimId,
            Func<IQueryable<TUnitClaim>, IQueryable<TUnitClaim>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var realUnitId = unitId.CastTo<object, TGenId>(nameof(unitId));
            var realClaimId = claimId.CastTo<object, TIncremId>(nameof(claimId));

            var query = queryFactory?.Invoke(UnitClaims) ?? UnitClaims;

            return cancellationToken.RunFactoryOrCancellationValueAsync(() =>
            {
                return query.Where(p => p.UnitId.Equals(realUnitId) && p.ClaimId.Equals(realClaimId))
                    .OrderByDescending(k => k.Rank).AsReadOnlyList();
            });
        }


        /// <summary>
        /// 尝试异步创建单元声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="unitClaims">给定的 <typeparamref name="TUnitClaim"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        public virtual Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TUnitClaim[] unitClaims)
            => Accessor.UnitClaims.TryCreateAsync(cancellationToken, unitClaims);

        /// <summary>
        /// 尝试创建单元声明集合。
        /// </summary>
        /// <param name="unitClaims">给定的 <typeparamref name="TUnitClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryCreate(params TUnitClaim[] unitClaims)
            => Accessor.UnitClaims.TryCreate(unitClaims);

        /// <summary>
        /// 尝试更新单元声明集合。
        /// </summary>
        /// <param name="unitClaims">给定的 <typeparamref name="TUnitClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryUpdate(params TUnitClaim[] unitClaims)
            => Accessor.UnitClaims.TryUpdate(unitClaims);

        /// <summary>
        /// 尝试删除单元声明集合。
        /// </summary>
        /// <param name="unitClaims">给定的 <typeparamref name="TUnitClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryDelete(params TUnitClaim[] unitClaims)
            => Accessor.UnitClaims.TryLogicDelete(unitClaims);

        #endregion


        #region UnitTag

        /// <summary>
        /// 异步查找单元标签。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TUnitTag"/> 的异步操作。</returns>
        public virtual ValueTask<TUnitTag> FindUnitTagAsync(CancellationToken cancellationToken,
            params object[] keyValues)
            => Accessor.UnitTags.FindAsync(keyValues, cancellationToken);

        /// <summary>
        /// 异步获取分页单元标签集合。
        /// </summary>
        /// <param name="unitId">给定的单元 <typeparamref name="TGenId"/>。</param>
        /// <param name="tagId">给定的内容标签 <typeparamref name="TIncremId"/>。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TUnitTag}"/> 的异步操作。</returns>
        public virtual ValueTask<IReadOnlyList<TUnitTag>> GetUnitTagsAsync(object unitId, object tagId,
            Func<IQueryable<TUnitTag>, IQueryable<TUnitTag>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var realUnitId = unitId.CastTo<object, TGenId>(nameof(unitId));
            var realTagId = tagId.CastTo<object, TIncremId>(nameof(tagId));

            var query = queryFactory?.Invoke(UnitTags) ?? UnitTags;

            return cancellationToken.RunFactoryOrCancellationValueAsync(() =>
            {
                return query.Where(p => p.UnitId.Equals(realUnitId) && p.TagId.Equals(realTagId))
                    .OrderByDescending(k => k.TagId).AsReadOnlyList();
            });
        }


        /// <summary>
        /// 尝试异步创建单元标签集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="unitTags">给定的 <typeparamref name="TUnitTag"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        public virtual Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TUnitTag[] unitTags)
            => Accessor.UnitTags.TryCreateAsync(cancellationToken, unitTags);

        /// <summary>
        /// 尝试创建单元标签集合。
        /// </summary>
        /// <param name="unitTags">给定的 <typeparamref name="TUnitTag"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryCreate(params TUnitTag[] unitTags)
            => Accessor.UnitTags.TryCreate(unitTags);

        /// <summary>
        /// 尝试更新单元标签集合。
        /// </summary>
        /// <param name="unitTags">给定的 <typeparamref name="TUnitTag"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryUpdate(params TUnitTag[] unitTags)
            => Accessor.UnitTags.TryUpdate(unitTags);

        ///// <summary>
        ///// 尝试删除单元标签集合。
        ///// </summary>
        ///// <param name="unitTags">给定的 <typeparamref name="TUnitTag"/> 数组。</param>
        ///// <returns>返回 <see cref="OperationResult"/>。</returns>
        //public virtual OperationResult TryDelete(params TUnitTag[] unitTags)
        //    => Accessor.UnitTags.TryDelete(unitTags);

        #endregion


        #region UnitVisitCount

        /// <summary>
        /// 异步查找单元访问计数。
        /// </summary>
        /// <param name="unitId">给定的单元标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TUnitVisitCount"/> 的异步操作。</returns>
        public virtual ValueTask<TUnitVisitCount> FindUnitVisitCountAsync(object unitId,
            CancellationToken cancellationToken = default)
        {
            var realUnitId = unitId.CastTo<object, TGenId>(nameof(unitId));

            return cancellationToken.RunFactoryOrCancellationValueAsync(()
                => UnitVisitCounts.SingleOrDefault(p => p.UnitId.Equals(realUnitId)));
        }


        /// <summary>
        /// 尝试异步创建单元访问计数集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="unitVisitCounts">给定的 <typeparamref name="TUnitVisitCount"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        public virtual Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TUnitVisitCount[] unitVisitCounts)
            => Accessor.UnitVisitCounts.TryCreateAsync(cancellationToken, unitVisitCounts);

        /// <summary>
        /// 尝试创建单元访问计数集合。
        /// </summary>
        /// <param name="unitVisitCounts">给定的 <typeparamref name="TUnitVisitCount"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryCreate(params TUnitVisitCount[] unitVisitCounts)
            => Accessor.UnitVisitCounts.TryCreate(unitVisitCounts);

        #endregion


        #region Pane

        /// <summary>
        /// 异步查找窗格。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TPane"/> 的异步操作。</returns>
        public virtual ValueTask<TPane> FindPaneAsync(CancellationToken cancellationToken, params object[] keyValues)
            => Accessor.Panes.FindAsync(keyValues, cancellationToken);

        /// <summary>
        /// 异步获取分页窗格集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TPane}"/> 的异步操作。</returns>
        public virtual ValueTask<IPageable<TPane>> GetPagingPanesAsync(int index, int size,
            Func<IQueryable<TPane>, IQueryable<TPane>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Panes.AsNoTracking()) ?? Panes.AsNoTracking();
            return query.AsPagingByIndexAsync(q => q.OrderByDescending(k => k.Rank),
                index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建窗格集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        public virtual Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TPane[] panes)
            => Accessor.Panes.TryCreateAsync(cancellationToken, panes);

        /// <summary>
        /// 尝试创建窗格集合。
        /// </summary>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryCreate(params TPane[] panes)
            => Accessor.Panes.TryCreate(panes);

        /// <summary>
        /// 尝试更新窗格集合。
        /// </summary>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryUpdate(params TPane[] panes)
            => Accessor.Panes.TryUpdate(panes);

        /// <summary>
        /// 尝试删除窗格集合。
        /// </summary>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryDelete(params TPane[] panes)
            => Accessor.Panes.TryLogicDelete(panes);

        #endregion


        #region PaneUnit

        /// <summary>
        /// 异步查找窗格单元。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TPaneUnit"/> 的异步操作。</returns>
        public virtual ValueTask<TPaneUnit> FindPaneUnitAsync(CancellationToken cancellationToken, params object[] keyValues)
            => Accessor.PaneUnits.FindAsync(keyValues, cancellationToken);

        /// <summary>
        /// 异步获取分页窗格单元集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TPaneUnit}"/> 的异步操作。</returns>
        public virtual ValueTask<IPageable<TPaneUnit>> GetPagingPaneUnitsAsync(int index, int size,
            Func<IQueryable<TPaneUnit>, IQueryable<TPaneUnit>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(PaneUnits.AsNoTracking()) ?? PaneUnits.AsNoTracking();
            return query.AsPagingByIndexAsync(q => q.OrderByDescending(k => k.Rank),
                index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建窗格单元集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="paneUnits">给定的 <typeparamref name="TPaneUnit"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        public virtual Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TPaneUnit[] paneUnits)
            => Accessor.PaneUnits.TryCreateAsync(cancellationToken, paneUnits);

        /// <summary>
        /// 尝试创建窗格单元集合。
        /// </summary>
        /// <param name="paneUnits">给定的 <typeparamref name="TPaneUnit"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryCreate(params TPaneUnit[] paneUnits)
            => Accessor.PaneUnits.TryCreate(paneUnits);

        /// <summary>
        /// 尝试更新窗格单元集合。
        /// </summary>
        /// <param name="paneUnits">给定的 <typeparamref name="TPaneUnit"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryUpdate(params TPaneUnit[] paneUnits)
            => Accessor.PaneUnits.TryUpdate(paneUnits);

        /// <summary>
        /// 尝试删除窗格单元集合。
        /// </summary>
        /// <param name="paneUnits">给定的 <typeparamref name="TPaneUnit"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryDelete(params TPaneUnit[] paneUnits)
            => Accessor.PaneUnits.TryLogicDelete(paneUnits);

        #endregion

    }
}
