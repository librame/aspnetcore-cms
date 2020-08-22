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

namespace Librame.Extensions.Content.Stores
{
    using Content.Accessors;
    using Data.Accessors;
    using Data.Stores;

    /// <summary>
    /// 内容存储中心。
    /// </summary>
    public class ContentStoreHub : ContentStoreHub<ContentDbContextAccessor>
    {
        /// <summary>
        /// 构造一个内容存储中心。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public ContentStoreHub(IAccessor accessor)
            : base(accessor)
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
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public ContentStoreHub(IAccessor accessor)
            : base(accessor)
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
            ContentClaim<TIncremId, TIncremId, TPublishedBy>,
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
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public ContentStoreHub(IAccessor accessor)
            : base(accessor)
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
        where TAccessor : class, IContentAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit>,
            IDataAccessor<TGenId, TIncremId, TPublishedBy>
        where TCategory : ContentCategory<TIncremId, TPublishedBy>
        where TSource : ContentSource<TIncremId, TPublishedBy>
        where TClaim : ContentClaim<TIncremId, TIncremId, TPublishedBy>
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
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public ContentStoreHub(IAccessor accessor)
            : base(accessor)
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
    }
}
