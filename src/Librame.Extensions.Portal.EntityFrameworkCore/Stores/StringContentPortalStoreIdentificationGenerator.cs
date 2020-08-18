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
    /// <see cref="string"/> 内容门户存储标识生成器。
    /// </summary>
    public class StringContentPortalStoreIdentificationGenerator : AbstractContentPortalStoreIdentificationGenerator<string>
    {
        /// <summary>
        /// 构造一个 <see cref="StringPortalStoreIdentificationGenerator"/>。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="factory">给定的 <see cref="IIdentificationGeneratorFactory"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public StringContentPortalStoreIdentificationGenerator(IClockService clock,
            IIdentificationGeneratorFactory factory, ILoggerFactory loggerFactory)
            : base(clock, factory, loggerFactory)
        {
        }


        /// <summary>
        /// 生成标识。
        /// </summary>
        /// <param name="idName">给定的标识名称。</param>
        /// <returns>返回 <see cref="string"/>。</returns>
        public virtual string GenerateId(string idName)
            => GenerateId<string>(idName);

        /// <summary>
        /// 异步生成标识。
        /// </summary>
        /// <param name="idName">给定的标识名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="string"/> 的异步操作。</returns>
        public virtual Task<string> GenerateIdAsync(string idName,
            CancellationToken cancellationToken = default)
            => GenerateIdAsync<string>(idName, cancellationToken);

    }
}
