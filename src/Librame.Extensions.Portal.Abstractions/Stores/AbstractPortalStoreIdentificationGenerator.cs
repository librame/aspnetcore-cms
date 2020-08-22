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
    using Core.Identifiers;
    using Core.Services;
    using Data.Stores;

    /// <summary>
    /// 抽象门户存储标识生成器。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    public abstract class AbstractPortalStoreIdentificationGenerator<TId>
        : AbstractDataStoreIdentificationGenerator<TId>, IPortalStoreIdentificationGenerator<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractPortalStoreIdentificationGenerator{TId}"/>。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="factory">给定的 <see cref="IIdentificationGeneratorFactory"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected AbstractPortalStoreIdentificationGenerator(IClockService clock,
            IIdentificationGeneratorFactory factory, ILoggerFactory loggerFactory)
            : base(clock, factory, loggerFactory)
        {
        }


        /// <summary>
        /// 生成编者标识。
        /// </summary>
        /// <returns>返回 <typeparamref name="TId"/>。</returns>
        public virtual TId GenerateEditorId()
            => GenerateId("EditorId");

        /// <summary>
        /// 异步生成编者标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TId"/> 的异步操作。</returns>
        public virtual Task<TId> GenerateEditorIdAsync(CancellationToken cancellationToken = default)
            => GenerateIdAsync("EditorId", cancellationToken);


        /// <summary>
        /// 生成内置用户标识。
        /// </summary>
        /// <returns>返回 <typeparamref name="TId"/>。</returns>
        public virtual TId GenerateInternalUserId()
            => GenerateId("InternalUserId");

        /// <summary>
        /// 异步生成内置用户标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TId"/> 的异步操作。</returns>
        public virtual Task<TId> GenerateInternalUserIdAsync(CancellationToken cancellationToken = default)
            => GenerateIdAsync("InternalUserId", cancellationToken);
    }
}
