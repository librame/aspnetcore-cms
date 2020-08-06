﻿#region License

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

namespace Librame.Extensions.Content.Stores
{
    using Core.Identifiers;
    using Core.Services;
    using Data.Stores;

    /// <summary>
    /// 抽象内容存储标识生成器。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    public abstract class AbstractContentStoreIdentityGenerator<TId>
        : AbstractDataStoreIdentityGenerator<TId>, IContentStoreIdentityGenerator<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractContentStoreIdentityGenerator{TId}"/>。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="factory">给定的 <see cref="IIdentityGeneratorFactory"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected AbstractContentStoreIdentityGenerator(IClockService clock,
            IIdentityGeneratorFactory factory, ILoggerFactory loggerFactory)
            : base(clock, factory, loggerFactory)
        {
        }


        /// <summary>
        /// 异步生成单元标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TId"/> 的异步操作。</returns>
        public virtual Task<TId> GenerateUnitIdAsync(CancellationToken cancellationToken = default)
            => GenerateIdAsync<TId>("UnitId", cancellationToken);
    }
}