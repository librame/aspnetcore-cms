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

    }
}
