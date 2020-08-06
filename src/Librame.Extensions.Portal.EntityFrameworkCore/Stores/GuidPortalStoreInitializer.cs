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
    using Data.Stores;
    using Portal.Accessors;
    using Portal.Builders;
    using Portal.Services;

    /// <summary>
    /// <see cref="Guid"/> 门户存储初始化器。
    /// </summary>
    public class GuidPortalStoreInitializer : GuidPortalStoreInitializer<PortalDbContextAccessor>
    {
        /// <summary>
        /// 构造一个 <see cref="GuidPortalStoreInitializer{TAccessor}"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{PortalBuilderOptions}"/>。</param>
        /// <param name="passwordHashService">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidPortalStoreInitializer(IOptions<PortalBuilderOptions> options,
            IPasswordHashService<PortalInternalUser<Guid, Guid>> passwordHashService,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(options, passwordHashService, identifierGenerator, validator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// <see cref="Guid"/> 门户存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class GuidPortalStoreInitializer<TAccessor> : AbstractPortalStoreInitializer<TAccessor,
        Guid, int, Guid, Guid>
        where TAccessor : PortalDbContextAccessor
    {
        /// <summary>
        /// 构造一个 <see cref="GuidPortalStoreInitializer{TAccessor}"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{PortalBuilderOptions}"/>。</param>
        /// <param name="passwordHashService">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidPortalStoreInitializer(IOptions<PortalBuilderOptions> options,
            IPasswordHashService<PortalInternalUser<Guid, Guid>> passwordHashService,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(options?.Value.Stores.Initialization, passwordHashService,
                  identifierGenerator, validator, loggerFactory)
        {
        }


        /// <summary>
        /// 获取用户标识。
        /// </summary>
        /// <param name="internalUserId">给定的内置用户标识。</param>
        /// <returns>返回 <see cref="Guid"/>。</returns>
        protected override Guid GetUserId(Guid internalUserId)
            => internalUserId;

    }
}
