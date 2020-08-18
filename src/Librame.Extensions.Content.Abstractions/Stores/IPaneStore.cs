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
    /// 窗格存储接口。
    /// </summary>
    /// <typeparam name="TPane">指定的内容窗格类型。</typeparam>
    /// <typeparam name="TPaneUnit">指定的内容窗格单元类型。</typeparam>
    public interface IPaneStore<TPane, TPaneUnit> : IStore
        where TPane : class
        where TPaneUnit : class
    {
        /// <summary>
        /// 窗格查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TPane}"/>。</value>
        IQueryable<TPane> Panes { get; }

        /// <summary>
        /// 窗格单元查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TPaneUnit}"/>。</value>
        IQueryable<TPaneUnit> PaneUnits { get; }
    }
}
