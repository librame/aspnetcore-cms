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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Librame.AspNetCore.Portal
{
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// 门户来源。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
    [Description("门户来源")]
    public class PortalSource<TIncremId, TCategoryId> : AbstractEntityCreation<TIncremId>, IEquatable<PortalSource<TIncremId, TCategoryId>>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 构造一个门户来源。
        /// </summary>
        public PortalSource()
        {
        }

        /// <summary>
        /// 构造一个门户来源。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="link">给定的链接。</param>
        public PortalSource(string name, string link)
        {
            Name = name;
            Link = link;
        }


        /// <summary>
        /// 分类标识。
        /// </summary>
        [Display(Name = nameof(CategoryId), ResourceType = typeof(PortalSourceResource))]
        public virtual TCategoryId CategoryId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        [Display(Name = nameof(Name), ResourceType = typeof(PortalSourceResource))]
        public virtual string Name { get; set; }

        /// <summary>
        /// 标志。
        /// </summary>
        [Display(Name = nameof(Logo), ResourceType = typeof(PortalSourceResource))]
        public virtual string Logo { get; set; }

        /// <summary>
        /// 链接。
        /// </summary>
        [Display(Name = nameof(Link), ResourceType = typeof(PortalSourceResource))]
        public virtual string Link { get; set; }

        /// <summary>
        /// 备注。
        /// </summary>
        [Display(Name = nameof(Descr), ResourceType = typeof(PortalSourceResource))]
        public virtual string Descr { get; set; }


        /// <summary>
        /// 唯一索引是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="PortalSource{TIncremId, TCategoryId}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(PortalSource<TIncremId, TCategoryId> other)
            => CategoryId.Equals(other.NotNull(nameof(other)).CategoryId) && Name == other?.Name;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is PortalSource<TIncremId, TCategoryId> other) ? Equals(other) : false;


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回 32 位整数。</returns>
        public override int GetHashCode()
            => ToString().GetHashCode(StringComparison.OrdinalIgnoreCase);


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => $"{nameof(CategoryId)}={CategoryId},{nameof(Name)}={Name}";


        /// <summary>
        /// 获取唯一索引表达式。
        /// </summary>
        /// <typeparam name="TSource">指定的来源类型。</typeparam>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        public static Expression<Func<TSource, bool>> GetUniqueIndexExpression<TSource>(TCategoryId categoryId, string name)
            where TSource : PortalSource<TIncremId, TCategoryId>
        {
            name.NotEmpty(nameof(name));

            return p => p.CategoryId.Equals(categoryId) && p.Name == name;
        }
    }
}
