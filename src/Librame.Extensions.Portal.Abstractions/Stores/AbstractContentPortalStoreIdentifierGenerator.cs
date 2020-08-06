#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Portal.Stores
{
    using Content.Stores;
    using Core.Identifiers;
    using Core.Services;
    using Data.Stores;

    /// <summary>
    /// 抽象内容门户存储标识符生成器。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    public abstract class AbstractContentPortalStoreIdentifierGenerator<TId>
        : AbstractDataStoreIdentifierGenerator<TId>,
        IContentStoreIdentityGenerator<TId>,
        IPortalStoreIdentifierGenerator<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractContentPortalStoreIdentifierGenerator{TId}"/>。
        /// </summary>
        /// <param name="generator">给定的 <see cref="IIdentifierGenerator{TId}"/>。</param>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected AbstractContentPortalStoreIdentifierGenerator(IIdentifierGenerator<TId> generator,
            IClockService clock, ILoggerFactory loggerFactory)
            : base(generator, clock, loggerFactory)
        {
        }


        /// <summary>
        /// 异步生成单元标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TId"/> 的异步操作。</returns>
        public virtual Task<TId> GenerateUnitIdAsync(CancellationToken cancellationToken = default)
            => GenerateIdAsync("UnitId", cancellationToken);

        /// <summary>
        /// 异步生成编者标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TId"/> 的异步操作。</returns>
        public virtual Task<TId> GenerateEditorIdAsync(CancellationToken cancellationToken = default)
            => GenerateIdAsync("EditorId", cancellationToken);

        /// <summary>
        /// 异步生成内置用户标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TId"/> 的异步操作。</returns>
        public virtual Task<TId> GenerateInternalUserIdAsync(CancellationToken cancellationToken = default)
            => GenerateIdAsync("InternalUserId", cancellationToken);
    }
}
