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
    /// 内容访问器接口。
    /// </summary>
    public interface IContentAccessor : IContentAccessor<Guid, int, Guid>
    {
    }


    /// <summary>
    /// 内容访问器接口。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public interface IContentAccessor<TGenId, TIncremId, TPublishedBy>
        : IContentAccessor<ContentCategory<TIncremId, TPublishedBy>,
            ContentSource<TIncremId, TPublishedBy>,
            ContentClaim<TIncremId, TIncremId, TPublishedBy>,
            ContentTag<TIncremId, TPublishedBy>,
            ContentUnit<TGenId, TIncremId, TIncremId, TIncremId, TPublishedBy>,
            ContentUnitClaim<TIncremId, TGenId, TIncremId, TPublishedBy>,
            ContentUnitTag<TIncremId, TGenId, TIncremId>,
            ContentUnitVisitCount<TGenId>,
            ContentPane<TIncremId, TPublishedBy>,
            ContentPaneClaim<TIncremId, TIncremId, TIncremId, TPublishedBy>>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
    }


    /// <summary>
    /// 内容访问器接口。
    /// </summary>
    /// <typeparam name="TCategory">指定的类别类型。</typeparam>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    /// <typeparam name="TClaim">指定的声明类型。</typeparam>
    /// <typeparam name="TTag">指定的标签类型。</typeparam>
    /// <typeparam name="TUnit">指定的单元类型。</typeparam>
    /// <typeparam name="TUnitClaim">指定的单元声明类型。</typeparam>
    /// <typeparam name="TUnitTag">指定的单元标签类型。</typeparam>
    /// <typeparam name="TUnitVisitCount">指定的单元访问计数类型。</typeparam>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TPaneClaim">指定的单元类型。</typeparam>
    public interface IContentAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim>
        : IAccessor // 接口不强制继承 IAccessor<TAudit, TAuditProperty, TEntity, TMigration, TTenant>
        where TCategory : class
        where TSource : class
        where TClaim : class
        where TTag : class
        where TUnit : class
        where TUnitClaim : class
        where TUnitTag : class
        where TUnitVisitCount : class
        where TPane : class
        where TPaneClaim : class
    {
        /// <summary>
        /// 类别数据集。
        /// </summary>
        DbSet<TCategory> Categories { get; set; }

        /// <summary>
        /// 来源数据集。
        /// </summary>
        DbSet<TSource> Sources { get; set; }

        /// <summary>
        /// 声明数据集。
        /// </summary>
        DbSet<TClaim> Claims { get; set; }

        /// <summary>
        /// 标签数据集。
        /// </summary>
        DbSet<TTag> Tags { get; set; }

        /// <summary>
        /// 单元数据集。
        /// </summary>
        DbSet<TUnit> Units { get; set; }

        /// <summary>
        /// 单元声明数据集。
        /// </summary>
        DbSet<TUnitClaim> UnitClaims { get; set; }

        /// <summary>
        /// 单元标签数据集。
        /// </summary>
        DbSet<TUnitTag> UnitTags { get; set; }

        /// <summary>
        /// 单元访问计数数据集。
        /// </summary>
        DbSet<TUnitVisitCount> UnitVisitCounts { get; set; }

        /// <summary>
        /// 窗格数据集。
        /// </summary>
        DbSet<TPane> Panes { get; set; }

        /// <summary>
        /// 窗格声明数据集。
        /// </summary>
        DbSet<TPaneClaim> PaneClaims { get; set; }


        /// <summary>
        /// 类别数据集管理器。
        /// </summary>
        DbSetManager<TCategory> CategoriesManager { get; }

        /// <summary>
        /// 来源数据集管理器。
        /// </summary>
        DbSetManager<TSource> SourcesManager { get; }

        /// <summary>
        /// 声明数据集管理器。
        /// </summary>
        DbSetManager<TClaim> ClaimsManager { get; }

        /// <summary>
        /// 标签数据集管理器。
        /// </summary>
        DbSetManager<TTag> TagsManager { get; }

        /// <summary>
        /// 单元数据集管理器。
        /// </summary>
        DbSetManager<TUnit> UnitsManager { get; }

        /// <summary>
        /// 单元声明数据集管理器。
        /// </summary>
        DbSetManager<TUnitClaim> UnitClaimsManager { get; }

        /// <summary>
        /// 单元标签数据集管理器。
        /// </summary>
        DbSetManager<TUnitTag> UnitTagsManager { get; }

        /// <summary>
        /// 单元访问计数数据集管理器。
        /// </summary>
        DbSetManager<TUnitVisitCount> UnitVisitCountsManager { get; }

        /// <summary>
        /// 窗格数据集管理器。
        /// </summary>
        DbSetManager<TPane> PanesManager { get; }

        /// <summary>
        /// 窗格声明数据集管理器。
        /// </summary>
        DbSetManager<TPaneClaim> PaneClaimsManager { get; }
    }
}
