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
using System.Diagnostics.CodeAnalysis;

namespace Librame.Extensions.Portal.Accessors
{
    using Content.Accessors;
    using Content.Stores;
    using Data;
    using Data.Accessors;
    using Portal.Stores;

    /// <summary>
    /// 内容门户数据库上下文访问器。
    /// </summary>
    public class ContentPortalDbContextAccessor : ContentPortalDbContextAccessor<Guid, int, Guid, Guid>,
        IContentPortalAccessor, IDataAccessor
    {
        /// <summary>
        /// 构造一个内容门户数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public ContentPortalDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }

    }


    /// <summary>
    /// 内容门户数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public class ContentPortalDbContextAccessor<TGenId, TIncremId, TUserId, TPublishedBy>
        : ContentPortalDbContextAccessor<ContentCategory<TIncremId, TPublishedBy>,
            ContentSource<TIncremId, TPublishedBy>,
            ContentClaim<TIncremId, TIncremId, TPublishedBy>,
            ContentTag<TIncremId, TPublishedBy>,
            ContentUnit<TGenId, TIncremId, TIncremId, TIncremId, TPublishedBy>,
            ContentUnitClaim<TIncremId, TGenId, TIncremId, TPublishedBy>,
            ContentUnitTag<TIncremId, TGenId, TIncremId>,
            ContentUnitVisitCount<TGenId>,
            ContentPane<TIncremId, TPublishedBy>,
            ContentPaneClaim<TIncremId, TIncremId, TIncremId, TPublishedBy>,
            PortalEditor<TGenId, TUserId, TPublishedBy>,
            PortalInternalUser<TGenId, TPublishedBy>,
            TGenId, TIncremId, TUserId, TPublishedBy>,
            IContentPortalAccessor<TGenId, TIncremId, TUserId, TPublishedBy>,
            IDataAccessor<TGenId, TIncremId, TPublishedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
        /// <summary>
        /// 构造一个内容门户数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public ContentPortalDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }

    }


    /// <summary>
    /// 内容门户数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TCategory">指定的类别类型。</typeparam>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    /// <typeparam name="TClaim">指定的声明类型。</typeparam>
    /// <typeparam name="TTag">指定的标签类型。</typeparam>
    /// <typeparam name="TUnit">指定的单元类型。</typeparam>
    /// <typeparam name="TUnitClaim">指定的单元声明类型。</typeparam>
    /// <typeparam name="TUnitTag">指定的单元标签类型。</typeparam>
    /// <typeparam name="TUnitVisitCount">指定的单元统计数据类型。</typeparam>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TPaneClaim">指定的单元类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TInternalUser">指定的内置用户类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public class ContentPortalDbContextAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim, TEditor, TInternalUser, TGenId, TIncremId, TUserId, TPublishedBy>
        : ContentDbContextAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim, TGenId, TIncremId, TPublishedBy>,
            IContentPortalAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim, TEditor, TInternalUser>
        where TCategory : ContentCategory<TIncremId, TPublishedBy>
        where TSource : ContentSource<TIncremId, TPublishedBy>
        where TClaim : ContentClaim<TIncremId, TIncremId, TPublishedBy>
        where TTag : ContentTag<TIncremId, TPublishedBy>
        where TUnit : ContentUnit<TGenId, TIncremId, TIncremId, TIncremId, TPublishedBy>
        where TUnitClaim : ContentUnitClaim<TIncremId, TGenId, TIncremId, TPublishedBy>
        where TUnitTag : ContentUnitTag<TIncremId, TGenId, TIncremId>
        where TUnitVisitCount : ContentUnitVisitCount<TGenId>
        where TPane : ContentPane<TIncremId, TPublishedBy>
        where TPaneClaim : ContentPaneClaim<TIncremId, TIncremId, TIncremId, TPublishedBy>
        where TEditor : PortalEditor<TGenId, TUserId, TPublishedBy>
        where TInternalUser : PortalInternalUser<TGenId, TPublishedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
        /// <summary>
        /// 构造一个内容门户数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public ContentPortalDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }


        /// <summary>
        /// 编者数据集。
        /// </summary>
        public DbSet<TEditor> Editors { get; set; }

        /// <summary>
        /// 内置用户数据集。
        /// </summary>
        public DbSet<TInternalUser> InternalUsers { get; set; }


        /// <summary>
        /// 编者数据集管理器。
        /// </summary>
        public DbSetManager<TEditor> EditorsManager
            => Editors.AsManager();

        /// <summary>
        /// 内置用户数据集管理器。
        /// </summary>
        public DbSetManager<TInternalUser> InternalUsersManager
            => InternalUsers.AsManager();


        /// <summary>
        /// 配置模型构建器核心。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected override void OnModelCreatingCore(ModelBuilder modelBuilder)
        {
            base.OnModelCreatingCore(modelBuilder);

            modelBuilder.ConfigureContentStores(this);

            modelBuilder.ConfigurePortalStores<TEditor, TInternalUser,
                TGenId, TUserId, TPublishedBy>(this);
        }

    }
}
