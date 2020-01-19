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
    /// 门户专题主体。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TSubjectId">指定的专题标识类型。</typeparam>
    [Description("门户专题主体")]
    public class PortalSubjectBody<TIncremId, TSubjectId> : AbstractEntity<TIncremId>, IEquatable<PortalSubjectBody<TIncremId, TSubjectId>>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 专题标识。
        /// </summary>
        [Display(Name = nameof(SubjectId), ResourceType = typeof(PortalSubjectBodyResource))]
        public virtual TSubjectId SubjectId { get; set; }

        /// <summary>
        /// 文本散列。
        /// </summary>
        [Display(Name = nameof(TextHash), ResourceType = typeof(PortalSubjectBodyResource))]
        public virtual string TextHash { get; set; }

        /// <summary>
        /// 文本。
        /// </summary>
        [Display(Name = nameof(Text), ResourceType = typeof(PortalSubjectBodyResource))]
        public virtual string Text { get; set; }


        /// <summary>
        /// 唯一索引是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="PortalSubjectBody{TIncremId, TSubjectId}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(PortalSubjectBody<TIncremId, TSubjectId> other)
            => SubjectId.Equals(other.NotNull(nameof(other)).SubjectId) && TextHash == other?.TextHash;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is PortalSubjectBody<TIncremId, TSubjectId> other) ? Equals(other) : false;


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
            => $"{nameof(SubjectId)}={SubjectId},{nameof(TextHash)}={TextHash}";


        /// <summary>
        /// 获取唯一索引表达式。
        /// </summary>
        /// <typeparam name="TSubjectBody">指定的专题主体类型。</typeparam>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="textHash">给定的文本哈希。</param>
        /// <returns>返回查询表达式。</returns>
        public static Expression<Func<TSubjectBody, bool>> GetUniqueIndexExpression<TSubjectBody>(TSubjectId subjectId, string textHash)
            where TSubjectBody : PortalSubjectBody<TIncremId, TSubjectId>
        {
            textHash.NotEmpty(nameof(textHash));

            return p => p.SubjectId.Equals(subjectId) && p.TextHash == textHash;
        }
    }
}
