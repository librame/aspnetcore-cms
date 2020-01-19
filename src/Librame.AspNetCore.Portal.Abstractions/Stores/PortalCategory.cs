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

namespace Librame.AspNetCore.Portal
{
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// 门户分类。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    [Description("门户分类")]
    public class PortalCategory<TIncremId> : AbstractEntityCreation<TIncremId>, IParentId<TIncremId>, IEquatable<PortalCategory<TIncremId>>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 构造一个门户分类实例。
        /// </summary>
        public PortalCategory()
        {
        }

        /// <summary>
        /// 构造一个门户分类实例。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="descr">给定的说明。</param>
        public PortalCategory(string name, string descr)
        {
            Name = name;
            Descr = descr;
        }


        /// <summary>
        /// 父标识。
        /// </summary>
        [Display(Name = nameof(ParentId), ResourceType = typeof(PortalCategoryResource))]
        public virtual TIncremId ParentId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        [Display(Name = nameof(Name), ResourceType = typeof(PortalCategoryResource))]
        public virtual string Name { get; set; }

        /// <summary>
        /// 说明。
        /// </summary>
        [Display(Name = nameof(Descr), ResourceType = typeof(PortalCategoryResource))]
        public virtual string Descr { get; set; }


        /// <summary>
        /// 唯一索引是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="PortalCategory{TIncremId}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(PortalCategory<TIncremId> other)
            => ParentId.Equals(other.NotNull(nameof(other)).ParentId) && Name == other?.Name;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is PortalCategory<TIncremId> other) ? Equals(other) : false;


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
            => $"{nameof(ParentId)}={ParentId},{nameof(Name)}={Name}";
    }
}
