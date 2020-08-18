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

namespace Librame.Extensions.Portal.Stores
{
    using Data.Stores;

    /// <summary>
    /// 编者存储接口。
    /// </summary>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    public interface IEditorStore<TEditor> : IStore
        where TEditor : class
    {
        /// <summary>
        /// 编者查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TEditor}"/>。</value>
        IQueryable<TEditor> Editors { get; }
    }
}
