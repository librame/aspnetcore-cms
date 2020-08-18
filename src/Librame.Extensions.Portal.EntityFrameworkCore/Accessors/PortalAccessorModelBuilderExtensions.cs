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
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Librame.Extensions.Portal.Accessors
{
    using Data.Accessors;
    using Portal.Builders;
    using Portal.Stores;

    /// <summary>
    /// 门户访问器模型构建器静态扩展。
    /// </summary>
    public static class PortalAccessorModelBuilderExtensions
    {
        /// <summary>
        /// 配置门户存储。
        /// </summary>
        /// <typeparam name="TEditor">指定的编者类型。</typeparam>
        /// <typeparam name="TInternalUser">指定的内置用户类型。</typeparam>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        /// <param name="accessor">给定的 <see cref="DbContextAccessorBase"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static void ConfigurePortalStores<TEditor, TInternalUser, TGenId, TIncremId, TUserId, TCreatedBy>
            (this ModelBuilder modelBuilder, PortalDbContextAccessor<TEditor, TInternalUser, TGenId, TIncremId, TUserId, TCreatedBy> accessor)
            where TEditor : PortalEditor<TGenId, TUserId, TCreatedBy>
            where TInternalUser : PortalInternalUser<TGenId, TCreatedBy>
            where TGenId : IEquatable<TGenId>
            where TIncremId : IEquatable<TIncremId>
            where TUserId : IEquatable<TUserId>
            where TCreatedBy : IEquatable<TCreatedBy>
            => modelBuilder.ConfigurePortalStores<TEditor, TInternalUser, TGenId, TUserId, TCreatedBy>(accessor);

        /// <summary>
        /// 配置门户存储。
        /// </summary>
        /// <typeparam name="TEditor">指定的编者类型。</typeparam>
        /// <typeparam name="TInternalUser">指定的内置用户类型。</typeparam>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        /// <param name="accessor">给定的 <see cref="DbContextAccessorBase"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static void ConfigurePortalStores<TEditor, TInternalUser, TGenId, TUserId, TCreatedBy>
            (this ModelBuilder modelBuilder, DbContextAccessorBase accessor)
            where TEditor : PortalEditor<TGenId, TUserId, TCreatedBy>
            where TInternalUser : PortalInternalUser<TGenId, TCreatedBy>
            where TGenId : IEquatable<TGenId>
            where TUserId : IEquatable<TUserId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            modelBuilder.NotNull(nameof(modelBuilder));
            accessor.NotNull(nameof(accessor));

            var options = accessor.GetService<IOptions<PortalBuilderOptions>>().Value;

            var mapRelationship = options.Stores?.MapRelationship ?? true;
            var usePortalPrefix = options.Tables.UsePortalPrefix;

            modelBuilder.Entity<TEditor>(b =>
            {
                b.ToTable(table =>
                {
                    if (usePortalPrefix)
                        table.InsertPortalPrefix();

                    table.Configure(options.Tables.Editor);
                });

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.UserId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Name).HasMaxLength(256);
                b.Property(p => p.Description).HasMaxLength(256);
                b.Property(p => p.Portrait).HasMaxLength(256);
            });

            modelBuilder.Entity<TInternalUser>(b =>
            {
                b.ToTable(table =>
                {
                    if (usePortalPrefix)
                        table.InsertPortalPrefix();

                    table.Configure(options.Tables.InternalUser);
                });

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.Name).HasName().IsUnique();

                b.Property(p => p.Name).HasMaxLength(256);
            });
        }

    }
}
