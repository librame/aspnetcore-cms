#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Portal.Stores
{
    using Data.Stores;

    /// <summary>
    /// 门户存储标识生成器接口。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    public interface IPortalStoreIdentificationGenerator<TId> : IDataStoreIdentificationGenerator<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 生成编者标识。
        /// </summary>
        /// <returns>返回 <typeparamref name="TId"/>。</returns>
        TId GenerateEditorId();

        /// <summary>
        /// 异步生成编者标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TId"/> 的异步操作。</returns>
        Task<TId> GenerateEditorIdAsync(CancellationToken cancellationToken = default);


        /// <summary>
        /// 生成内置用户标识。
        /// </summary>
        /// <returns>返回 <typeparamref name="TId"/>。</returns>
        TId GenerateInternalUserId();

        /// <summary>
        /// 异步生成内置用户标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TId"/> 的异步操作。</returns>
        Task<TId> GenerateInternalUserIdAsync(CancellationToken cancellationToken = default);
    }
}
