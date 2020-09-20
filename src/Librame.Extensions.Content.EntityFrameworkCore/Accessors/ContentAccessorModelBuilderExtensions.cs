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
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Librame.Extensions.Content.Accessors
{
    using Content.Builders;
    using Content.Stores;
    using Data;
    using Data.Accessors;

    /// <summary>
    /// 内容访问器模型构建器静态扩展。
    /// </summary>
    public static class ContentAccessorModelBuilderExtensions
    {
        /// <summary>
        /// 配置内容存储集合。
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
        /// <typeparam name="TPaneClaim">指定的窗格声明类型。</typeparam>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        /// <param name="accessor">给定的 <see cref="DbContextAccessorBase"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static void ConfigureContentStores<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim, TGenId, TIncremId, TPublishedBy>
            (this ModelBuilder modelBuilder, ContentDbContextAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim, TGenId, TIncremId, TPublishedBy> accessor)
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
            where TGenId : IEquatable<TGenId>
            where TIncremId : IEquatable<TIncremId>
            where TPublishedBy : IEquatable<TPublishedBy>
            => modelBuilder.ConfigureContentStores<TCategory, TSource, TClaim, TTag,
                TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim,
                TGenId, TIncremId, TPublishedBy>(accessor as DbContextAccessorBase);

        /// <summary>
        /// 配置内容存储集合。
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
        /// <typeparam name="TPaneClaim">指定的窗格声明类型。</typeparam>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        /// <param name="accessor">给定的 <see cref="DbContextAccessorBase"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static void ConfigureContentStores<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim, TGenId, TIncremId, TPublishedBy>
            (this ModelBuilder modelBuilder, DbContextAccessorBase accessor)
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
            where TGenId : IEquatable<TGenId>
            where TIncremId : IEquatable<TIncremId>
            where TPublishedBy : IEquatable<TPublishedBy>
        {
            modelBuilder.NotNull(nameof(modelBuilder));
            accessor.NotNull(nameof(accessor));

            var options = accessor.GetService<IOptions<ContentBuilderOptions>>().Value;

            var mapRelationship = options.Stores?.MapRelationship ?? true;
            var useContentPrefix = options.Tables.UseContentPrefix;

            modelBuilder.Entity<TCategory>(b =>
            {
                b.ToTable(table =>
                {
                    if (useContentPrefix)
                        table.InsertContentPrefix();

                    table.Configure(options.Tables.Category);
                });

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.ParentId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.ParentId).HasDefaultValue(default(TIncremId));
                b.Property(p => p.Name).HasMaxLength(50).IsRequired();
                b.Property(p => p.Description).HasMaxLength(256);

                if (mapRelationship)
                {
                    b.HasMany<TUnit>().WithOne().HasForeignKey(fk => fk.CategoryId).IsRequired();
                }
            });

            modelBuilder.Entity<TSource>(b =>
            {
                b.ToTable(table =>
                {
                    if (useContentPrefix)
                        table.InsertContentPrefix();

                    table.Configure(options.Tables.Source);
                });

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.ParentId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.ParentId).HasDefaultValue(default(TIncremId));
                b.Property(p => p.Name).HasMaxLength(50).IsRequired();
                b.Property(p => p.Description).HasMaxLength(256);
                b.Property(p => p.Website).HasMaxLength(256);
                b.Property(p => p.Weblogo).HasMaxLength(256);

                if (mapRelationship)
                {
                    b.HasMany<TUnit>().WithOne().HasForeignKey(fk => fk.SourceId).IsRequired();
                }
            });

            modelBuilder.Entity<TClaim>(b =>
            {
                b.ToTable(table =>
                {
                    if (useContentPrefix)
                        table.InsertContentPrefix();

                    table.Configure(options.Tables.Claim);
                });

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.Name).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(50).IsRequired();
                b.Property(p => p.Description).HasMaxLength(256);

                if (mapRelationship)
                {
                    b.HasMany<TPaneClaim>().WithOne().HasForeignKey(fk => fk.ClaimId).IsRequired();
                    b.HasMany<TUnitClaim>().WithOne().HasForeignKey(fk => fk.ClaimId).IsRequired();
                }
            });

            modelBuilder.Entity<TTag>(b =>
            {
                b.ToTable(table =>
                {
                    if (useContentPrefix)
                        table.InsertContentPrefix();

                    table.Configure(options.Tables.Tag);
                });

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.Name).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(50).IsRequired();

                if (mapRelationship)
                {
                    b.HasMany<TUnitTag>().WithOne().HasForeignKey(fk => fk.TagId).IsRequired();
                }
            });

            modelBuilder.Entity<TUnit>(b =>
            {
                b.ToTable(table =>
                {
                    if (useContentPrefix)
                        table.InsertContentPrefix();

                    // 按季度分表
                    table.AppendYearAndQuarterSuffix(accessor.CurrentTimestamp);
                    table.Configure(options.Tables.Unit);
                });

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.CategoryId, i.Title }).HasName().IsUnique();
                b.HasIndex(i => new { i.PublishedBy, i.PublishedTime }).HasName();

                b.Property(p => p.Title).HasMaxLength(256).IsRequired();
                b.Property(p => p.Subtitle).HasMaxLength(256);
                b.Property(p => p.Reference).HasMaxLength(256);
                b.Property(p => p.PublishedAs).HasMaxLength(256);

                if (mapRelationship)
                {
                    b.HasMany<TCategory>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                    b.HasMany<TPane>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                    b.HasMany<TSource>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                    b.HasMany<TUnitClaim>().WithOne().HasForeignKey(fk => fk.UnitId).IsRequired();
                    b.HasMany<TUnitVisitCount>().WithOne().HasForeignKey(fk => fk.UnitId).IsRequired();
                }
            });

            modelBuilder.Entity<TUnitClaim>(b =>
            {
                b.ToTable(table =>
                {
                    if (useContentPrefix)
                        table.InsertContentPrefix();

                    table.Configure(options.Tables.UnitClaim);
                });

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.UnitId, i.ClaimId }).HasName();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.ClaimValue).IsRequired(); // 不限长度

                if (mapRelationship)
                {
                    b.HasMany<TClaim>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                    b.HasMany<TUnit>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                }
            });

            modelBuilder.Entity<TUnitTag>(b =>
            {
                b.ToTable(table =>
                {
                    if (useContentPrefix)
                        table.InsertContentPrefix();

                    table.Configure(options.Tables.UnitTag);
                });

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.UnitId, i.TagId }).HasName();

                b.Property(p => p.Id).ValueGeneratedOnAdd();

                if (mapRelationship)
                {
                    b.HasMany<TTag>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                    b.HasMany<TUnit>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                }
            });

            modelBuilder.Entity<TUnitVisitCount>(b =>
            {
                b.ToTable(table =>
                {
                    if (useContentPrefix)
                        table.InsertContentPrefix();

                    table.Configure(options.Tables.UnitVisitCount);
                });

                b.HasKey(k => k.UnitId);

                b.Property(p => p.SupporterCount).HasDefaultValue(0);
                b.Property(p => p.ObjectorCount).HasDefaultValue(0);
                b.Property(p => p.FavoriteCount).HasDefaultValue(0);
                b.Property(p => p.RetweetCount).HasDefaultValue(0);

                b.Property(p => p.VisitCount).HasDefaultValue(0);
                b.Property(p => p.VisitorCount).HasDefaultValue(0);

                if (mapRelationship)
                {
                    b.HasMany<TUnit>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                }
            });

            modelBuilder.Entity<TPane>(b =>
            {
                b.ToTable(table =>
                {
                    if (useContentPrefix)
                        table.InsertContentPrefix();

                    table.Configure(options.Tables.Pane);
                });

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.ParentId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.ParentId).HasDefaultValue(default(TIncremId));
                b.Property(p => p.Name).HasMaxLength(256);
                b.Property(p => p.Description).HasMaxLength(256);
                b.Property(p => p.Icon).HasMaxLength(256);
                b.Property(p => p.More).HasMaxLength(256);

                if (mapRelationship)
                {
                    b.HasMany<TPaneClaim>().WithOne().HasForeignKey(fk => fk.PaneId).IsRequired();
                    b.HasMany<TUnit>().WithOne().HasForeignKey(fk => fk.PaneId).IsRequired();
                }
            });

            modelBuilder.Entity<TPaneClaim>(b =>
            {
                b.ToTable(table =>
                {
                    if (useContentPrefix)
                        table.InsertContentPrefix();

                    table.Configure(options.Tables.PaneClaim);
                });

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.PaneId, i.ClaimId }).HasName();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.ClaimValue).IsRequired(); // 不限长度

                if (mapRelationship)
                {
                    b.HasMany<TClaim>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                    b.HasMany<TPane>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                }
            });
        }

    }
}
