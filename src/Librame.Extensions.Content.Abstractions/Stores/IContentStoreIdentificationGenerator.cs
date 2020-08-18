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

namespace Librame.Extensions.Content.Stores
{
    using Data.Stores;

    /// <summary>
    /// 内容存储标识生成器接口。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    public interface IContentStoreIdentificationGenerator<TId> : IDataStoreIdentificationGenerator<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 生成单元标识。
        /// </summary>
        /// <returns>返回 <typeparamref name="TId"/>。</returns>
        TId GenerateUnitId();

        /// <summary>
        /// 异步生成单元标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TId"/> 的异步操作。</returns>
        Task<TId> GenerateUnitIdAsync(CancellationToken cancellationToken = default);
    }
}
