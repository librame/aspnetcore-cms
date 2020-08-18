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

namespace Librame.Extensions.Portal.Accessors
{
    using Data;
    using Data.Accessors;
    using Portal.Stores;

    /// <summary>
    /// 门户数据库上下文访问器。
    /// </summary>
    public class PortalDbContextAccessor : PortalDbContextAccessor<Guid, int, Guid, Guid>,
        IPortalAccessor, IDataAccessor
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
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class PortalDbContextAccessor<TGenId, TIncremId, TUserId, TCreatedBy>
        : PortalDbContextAccessor<
            PortalEditor<TGenId, TUserId, TCreatedBy>,
            PortalInternalUser<TGenId, TCreatedBy>,
            TGenId, TIncremId, TUserId, TCreatedBy>,
            IDataAccessor<TGenId, TIncremId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个门户数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        protected PortalDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }

    }


    /// <summary>
    /// 门户数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TInternalUser">指定的内置用户类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class PortalDbContextAccessor<TEditor, TInternalUser, TGenId, TIncremId, TUserId, TCreatedBy>
        : DataDbContextAccessor<TGenId, TIncremId, TCreatedBy>,
            IPortalAccessor<TEditor, TInternalUser>
        where TEditor : PortalEditor<TGenId, TUserId, TCreatedBy>
        where TInternalUser : PortalInternalUser<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个门户数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        protected PortalDbContextAccessor(DbContextOptions options)
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
        protected override void OnModelCreatingCore(ModelBuilder modelBuilder)
        {
            base.OnModelCreatingCore(modelBuilder);

            modelBuilder.ConfigurePortalStores(this);
        }

    }
}
