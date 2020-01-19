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
    /// 门户标签引用。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TTagId">指定的标签标识类型。</typeparam>
    [Description("门户标签引用")]
    public class PortalTagReference<TIncremId, TTagId> : AbstractEntityCreation<TIncremId>, IEquatable<PortalTagReference<TIncremId, TTagId>>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 标签标识。
        /// </summary>
        [Display(Name = nameof(TagId), ResourceType = typeof(PortalTagReferenceResource))]
        public virtual TTagId TagId { get; set; }

        /// <summary>
        /// 引用实体标识（参考 <see cref="DataEntity"/>）。
        /// </summary>
        [Display(Name = nameof(ReferEntityId), ResourceType = typeof(PortalTagReferenceResource))]
        public virtual string ReferEntityId { get; set; }

        /// <summary>
        /// 引用标识。
        /// </summary>
        [Display(Name = nameof(ReferId), ResourceType = typeof(PortalTagReferenceResource))]
        public virtual string ReferId { get; set; }

        /// <summary>
        /// 引用 URL。
        /// </summary>
        [Display(Name = nameof(ReferUrl), ResourceType = typeof(PortalTagReferenceResource))]
        public virtual string ReferUrl { get; set; }


        /// <summary>
        /// 唯一索引是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="PortalTagReference{TIncremId, TTagId}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(PortalTagReference<TIncremId, TTagId> other)
            => TagId.Equals(other.NotNull(nameof(other)).TagId) && ReferEntityId == other?.ReferEntityId && ReferId == other.ReferId;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is PortalTagReference<TIncremId, TTagId> other) ? Equals(other) : false;


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
            => $"{nameof(TagId)}={TagId},{nameof(ReferEntityId)}={ReferEntityId},{nameof(ReferId)}={ReferId}";


        /// <summary>
        /// 获取唯一索引表达式。
        /// </summary>
        /// <typeparam name="TTagReference">指定的标签引用类型。</typeparam>
        /// <param name="tagId">给定的标签标识。</param>
        /// <param name="referEntityId">给定的引用实体标识。</param>
        /// <param name="referId">给定的引用标识。</param>
        /// <returns>返回查询表达式。</returns>
        public static Expression<Func<TTagReference, bool>> GetUniqueIndexExpression<TTagReference>(TTagId tagId, string referEntityId, string referId)
            where TTagReference : PortalTagReference<TIncremId, TTagId>
        {
            referEntityId.NotEmpty(nameof(referEntityId));
            referId.NotEmpty(nameof(referId));

            return p => p.TagId.Equals(tagId) && p.ReferEntityId == referEntityId && p.ReferId == referId;
        }
    }
}
