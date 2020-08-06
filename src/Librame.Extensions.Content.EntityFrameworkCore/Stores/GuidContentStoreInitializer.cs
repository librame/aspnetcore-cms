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
using Microsoft.Extensions.Options;
using System;

namespace Librame.Extensions.Content.Stores
{
    using Content.Builders;
    using Content.Accessors;
    using Data.Stores;

    /// <summary>
    /// <see cref="Guid"/> 内容存储初始化器。
    /// </summary>
    public class GuidContentStoreInitializer : GuidContentStoreInitializer<ContentDbContextAccessor>
    {
        /// <summary>
        /// 构造一个 <see cref="GuidContentStoreInitializer{TAccessor}"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{ContentBuilderOptions}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidContentStoreInitializer(IOptions<ContentBuilderOptions> options,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(options, identifierGenerator, validator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// <see cref="Guid"/> 内容存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class GuidContentStoreInitializer<TAccessor> : ContentStoreInitializer<TAccessor, Guid, int, Guid>
        where TAccessor : ContentDbContextAccessor
    {
        /// <summary>
        /// 构造一个 <see cref="GuidContentStoreInitializer{TAccessor}"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{ContentBuilderOptions}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidContentStoreInitializer(IOptions<ContentBuilderOptions> options,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(options?.Value.Stores.Initialization, identifierGenerator, validator, loggerFactory)
        {
        }


        /// <summary>
        /// 累加增量标识。
        /// </summary>
        /// <param name="index">给定的索引。</param>
        /// <returns>返回整数。</returns>
        protected override int ProgressiveIncremId(int index)
            => ++index;

        /// <summary>
        /// 将生成式标识发表为查询参数值。
        /// </summary>
        /// <param name="id">给定的 <see cref="Guid"/>。</param>
        /// <param name="createdTime">给定的创建时间。</param>
        /// <returns>返回字符串。</returns>
        protected override string PublishedAsQueryValue(Guid id, DateTimeOffset createdTime)
            => id.AsShortString(createdTime);
    }
}
