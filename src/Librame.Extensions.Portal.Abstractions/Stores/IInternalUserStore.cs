#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Linq;

namespace Librame.Extensions.Portal.Stores
{
    using Data.Stores;

    /// <summary>
    /// 内置用户存储接口。
    /// </summary>
    /// <typeparam name="TInternalUser">指定的内置用户类型。</typeparam>
    public interface IInternalUserStore<TInternalUser> : IStore
        where TInternalUser : class
    {
        /// <summary>
        /// 内置用户查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TInternalUser}"/>。</value>
        IQueryable<TInternalUser> InternalUsers { get; }
    }
}
