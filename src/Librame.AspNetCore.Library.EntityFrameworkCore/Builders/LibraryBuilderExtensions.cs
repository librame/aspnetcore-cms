﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

namespace Librame.AspNetCore.Library
{
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 文库构建器静态扩展。
    /// </summary>
    public static class LibraryBuilderExtensions
    {
        /// <summary>
        /// 添加文库扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{LibraryBuilderOptions}"/>（可选；高优先级）。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选；次优先级）。</param>
        /// <param name="configureBinderOptions">给定的配置绑定器选项动作（可选）。</param>
        /// <returns>返回 <see cref="ILibraryBuilder"/>。</returns>
        public static ILibraryBuilder AddLibrary<TAccessor>(this IBuilder builder,
            Action<LibraryBuilderOptions> configureOptions = null,
            IConfiguration configuration = null,
            Action<BinderOptions> configureBinderOptions = null)
            where TAccessor : DbContext, IAccessor
        {
            return builder.AddLibrary<DefaultLibraryUser, DefaultLibraryRole,
                TAccessor>(configureOptions, configuration, configureBinderOptions);
        }

        /// <summary>
        /// 添加文库扩展。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{LibraryBuilderOptions}"/>（可选；高优先级）。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选；次优先级）。</param>
        /// <param name="configureBinderOptions">给定的配置绑定器选项动作（可选）。</param>
        /// <returns>返回 <see cref="ILibraryBuilder"/>。</returns>
        public static ILibraryBuilder AddLibrary<TUser, TRole, TAccessor>(this IBuilder builder,
            Action<LibraryBuilderOptions> configureOptions = null,
            IConfiguration configuration = null,
            Action<BinderOptions> configureBinderOptions = null)
            where TUser : class
            where TRole : class
            where TAccessor : DbContext, IAccessor
        {
            var options = builder.Configure(configureOptions,
                configuration, configureBinderOptions);

            var LibraryBuilder = new InternalLibraryBuilder(builder, options);

            return LibraryBuilder;
        }

    }
}
