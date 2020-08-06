#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Portal.Builders
{
    using Core.Builders;

    /// <summary>
    /// 门户构建器依赖选项。
    /// </summary>
    public class PortalBuilderDependency : AbstractExtensionBuilderDependency<PortalBuilderOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="PortalBuilderDependency"/>。
        /// </summary>
        /// <param name="parentDependency">给定的父级 <see cref="IExtensionBuilderDependency"/>（可选）。</param>
        public PortalBuilderDependency(IExtensionBuilderDependency parentDependency = null)
            : this(nameof(PortalBuilderDependency), parentDependency)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="PortalBuilderDependency"/>。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="parentDependency">给定的父级 <see cref="IExtensionBuilderDependency"/>（可选）。</param>
        protected PortalBuilderDependency(string name, IExtensionBuilderDependency parentDependency = null)
            : base(name, parentDependency)
        {
        }

    }
}
