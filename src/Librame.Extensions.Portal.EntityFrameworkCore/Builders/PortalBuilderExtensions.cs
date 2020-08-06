#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using Librame.Extensions.Core.Builders;
using Librame.Extensions.Core.Options;
using Librame.Extensions.Portal.Builders;
using Librame.Extensions.Portal.Stores;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 门户构建器静态扩展。
    /// </summary>
    public static class PortalBuilderExtensions
    {
        /// <summary>
        /// 添加内置用户门户扩展。
        /// </summary>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建门户构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IPortalBuilder"/>。</returns>
        public static IPortalBuilder AddInternalUserPortal(this IExtensionBuilder parentBuilder,
            Action<PortalBuilderDependency> configureDependency = null,
            Func<Type, IExtensionBuilder, PortalBuilderDependency, IPortalBuilder> builderFactory = null)
            => parentBuilder.AddPortal<PortalInternalUser<Guid, Guid>>(configureDependency, builderFactory);


        /// <summary>
        /// 添加门户扩展。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建门户构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IPortalBuilder"/>。</returns>
        public static IPortalBuilder AddPortal<TUser>(this IExtensionBuilder parentBuilder,
            Action<PortalBuilderDependency> configureDependency = null,
            Func<Type, IExtensionBuilder, PortalBuilderDependency, IPortalBuilder> builderFactory = null)
            where TUser : class
            => parentBuilder.AddPortal<PortalBuilderDependency, TUser>(configureDependency, builderFactory);

        /// <summary>
        /// 添加门户扩展。
        /// </summary>
        /// <typeparam name="TDependency">指定的依赖类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建门户构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IPortalBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IPortalBuilder AddPortal<TDependency, TUser>(this IExtensionBuilder parentBuilder,
            Action<TDependency> configureDependency = null,
            Func<Type, IExtensionBuilder, TDependency, IPortalBuilder> builderFactory = null)
            where TDependency : PortalBuilderDependency
            where TUser : class
        {
            // Clear Options Cache
            ConsistencyOptionsCache.TryRemove<PortalBuilderOptions>();

            // Add Builder Dependency
            var dependency = parentBuilder.AddBuilderDependency(out var dependencyType, configureDependency);
            parentBuilder.Services.TryAddReferenceBuilderDependency<PortalBuilderDependency>(dependency, dependencyType);

            // Create Builder
            return builderFactory.NotNullOrDefault(()
                => (u, b, d) => new PortalBuilder(u, b, d)).Invoke(typeof(TUser), parentBuilder, dependency);
        }

    }
}
