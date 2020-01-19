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
    /// 门户编者。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    [Description("门户编者")]
    public class PortalEditor<TGenId, TUserId> : AbstractEntityCreation<TGenId>, IEquatable<PortalEditor<TGenId, TUserId>>
        where TGenId : IEquatable<TGenId>
        where TUserId : IEquatable<TUserId>
    {
        /// <summary>
        /// 用户标识。
        /// </summary>
        [Display(Name = nameof(UserId), ResourceType = typeof(PortalEditorResource))]
        public virtual TUserId UserId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        [Display(Name = nameof(Name), ResourceType = typeof(PortalEditorResource))]
        public virtual string Name { get; set; }


        /// <summary>
        /// 唯一索引是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="PortalEditor{TGenId, TUserId}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(PortalEditor<TGenId, TUserId> other)
            => UserId.Equals(other.NotNull(nameof(other)).UserId) && Name == other?.Name;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is PortalEditor<TGenId, TUserId> other) ? Equals(other) : false;


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
            => $"{nameof(UserId)}={UserId},{nameof(Name)}={Name}";


        /// <summary>
        /// 获取唯一索引表达式。
        /// </summary>
        /// <typeparam name="TEditor">指定的编者类型。</typeparam>
        /// <param name="userId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        public static Expression<Func<TEditor, bool>> GetUniqueIndexExpression<TEditor>(TUserId userId, string name)
            where TEditor : PortalEditor<TGenId, TUserId>
        {
            name.NotEmpty(nameof(name));

            return p => p.UserId.Equals(userId) && p.Name == name;
        }
    }
}
