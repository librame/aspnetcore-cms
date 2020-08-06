#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.Extensions.Content.Builders
{
    using Content.Mappers;
    using Core.Builders;
    using Core.Services;

    /// <summary>
    /// 内容构建器。
    /// </summary>
    public class ContentBuilder : AbstractExtensionBuilder, IContentBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="ContentBuilder"/>。
        /// </summary>
        /// <param name="parentBuilder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependency">给定的 <see cref="ContentBuilderDependency"/>。</param>
        public ContentBuilder(IExtensionBuilder parentBuilder, ContentBuilderDependency dependency)
            : base(parentBuilder, dependency)
        {
            Services.AddSingleton<IContentBuilder>(this);

            //AddInternalServices();
        }


        /// <summary>
        /// 内容访问器类型参数映射器。
        /// </summary>
        /// <value>返回 <see cref="ContentAccessorTypeParameterMapper"/>。</value>
        public ContentAccessorTypeParameterMapper AccessorTypeParameterMapper { get; private set; }


        //private void AddInternalServices()
        //{
        //}


        /// <summary>
        /// 获取指定服务类型的特征。
        /// </summary>
        /// <param name="serviceType">给定的服务类型。</param>
        /// <returns>返回 <see cref="ServiceCharacteristics"/>。</returns>
        public override ServiceCharacteristics GetServiceCharacteristics(Type serviceType)
            => ContentBuilderServiceCharacteristicsRegistration.Register.GetOrDefault(serviceType);
    }
}
