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
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Portal.Stores
{
    using Data;
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


        /// <summary>
        /// 异步查找内置用户。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TInternalUser"/> 的异步操作。</returns>
        ValueTask<TInternalUser> FindInternalUserAsync(CancellationToken cancellationToken, params object[] keyValues);


        /// <summary>
        /// 尝试异步创建内置用户集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="internalUsers">给定的 <typeparamref name="TInternalUser"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="OperationResult"/> 的异步操作。</returns>
        Task<OperationResult> TryCreateAsync(CancellationToken cancellationToken,
            params TInternalUser[] internalUsers);

        /// <summary>
        /// 尝试创建内置用户集合。
        /// </summary>
        /// <param name="internalUsers">给定的 <typeparamref name="TInternalUser"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TInternalUser[] internalUsers);

        /// <summary>
        /// 尝试更新内置用户集合。
        /// </summary>
        /// <param name="internalUsers">给定的 <typeparamref name="TInternalUser"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryUpdate(params TInternalUser[] internalUsers);

        /// <summary>
        /// 尝试删除内置用户集合。
        /// </summary>
        /// <param name="internalUsers">给定的 <typeparamref name="TInternalUser"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryDelete(params TInternalUser[] internalUsers);
    }
}
