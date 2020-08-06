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
    using Core.Builders;

    /// <summary>
    /// 内容构建器依赖选项。
    /// </summary>
    public class ContentBuilderDependency : AbstractExtensionBuilderDependency<ContentBuilderOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="ContentBuilderDependency"/>。
        /// </summary>
        /// <param name="parentDependency">给定的父级 <see cref="IExtensionBuilderDependency"/>（可选）。</param>
        public ContentBuilderDependency(IExtensionBuilderDependency parentDependency = null)
            : this(nameof(ContentBuilderDependency), parentDependency)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="ContentBuilderDependency"/>。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="parentDependency">给定的父级 <see cref="IExtensionBuilderDependency"/>（可选）。</param>
        protected ContentBuilderDependency(string name, IExtensionBuilderDependency parentDependency = null)
            : base(name, parentDependency)
        {
        }

    }
}
