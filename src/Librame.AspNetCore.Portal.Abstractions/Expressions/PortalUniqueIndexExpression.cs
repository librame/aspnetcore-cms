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
using System.Linq.Expressions;

namespace Librame.AspNetCore.Portal
{
    using Extensions;

    /// <summary>
    /// 门户唯一索引表达式。
    /// </summary>
    public static class PortalUniqueIndexExpression
    {
        /// <summary>
        /// 获取分类唯一索引表达式。
        /// </summary>
        /// <typeparam name="TCategory">指定的分类类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <param name="parentId">给定的父标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        public static Expression<Func<TCategory, bool>> GetCategory<TCategory, TIncremId>(TIncremId parentId, string name)
            where TCategory : PortalCategory<TIncremId>
            where TIncremId : IEquatable<TIncremId>
        {
            name.NotEmpty(nameof(name));

            return p => p.ParentId.Equals(parentId) && p.Name == name;
        }

    }
}
