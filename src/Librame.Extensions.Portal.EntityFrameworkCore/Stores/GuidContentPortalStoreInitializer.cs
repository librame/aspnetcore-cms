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

namespace Librame.Extensions.Portal.Stores
{
    using Content.Options;
    using Data.Stores;
    using Portal.Accessors;
    using Portal.Builders;
    using Portal.Services;

    /// <summary>
    /// <see cref="Guid"/> 内容门户存储初始化器。
    /// </summary>
    public class GuidContentPortalStoreInitializer : GuidContentPortalStoreInitializer<ContentPortalDbContextAccessor>
    {
        /// <summary>
        /// 构造一个 <see cref="GuidContentPortalStoreInitializer"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{PortalBuilderOptions}"/>。</param>
        /// <param name="passwordHashService">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="initializationOptions">给定的 <see cref="ContentStoreInitializationOptions"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidContentPortalStoreInitializer(IOptions<PortalBuilderOptions> options,
            IPasswordHashService<PortalInternalUser<Guid, Guid>> passwordHashService,
            ContentStoreInitializationOptions initializationOptions,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(options, passwordHashService, initializationOptions,
                  identifierGenerator, validator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// <see cref="Guid"/> 内容门户存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class GuidContentPortalStoreInitializer<TAccessor> : AbstractContentPortalStoreInitializer<TAccessor, Guid, int, Guid, Guid>
        where TAccessor : ContentPortalDbContextAccessor
    {
        /// <summary>
        /// 构造一个 <see cref="GuidContentPortalStoreInitializer{TAccessor}"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{PortalBuilderOptions}"/>。</param>
        /// <param name="passwordHashService">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="initializationOptions">给定的 <see cref="ContentStoreInitializationOptions"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidContentPortalStoreInitializer(IOptions<PortalBuilderOptions> options,
            IPasswordHashService<PortalInternalUser<Guid, Guid>> passwordHashService,
            ContentStoreInitializationOptions initializationOptions,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(options?.Value.Stores.Initialization, passwordHashService, initializationOptions,
                  identifierGenerator, validator, loggerFactory)
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

        /// <summary>
        /// 获取用户标识。
        /// </summary>
        /// <param name="internalUserId">给定的内置用户标识。</param>
        /// <returns>返回 <see cref="Guid"/>。</returns>
        protected override Guid GetUserId(Guid internalUserId)
            => internalUserId;

    }
}
