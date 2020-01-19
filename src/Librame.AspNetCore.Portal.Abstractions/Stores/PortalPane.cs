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
    /// 门户窗格。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
    [Description("门户窗格")]
    public class PortalPane<TIncremId, TCategoryId> : AbstractEntityCreation<TIncremId>, IEquatable<PortalPane<TIncremId, TCategoryId>>
        where TIncremId : IEquatable<TIncremId>
        where TCategoryId : IEquatable<TCategoryId>
    {
        /// <summary>
        /// 构造一个门户窗格实例。
        /// </summary>
        public PortalPane()
        {
        }

        /// <summary>
        /// 构造一个门户窗格实例。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="path">给定的路径。</param>
        public PortalPane(string name, string path)
        {
            Name = name;
            Path = path;
        }


        /// <summary>
        /// 分类标识。
        /// </summary>
        [Display(Name = nameof(CategoryId), ResourceType = typeof(PortalPaneResource))]
        public virtual TCategoryId CategoryId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        [Display(Name = nameof(Name), ResourceType = typeof(PortalPaneResource))]
        public virtual string Name { get; set; }

        /// <summary>
        /// 路径。
        /// </summary>
        [Display(Name = nameof(Path), ResourceType = typeof(PortalPaneResource))]
        public virtual string Path { get; set; }


        /// <summary>
        /// 唯一索引是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="PortalPane{TIncremId, TCategoryId}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(PortalPane<TIncremId, TCategoryId> other)
            => CategoryId.Equals(other.NotNull(nameof(other)).CategoryId) && Name == other?.Name;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is PortalPane<TIncremId, TCategoryId> other) ? Equals(other) : false;


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
        /// <typeparam name="TPane">指定的窗格类型。</typeparam>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        public static Expression<Func<TPane, bool>> GetUniqueIndexExpression<TPane>(TCategoryId categoryId, string name)
            where TPane : PortalPane<TIncremId, TCategoryId>
        {
            name.NotEmpty(nameof(name));

            return p => p.CategoryId.Equals(categoryId) && p.Name == name;
        }
    }
}
