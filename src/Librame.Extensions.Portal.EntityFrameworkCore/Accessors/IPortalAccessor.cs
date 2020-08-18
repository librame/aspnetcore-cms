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
    /// 门户访问器接口。
    /// </summary>
    public interface IPortalAccessor : IPortalAccessor<Guid, int, Guid, Guid>
    {
    }


    /// <summary>
    /// 门户访问器接口。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public interface IPortalAccessor<TGenId, TIncremId, TUserId, TCreatedBy>
        : IPortalAccessor<PortalEditor<TGenId, TUserId, TCreatedBy>,
            PortalInternalUser<TGenId, TCreatedBy>>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
    }


    /// <summary>
    /// 门户访问器接口。
    /// </summary>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TInternalUser">指定的内置用户类型。</typeparam>
    public interface IPortalAccessor<TEditor, TInternalUser>
        : IAccessor // 接口不强制继承 IAccessor<TAudit, TAuditProperty, TEntity, TMigration, TTenant>
        where TEditor : class
        where TInternalUser : class
    {
        /// <summary>
        /// 编者数据集。
        /// </summary>
        DbSet<TEditor> Editors { get; set; }

        /// <summary>
        /// 内置用户数据集。
        /// </summary>
        DbSet<TInternalUser> InternalUsers { get; set; }


        /// <summary>
        /// 编者数据集管理器。
        /// </summary>
        DbSetManager<TEditor> EditorsManager { get; }

        /// <summary>
        /// 内置用户数据集管理器。
        /// </summary>
        DbSetManager<TInternalUser> InternalUsersManager { get; }
    }
}
