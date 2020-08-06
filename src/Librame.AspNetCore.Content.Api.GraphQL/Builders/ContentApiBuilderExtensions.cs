#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Api;
using Librame.AspNetCore.Api.Builders;
using Librame.AspNetCore.Content.Api;
using Librame.Extensions.Content.Builders;
using Librame.Extensions.Core.Builders;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 内容 API 构建器静态扩展。
    /// </summary>
    public static class ContentApiBuilderExtensions
    {
        /// <summary>
        /// 添加 Content API 扩展。
        /// </summary>
        /// <param name="contentBuilder">给定的 <see cref="IContentBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public static IApiBuilder AddContentApi(this IContentBuilder contentBuilder,
            Action<ApiBuilderDependency> configureDependency = null,
            Func<IExtensionBuilder, ApiBuilderDependency, IApiBuilder> builderFactory = null)
        {
            var builder = contentBuilder.AddApi(configureDependency, builderFactory);

            var accessorMappingDescriptor = contentBuilder.AccessorTypeParameterMapper;

            var apiMutationType = typeof(ContentGraphApiMutation<,,>).MakeGenericType(
                contentBuilder.Source.UserType,
                accessorMappingDescriptor.GenId.ArgumentType,
                accessorMappingDescriptor.CreatedBy.ArgumentType);

            var apiQueryType = typeof(ContentGraphApiQuery<,>).MakeGenericType(
                contentBuilder.Source.RoleType,
                contentBuilder.Source.UserType);

            contentBuilder.Services.TryReplaceAll(typeof(IGraphApiMutation), apiMutationType);
            contentBuilder.Services.TryReplaceAll(typeof(IGraphApiQuery), apiQueryType);

            return builder;
        }

    }
}
