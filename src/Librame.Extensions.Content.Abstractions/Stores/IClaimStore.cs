#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Linq;

namespace Librame.Extensions.Content.Stores
{
    using Data.Stores;

    /// <summary>
    /// 声明存储接口。
    /// </summary>
    /// <typeparam name="TClaim">指定的内容声明类型。</typeparam>
    public interface IClaimStore<TClaim> : IStore
        where TClaim : class
    {
        /// <summary>
        /// 声明查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TClaim}"/>。</value>
        IQueryable<TClaim> Claims { get; }
    }
}
