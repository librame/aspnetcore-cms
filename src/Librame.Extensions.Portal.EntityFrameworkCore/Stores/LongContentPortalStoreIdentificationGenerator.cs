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
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Portal.Stores
{
    using Core.Identifiers;
    using Core.Services;

    /// <summary>
    /// <see cref="long"/> 内容门户存储标识生成器。
    /// </summary>
    public class LongContentPortalStoreIdentificationGenerator : AbstractContentPortalStoreIdentificationGenerator<long>
    {
        /// <summary>
        /// 构造一个 <see cref="LongPortalStoreIdentificationGenerator"/>。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="factory">给定的 <see cref="IIdentificationGeneratorFactory"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public LongContentPortalStoreIdentificationGenerator(IClockService clock,
            IIdentificationGeneratorFactory factory, ILoggerFactory loggerFactory)
            : base(clock, factory, loggerFactory)
        {
        }


        /// <summary>
        /// 生成标识。
        /// </summary>
        /// <param name="idName">给定的标识名称。</param>
        /// <returns>返回 <see cref="long"/>。</returns>
        public virtual long GenerateId(string idName)
            => GenerateId<long>(idName);

        /// <summary>
        /// 异步生成标识。
        /// </summary>
        /// <param name="idName">给定的标识名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="long"/> 的异步操作。</returns>
        public virtual Task<long> GenerateIdAsync(string idName,
            CancellationToken cancellationToken = default)
            => GenerateIdAsync<long>(idName, cancellationToken);

    }
}
