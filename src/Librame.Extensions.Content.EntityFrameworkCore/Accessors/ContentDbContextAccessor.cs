#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using System;

namespace Librame.Extensions.Content.Accessors
{
    using Content.Stores;
    using Data;
    using Data.Accessors;

    /// <summary>
    /// 内容数据库上下文访问器。
    /// </summary>
    public class ContentDbContextAccessor : ContentDbContextAccessor<Guid, int, Guid>,
        IContentAccessor, IDataAccessor
    {
        /// <summary>
        /// 构造一个内容数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public ContentDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }

    }


    /// <summary>
    /// 内容数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public class ContentDbContextAccessor<TGenId, TIncremId, TPublishedBy>
        : ContentDbContextAccessor<ContentCategory<TIncremId, TPublishedBy>,
            ContentSource<TIncremId, TPublishedBy>,
            ContentClaim<TIncremId, TIncremId, TPublishedBy>,
            ContentTag<TIncremId, TPublishedBy>,
            ContentUnit<TGenId, TIncremId, TIncremId, TPublishedBy>,
            ContentUnitClaim<TIncremId, TGenId, TIncremId, TPublishedBy>,
            ContentUnitTag<TIncremId, TGenId, TIncremId>,
            ContentUnitVisitCount<TGenId>,
            ContentPane<TIncremId, TPublishedBy>,
            ContentPaneUnit<TIncremId, TIncremId, TGenId, TPublishedBy>,
            TGenId, TIncremId, TPublishedBy>,
            IDataAccessor<TGenId, TIncremId, TPublishedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
        /// <summary>
        /// 构造一个内容数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        protected ContentDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }

    }


    /// <summary>
    /// 内容数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TCategory">指定的内容分类类型。</typeparam>
    /// <typeparam name="TSource">指定的内容来源类型。</typeparam>
    /// <typeparam name="TClaim">指定的内容声明类型。</typeparam>
    /// <typeparam name="TTag">指定的内容标签类型。</typeparam>
    /// <typeparam name="TUnit">指定的内容单元类型。</typeparam>
    /// <typeparam name="TUnitClaim">指定的内容单元声明类型。</typeparam>
    /// <typeparam name="TUnitTag">指定的内容单元标签类型。</typeparam>
    /// <typeparam name="TUnitVisitCount">指定的内容单元统计数据类型。</typeparam>
    /// <typeparam name="TPane">指定的内容窗格类型。</typeparam>
    /// <typeparam name="TPaneUnit">指定的内容单元类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public class ContentDbContextAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit, TGenId, TIncremId, TPublishedBy>
        : DataDbContextAccessor<TGenId, TIncremId, TPublishedBy>,
            IContentAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit>
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
        /// 构造一个内容数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        protected ContentDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }


        /// <summary>
        /// 分类数据集。
        /// </summary>
        public DbSet<TCategory> Categories { get; set; }

        /// <summary>
        /// 来源数据集。
        /// </summary>
        public DbSet<TSource> Sources { get; set; }

        /// <summary>
        /// 声明数据集。
        /// </summary>
        public DbSet<TClaim> Claims { get; set; }

        /// <summary>
        /// 标签数据集。
        /// </summary>
        public DbSet<TTag> Tags { get; set; }

        /// <summary>
        /// 单元数据集。
        /// </summary>
        public DbSet<TUnit> Units { get; set; }

        /// <summary>
        /// 单元声明数据集。
        /// </summary>
        public DbSet<TUnitClaim> UnitClaims { get; set; }

        /// <summary>
        /// 单元标签数据集。
        /// </summary>
        public DbSet<TUnitTag> UnitTags { get; set; }

        /// <summary>
        /// 单元统计数据集。
        /// </summary>
        public DbSet<TUnitVisitCount> UnitVisitCounts { get; set; }

        /// <summary>
        /// 窗格数据集。
        /// </summary>
        public DbSet<TPane> Panes { get; set; }

        /// <summary>
        /// 窗格单元数据集。
        /// </summary>
        public DbSet<TPaneUnit> PaneUnits { get; set; }


        /// <summary>
        /// 分类数据集管理器。
        /// </summary>
        public DbSetManager<TCategory> CategoriesManager
            => Categories.AsManager();

        /// <summary>
        /// 来源数据集管理器。
        /// </summary>
        public DbSetManager<TSource> SourcesManager
            => Sources.AsManager();

        /// <summary>
        /// 声明数据集管理器。
        /// </summary>
        public DbSetManager<TClaim> ClaimsManager
            => Claims.AsManager();

        /// <summary>
        /// 标签数据集管理器。
        /// </summary>
        public DbSetManager<TTag> TagsManager
            => Tags.AsManager();

        /// <summary>
        /// 单元数据集管理器。
        /// </summary>
        public DbSetManager<TUnit> UnitsManager
            => Units.AsManager();

        /// <summary>
        /// 单元声明数据集管理器。
        /// </summary>
        public DbSetManager<TUnitClaim> UnitClaimsManager
            => UnitClaims.AsManager();

        /// <summary>
        /// 单元标签数据集管理器。
        /// </summary>
        public DbSetManager<TUnitTag> UnitTagsManager
            => UnitTags.AsManager();

        /// <summary>
        /// 单元访问计数数据集管理器。
        /// </summary>
        public DbSetManager<TUnitVisitCount> UnitVisitCountsManager
            => UnitVisitCounts.AsManager();

        /// <summary>
        /// 窗格数据集管理器。
        /// </summary>
        public DbSetManager<TPane> PanesManager
            => Panes.AsManager();

        /// <summary>
        /// 窗格单元数据集管理器。
        /// </summary>
        public DbSetManager<TPaneUnit> PaneUnitsManager
            => PaneUnits.AsManager();


        /// <summary>
        /// 配置模型构建器核心。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected override void OnModelCreatingCore(ModelBuilder modelBuilder)
        {
            base.OnModelCreatingCore(modelBuilder);

            modelBuilder.ConfigureContentStores(this);
        }

    }
}
