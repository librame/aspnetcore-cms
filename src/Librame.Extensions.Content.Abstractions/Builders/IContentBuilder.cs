#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Content.Builders
{
    using Content.Mappers;
    using Core.Builders;

    /// <summary>
    /// 内容构建器接口。
    /// </summary>
    public interface IContentBuilder : IExtensionBuilder
    {
        /// <summary>
        /// 内容访问器类型参数映射器。
        /// </summary>
        /// <value>返回 <see cref="ContentAccessorTypeParameterMapper"/>。</value>
        ContentAccessorTypeParameterMapper AccessorTypeParameterMapper { get; }
    }
}
