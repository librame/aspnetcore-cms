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

namespace Librame.AspNetCore.Portal
{
    using Extensions.Data;

    /// <summary>
    /// 门户访问器模型构建器静态扩展。
    /// </summary>
    public static class PortalAccessorModelBuilderExtensions
    {
        /// <summary>
        /// 配置门户存储。
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
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="PortalBuilderOptions"/>。</param>
        public static void ConfigurePortalStore<TCategory, TEditor, TEditorTitle, TPane, TSource, TSubject, TSubjectBody, TTag, TTagReference, TGenId, TIncremId, TUserId>
            (this ModelBuilder modelBuilder, PortalBuilderOptions options)
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
            var mapRelationship = options.Stores?.MapRelationship ?? true;
            var maxLength = options.Stores?.MaxLengthForProperties ?? 0;

            modelBuilder.Entity<TCategory>(b =>
            {
                b.ToTable(options.Tables.CategoryFactory);

                b.HasKey(k => k.Id);
                
                b.HasIndex(i => new { i.ParentId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(256);

                if (maxLength > 0)
                {
                    b.Property(p => p.Descr).HasMaxLength(maxLength);
                    b.Property(p => p.CreatedBy).HasMaxLength(maxLength);
                }

                if (mapRelationship)
                {
                    b.HasMany<TPane>().WithOne().HasForeignKey(fk => fk.CategoryId).IsRequired();
                    b.HasMany<TSource>().WithOne().HasForeignKey(fk => fk.CategoryId).IsRequired();
                    b.HasMany<TSubject>().WithOne().HasForeignKey(fk => fk.CategoryId).IsRequired();
                }
            });

            modelBuilder.Entity<TEditor>(b =>
            {
                b.ToTable(options.Tables.EditorFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.UserId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.UserId).HasMaxLength(256);
                b.Property(p => p.Name).HasMaxLength(256);

                if (maxLength > 0)
                {
                    b.Property(p => p.CreatedBy).HasMaxLength(maxLength);
                }

                if (mapRelationship)
                {
                    b.HasMany<TEditorTitle>().WithOne().HasForeignKey(fk => fk.EditorId).IsRequired();
                }
            });
            modelBuilder.Entity<TEditorTitle>(b =>
            {
                b.ToTable(options.Tables.EditorTitleFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.EditorId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(256);

                if (maxLength > 0)
                {
                    b.Property(p => p.CreatedBy).HasMaxLength(maxLength);
                }
            });

            modelBuilder.Entity<TPane>(b =>
            {
                b.ToTable(options.Tables.PaneFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.CategoryId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(256);

                if (maxLength > 0)
                {
                    b.Property(p => p.Path).HasMaxLength(maxLength);
                    b.Property(p => p.CreatedBy).HasMaxLength(maxLength);
                }
            });

            modelBuilder.Entity<TSource>(b =>
            {
                b.ToTable(options.Tables.SourceFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.CategoryId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(256);

                if (maxLength > 0)
                {
                    b.Property(p => p.Logo).HasMaxLength(maxLength);
                    b.Property(p => p.Link).HasMaxLength(maxLength);
                    b.Property(p => p.Descr).HasMaxLength(maxLength);
                    b.Property(p => p.CreatedBy).HasMaxLength(maxLength);
                }
            });

            modelBuilder.Entity<TSubject>(b =>
            {
                b.ToTable(options.Tables.SubjectFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.CategoryId, i.Title }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Title).HasMaxLength(256);

                if (maxLength > 0)
                {
                    b.Property(p => p.PublishLink).HasMaxLength(maxLength);
                    b.Property(p => p.Subtitle).HasMaxLength(maxLength);
                    b.Property(p => p.CreatedBy).HasMaxLength(maxLength);
                }

                if (mapRelationship)
                {
                    b.HasMany<TSubjectBody>().WithOne().HasForeignKey(fk => fk.SubjectId).IsRequired();
                }
            });
            modelBuilder.Entity<TSubjectBody>(b =>
            {
                b.ToTable(options.Tables.SubjectBodyFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.SubjectId, i.TextHash }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.TextHash).HasMaxLength(256);
                b.Property(p => p.Text).HasMaxLength(4000);
            });

            modelBuilder.Entity<TTag>(b =>
            {
                b.ToTable(options.Tables.TagFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.Name).HasName().IsUnique();

                b.Property(p => p.Id).HasMaxLength(256);
                b.Property(p => p.Name).HasMaxLength(256);

                if (maxLength > 0)
                {
                    b.Property(p => p.CreatedBy).HasMaxLength(maxLength);
                }

                if (mapRelationship)
                {
                    b.HasMany<TTagReference>().WithOne().HasForeignKey(fk => fk.TagId).IsRequired();
                }
            });
            modelBuilder.Entity<TTagReference>(b =>
            {
                b.ToTable(options.Tables.TagReferenceFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.TagId, i.ReferEntityId, i.ReferId }).HasName().IsUnique();

                b.Property(p => p.TagId).HasMaxLength(256);
                b.Property(p => p.ReferEntityId).HasMaxLength(256);
                b.Property(p => p.ReferId).HasMaxLength(256);

                if (maxLength > 0)
                {
                    b.Property(p => p.ReferUrl).HasMaxLength(maxLength);
                    b.Property(p => p.CreatedBy).HasMaxLength(maxLength);
                }
            });
        }

    }
}
