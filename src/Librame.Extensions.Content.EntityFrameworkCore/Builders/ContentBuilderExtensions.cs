#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using Librame.Extensions.Content.Mappers;
using Librame.Extensions.Content.Builders;
using Librame.Extensions.Core.Builders;
using Librame.Extensions.Core.Options;
using Librame.Extensions.Data.Builders;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 内容构建器静态扩展。
    /// </summary>
    public static class ContentBuilderExtensions
    {
        /// <summary>
        /// 添加内容扩展。
        /// </summary>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建内容构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IContentBuilder"/>。</returns>
        public static IContentBuilder AddContent(this IExtensionBuilder parentBuilder,
            Action<ContentBuilderDependency> configureDependency = null,
            Func<IExtensionBuilder, ContentBuilderDependency, IContentBuilder> builderFactory = null)
            => parentBuilder.AddContent<ContentBuilderDependency>(configureDependency, builderFactory);

        /// <summary>
        /// 添加内容扩展。
        /// </summary>
        /// <typeparam name="TDependency">指定的依赖类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建内容构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IContentBuilder"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递")]
        public static IContentBuilder AddContent<TDependency>(this IExtensionBuilder parentBuilder,
            Action<TDependency> configureDependency = null,
            Func<IExtensionBuilder, TDependency, IContentBuilder> builderFactory = null)
            where TDependency : ContentBuilderDependency
        {
            if (!parentBuilder.TryGetBuilder<IDataBuilder>(out var dataBuilder))
                throw new NotSupportedException($"You need to register to builder.{nameof(DataBuilderExtensions.AddData)}().");

            // Clear Options Cache
            ConsistencyOptionsCache.TryRemove<ContentBuilderOptions>();

            // Add Builder Dependency
            var dependency = parentBuilder.AddBuilderDependency(out var dependencyType, configureDependency);
            parentBuilder.Services.TryAddReferenceBuilderDependency<ContentBuilderDependency>(dependency, dependencyType);

            // Create Builder
            var contentBuilder = builderFactory.NotNullOrDefault(()
                => (b, d) => new ContentBuilder(b, d)).Invoke(parentBuilder, dependency);

            var parameterMapper = ContentAccessorTypeParameterMappingHelper
                .ParseMapper(dataBuilder.AccessorTypeParameterMapper);

            contentBuilder.SetProperty(p => p.AccessorTypeParameterMapper, parameterMapper);

            return contentBuilder;
        }

    }
}
