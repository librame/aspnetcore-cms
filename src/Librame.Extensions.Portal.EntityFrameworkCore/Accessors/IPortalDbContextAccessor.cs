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

namespace Librame.Extensions.Portal.Accessors
{
    using Data.Accessors;

    /// <summary>
    /// 门户数据库上下文访问器接口。
    /// </summary>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TInternalUser">指定的内置用户类型。</typeparam>
    public interface IPortalDbContextAccessor<TEditor, TInternalUser> : IAccessor
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
    }
}
