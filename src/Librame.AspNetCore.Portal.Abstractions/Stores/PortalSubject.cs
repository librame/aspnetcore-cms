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
    /// 门户专题。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
    [Description("门户专题")]
    public class PortalSubject<TGenId, TCategoryId> : AbstractEntityCreation<TGenId>, IPublishing<DateTimeOffset>, IEquatable<PortalSubject<TGenId, TCategoryId>>
        where TGenId : IEquatable<TGenId>
    {
        /// <summary>
        /// 分类标识。
        /// </summary>
        [Display(Name = nameof(CategoryId), ResourceType = typeof(PortalSubjectResource))]
        public virtual TCategoryId CategoryId { get; set; }

        /// <summary>
        /// 发布时间。
        /// </summary>
        [Display(Name = nameof(PublishTime), ResourceType = typeof(PortalSubjectResource))]
        public virtual DateTimeOffset PublishTime { get; set; }

        /// <summary>
        /// 发布链接。
        /// </summary>
        [Display(Name = nameof(PublishLink), ResourceType = typeof(PortalSubjectResource))]
        public virtual string PublishLink { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        [Display(Name = nameof(Title), ResourceType = typeof(PortalSubjectResource))]
        public virtual string Title { get; set; }

        /// <summary>
        /// 副标题。
        /// </summary>
        [Display(Name = nameof(Subtitle), ResourceType = typeof(PortalSubjectResource))]
        public virtual string Subtitle { get; set; }


        /// <summary>
        /// 唯一索引是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="PortalSubject{TGenId, TCategoryId}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(PortalSubject<TGenId, TCategoryId> other)
            => CategoryId.Equals(other.NotNull(nameof(other)).CategoryId) && Title == other?.Title;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is PortalSubject<TGenId, TCategoryId> other) ? Equals(other) : false;


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
            => $"{nameof(CategoryId)}={CategoryId},{nameof(Title)}={Title}";


        /// <summary>
        /// 获取唯一索引表达式。
        /// </summary>
        /// <typeparam name="TSubject">指定的专题类型。</typeparam>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="title">给定的标题。</param>
        /// <returns>返回查询表达式。</returns>
        public static Expression<Func<TSubject, bool>> GetUniqueIndexExpression<TSubject>(TCategoryId categoryId, string title)
            where TSubject : PortalSubject<TGenId, TCategoryId>
        {
            title.NotEmpty(nameof(title));

            return p => p.CategoryId.Equals(categoryId) && p.Title == title;
        }
    }
}
