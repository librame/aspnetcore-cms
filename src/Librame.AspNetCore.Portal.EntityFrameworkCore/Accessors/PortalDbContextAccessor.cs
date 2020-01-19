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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Librame.AspNetCore.Portal
{
    using Extensions.Data;

    /// <summary>
    /// 门户数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    public class PortalDbContextAccessor<TUserId> : PortalDbContextAccessor<PortalPane<int, int>, string, int, TUserId>
        where TUserId : IEquatable<TUserId>
    {
        /// <summary>
        /// 构造一个门户数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public PortalDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }
    }


    /// <summary>
    /// 门户数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    public class PortalDbContextAccessor<TPane, TGenId, TIncremId, TUserId>
        : PortalDbContextAccessor<PortalCategory<TIncremId>, PortalEditor<TGenId, TUserId>, PortalEditorTitle<TIncremId, TGenId>, TPane, PortalSource<TIncremId, TIncremId>, PortalSubject<TGenId, TIncremId>, PortalSubjectBody<TIncremId, TGenId>, PortalTag<TGenId>, PortalTagReference<TIncremId, TGenId>, TGenId, TIncremId, TUserId>
        where TPane : PortalPane<TIncremId, TIncremId>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
    {
        /// <summary>
        /// 构造一个门户数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public PortalDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }
    }


    /// <summary>
    /// 门户数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TCategory">指定的分类类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TEditorTitle">指定的编者头衔类型。</typeparam>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TSubjectBody">指定的专题主体类型。</typeparam>
    /// <typeparam name="TTag">指定的标签类型。</typeparam>
    /// <typeparam name="TTagReference">指定的标签引用类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    public class PortalDbContextAccessor<TCategory, TEditor, TEditorTitle, TPane, TSource, TSubject, TSubjectBody, TTag, TTagReference, TGenId, TIncremId, TUserId> : DbContextAccessor
        , IPortalDbContextAccessor<TCategory, TEditor, TEditorTitle, TPane, TSource, TSubject, TSubjectBody, TTag, TTagReference>
        where TCategory : PortalCategory<TIncremId>
        where TEditor : PortalEditor<TGenId, TUserId>
        where TEditorTitle : PortalEditorTitle<TIncremId, TGenId>
        where TPane : PortalPane<TIncremId, TIncremId>
        where TSource : PortalSource<TIncremId, TIncremId>
        where TSubject : PortalSubject<TGenId, TIncremId>
        where TSubjectBody : PortalSubjectBody<TIncremId, TGenId>
        where TTag : PortalTag<TGenId>
        where TTagReference : PortalTagReference<TIncremId, TGenId>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
    {
        /// <summary>
        /// 构造一个门户数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public PortalDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }


        /// <summary>
        /// 分类数据集。
        /// </summary>
        public DbSet<TCategory> Categories { get; set; }

        /// <summary>
        /// 编者数据集。
        /// </summary>
        public DbSet<TEditor> Editors { get; set; }

        /// <summary>
        /// 编者头衔数据集。
        /// </summary>
        public DbSet<TEditorTitle> EditorTitles { get; set; }

        /// <summary>
        /// 窗格数据集。
        /// </summary>
        public DbSet<TPane> Panes { get; set; }

        /// <summary>
        /// 来源数据集。
        /// </summary>
        public DbSet<TSource> Sources { get; set; }

        /// <summary>
        /// 专题数据集。
        /// </summary>
        public DbSet<TSubject> Subjects { get; set; }

        /// <summary>
        /// 专题主体数据集。
        /// </summary>
        public DbSet<TSubjectBody> SubjectBodies { get; set; }

        /// <summary>
        /// 标签数据集。
        /// </summary>
        public DbSet<TTag> Tags { get; set; }

        /// <summary>
        /// 标签引用数据集。
        /// </summary>
        public DbSet<TTagReference> TagReferences { get; set; }


        /// <summary>
        /// 开始创建模型。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var options = ServiceProvider.GetRequiredService<IOptions<PortalBuilderOptions>>().Value;

            modelBuilder.ConfigurePortalStore<TCategory, TEditor, TEditorTitle,
                TPane, TSource, TSubject, TSubjectBody, TTag, TTagReference, TGenId, TIncremId, TUserId>(options);
        }
    }
}
