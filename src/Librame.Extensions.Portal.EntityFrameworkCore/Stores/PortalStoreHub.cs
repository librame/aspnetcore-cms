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

namespace Librame.Extensions.Portal.Stores
{
    using Data.Accessors;
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
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStoreHub(IAccessor accessor)
            : base(accessor)
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
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStoreHub(IAccessor accessor)
            : base(accessor)
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
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        protected PortalStoreHub(IAccessor accessor)
            : base(accessor)
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
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        protected PortalStoreHub(IAccessor accessor)
            : base(accessor)
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
    }
}
