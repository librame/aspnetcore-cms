#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Librame.Extensions.Portal.Builders
{
    using Core.Builders;
    using Core.Services;
    using Portal.Services;

    /// <summary>
    /// 门户构建器。
    /// </summary>
    public class PortalBuilder : AbstractExtensionBuilder, IPortalBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="PortalBuilder"/>。
        /// </summary>
        /// <param name="userType">指定的用户类型。</param>
        /// <param name="parentBuilder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependency">给定的 <see cref="PortalBuilderDependency"/>。</param>
        public PortalBuilder(Type userType, IExtensionBuilder parentBuilder, PortalBuilderDependency dependency)
            : base(parentBuilder, dependency)
        {
            UserType = userType.NotNull(nameof(userType));

            Services.AddSingleton<IPortalBuilder>(this);

            AddInternalServices();
        }


        /// <summary>
        /// 用户类型。
        /// </summary>
        public Type UserType { get; }


        private void AddInternalServices()
        {
            // Services
            AddPasswordHashService(typeof(PasswordHashService<>));
        }


        /// <summary>
        /// 获取指定服务类型的特征。
        /// </summary>
        /// <param name="serviceType">给定的服务类型。</param>
        /// <returns>返回 <see cref="ServiceCharacteristics"/>。</returns>
        public override ServiceCharacteristics GetServiceCharacteristics(Type serviceType)
            => PortalBuilderServiceCharacteristicsRegistration.Register.GetOrDefault(serviceType);


        /// <summary>
        /// 添加密码哈希服务。
        /// </summary>
        /// <param name="implementationTypeDefinition">给定的实现类型定义。</param>
        /// <returns>返回 <see cref="IPortalBuilder"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public IPortalBuilder AddPasswordHashService(Type implementationTypeDefinition)
        {
            implementationTypeDefinition.NotNull(nameof(implementationTypeDefinition));

            var implementationType = implementationTypeDefinition.MakeGenericType(UserType);

            var serviceTypeDefinition = typeof(IPasswordHashService<>);
            var characteristics = GetServiceCharacteristics(serviceTypeDefinition);
            var serviceType = serviceTypeDefinition.MakeGenericType(UserType);

            Services.TryRemoveAll(serviceType, throwIfNotFound: false);
            Services.AddByCharacteristics(serviceType, implementationType, characteristics);

            return this;
        }

    }
}
