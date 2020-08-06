#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Portal
{
    using Data;

    /// <summary>
    /// <see cref="TableDescriptor"/> 静态扩展。
    /// </summary>
    public static class PortalTableDescriptorExtensions
    {
        /// <summary>
        /// 插入门户标记前缀（如：Portal_）。
        /// </summary>
        /// <param name="table">给定的 <see cref="TableDescriptor"/>。</param>
        /// <returns>返回 <see cref="TableDescriptor"/>。</returns>
        public static TableDescriptor InsertPortalPrefix(this TableDescriptor table)
            => table.InsertPrefix(nameof(Portal), name => name.TrimStart(nameof(Portal)));
    }
}
