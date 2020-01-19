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
    /// 门户标签。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    [Description("门户标签")]
    public class PortalTag<TGenId> : AbstractEntityCreation<TGenId>, IEquatable<PortalTag<TGenId>>
        where TGenId : IEquatable<TGenId>
    {
        /// <summary>
        /// 名称。
        /// </summary>
        [Display(Name = nameof(Name), ResourceType = typeof(PortalTagResource))]
        public virtual string Name { get; set; }

        /// <summary>
        /// 引用计数。
        /// </summary>
        [Display(Name = nameof(RefersCount), ResourceType = typeof(PortalTagResource))]
        public virtual int RefersCount { get; set; }


        /// <summary>
        /// 唯一索引是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="PortalTag{TGenId}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(PortalTag<TGenId> other)
            => Name == other?.Name;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is PortalTag<TGenId> other) ? Equals(other) : false;


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
            => Name;


        /// <summary>
        /// 获取唯一索引表达式。
        /// </summary>
        /// <typeparam name="TTag">指定的标签类型。</typeparam>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        public static Expression<Func<TTag, bool>> GetUniqueIndexExpression<TTag>(string name)
            where TTag : PortalTag<TGenId>
        {
            name.NotEmpty(nameof(name));

            return p => p.Name == name;
        }
    }
}
