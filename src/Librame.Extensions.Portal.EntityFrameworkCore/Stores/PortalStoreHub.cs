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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Portal.Stores
{
    using Data;
    using Data.Accessors;
    using Data.Collections;
    using Data.Stores;
    using Portal.Accessors;

    /// <summary>
    /// 门户存储中心。
    /// </summary>
    public class PortalStoreHub : PortalStoreHub<PortalDbContextAccessor>
    {
        /// <summary>
        /// 构造一个门户存储中心。
        /// </summary>
        /// <param name="initializer">给定的 <see cref="IStoreInitializer"/>。</param>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStoreHub(IStoreInitializer initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
        }

    }


    /// <summary>
    /// 门户存储中心。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class PortalStoreHub<TAccessor> : PortalStoreHub<TAccessor, Guid, int, Guid, Guid>
        where TAccessor : PortalDbContextAccessor
    {
        /// <summary>
        /// 构造一个门户存储中心。
        /// </summary>
        /// <param name="initializer">给定的 <see cref="IStoreInitializer"/>。</param>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStoreHub(IStoreInitializer initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
        }

    }


    /// <summary>
    /// 门户存储中心。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class PortalStoreHub<TAccessor, TGenId, TIncremId, TUserId, TCreatedBy>
        : PortalStoreHub<TAccessor,
            PortalEditor<TGenId, TUserId, TCreatedBy>,
            PortalInternalUser<TGenId, TCreatedBy>,
            TGenId, TIncremId, TUserId, TCreatedBy>
        where TAccessor : PortalDbContextAccessor<TGenId, TIncremId, TUserId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个门户存储中心。
        /// </summary>
        /// <param name="initializer">给定的 <see cref="IStoreInitializer"/>。</param>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStoreHub(IStoreInitializer initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
        }

    }


    /// <summary>
    /// 门户存储中心。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TInternalUser">指定的内置用户类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class PortalStoreHub<TAccessor, TEditor, TInternalUser, TGenId, TIncremId, TUserId, TCreatedBy>
        : DataStoreHub<TAccessor, TGenId, TIncremId, TCreatedBy>,
        IPortalStoreHub<TEditor, TInternalUser>
        where TAccessor : PortalDbContextAccessor<TEditor, TInternalUser, TGenId, TIncremId, TUserId, TCreatedBy>
        where TEditor : PortalEditor<TGenId, TUserId, TCreatedBy>
        where TInternalUser : PortalInternalUser<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个门户存储中心。
        /// </summary>
        /// <param name="initializer">给定的 <see cref="IStoreInitializer"/>。</param>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStoreHub(IStoreInitializer initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
        }


        /// <summary>
        /// 编者查询。
        /// </summary>
        public IQueryable<TEditor> Editors
            => Accessor.Editors;

        /// <summary>
        /// 内置用户查询。
        /// </summary>
        public IQueryable<TInternalUser> InternalUsers
            => Accessor.InternalUsers;


        #region Editor

        /// <summary>
        /// 异步查找编者。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditor"/> 的异步操作。</returns>
        public virtual ValueTask<TEditor> FindEditorAsync(CancellationToken cancellationToken, params object[] keyValues)
            => Accessor.Editors.FindAsync(keyValues, cancellationToken);

        /// <summary>
        /// 异步获取分页编者集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TEditor}"/> 的异步操作。</returns>
        public virtual ValueTask<IPageable<TEditor>> GetPagingEditorsAsync(int index, int size,
            Func<IQueryable<TEditor>, IQueryable<TEditor>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Editors.AsNoTracking()) ?? Editors.AsNoTracking();
            return query.AsPagingByIndexAsync(q => q.OrderByDescending(k => k.Rank),
                index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建编者集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        public virtual Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TEditor[] editors)
            => Accessor.Editors.TryCreateAsync(cancellationToken, editors);

        /// <summary>
        /// 尝试创建编者集合。
        /// </summary>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryCreate(params TEditor[] editors)
            => Accessor.Editors.TryCreate(editors);

        /// <summary>
        /// 尝试更新编者集合。
        /// </summary>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryUpdate(params TEditor[] editors)
            => Accessor.Editors.TryUpdate(editors);

        /// <summary>
        /// 尝试删除编者集合。
        /// </summary>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryDelete(params TEditor[] editors)
            => Accessor.Editors.TryLogicDelete(editors);

        #endregion


        #region InternalUser

        /// <summary>
        /// 异步查找内置用户。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TInternalUser"/> 的异步操作。</returns>
        public virtual ValueTask<TInternalUser> FindInternalUserAsync(CancellationToken cancellationToken, params object[] keyValues)
            => Accessor.InternalUsers.FindAsync(keyValues, cancellationToken);


        /// <summary>
        /// 尝试异步创建内置用户集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="internalUsers">给定的 <typeparamref name="TInternalUser"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        public virtual Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TInternalUser[] internalUsers)
            => Accessor.InternalUsers.TryCreateAsync(cancellationToken, internalUsers);

        /// <summary>
        /// 尝试创建内置用户集合。
        /// </summary>
        /// <param name="internalUsers">给定的 <typeparamref name="TInternalUser"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryCreate(params TInternalUser[] internalUsers)
            => Accessor.InternalUsers.TryCreate(internalUsers);

        /// <summary>
        /// 尝试更新内置用户集合。
        /// </summary>
        /// <param name="internalUsers">给定的 <typeparamref name="TInternalUser"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryUpdate(params TInternalUser[] internalUsers)
            => Accessor.InternalUsers.TryUpdate(internalUsers);

        /// <summary>
        /// 尝试删除内置用户集合。
        /// </summary>
        /// <param name="internalUsers">给定的 <typeparamref name="TInternalUser"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        public virtual OperationResult TryDelete(params TInternalUser[] internalUsers)
            => Accessor.InternalUsers.TryLogicDelete(internalUsers);

        #endregion

    }
}
