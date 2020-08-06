#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.Extensions.Portal.Builders
{
    using Core.Builders;

    /// <summary>
    /// 门户构建器接口。
    /// </summary>
    public interface IPortalBuilder : IExtensionBuilder
    {
        /// <summary>
        /// 用户类型。
        /// </summary>
        Type UserType { get; }


        /// <summary>
        /// 添加密码哈希服务。
        /// </summary>
        /// <param name="implementationTypeDefinition">给定的实现类型定义。</param>
        /// <returns>返回 <see cref="IPortalBuilder"/>。</returns>
        IPortalBuilder AddPasswordHashService(Type implementationTypeDefinition);
    }
}
