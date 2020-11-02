#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Librame.Extensions.Content.Stores
{
    using Content.Resources;
    using Data;
    using Data.Stores;

    /// <summary>
    /// 单元。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的类别标识类型。</typeparam>
    /// <typeparam name="TPaneId">指定的窗格标识类型。</typeparam>
    /// <typeparam name="TSourceId">指定的来源标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    [Description("单元")]
    [Shardable]
    public class ContentUnit<TGenId, TCategoryId, TPaneId, TSourceId, TPublishedBy>
        : AbstractEntityPublication<TGenId, TPublishedBy>
        where TGenId : IEquatable<TGenId>
        where TCategoryId : IEquatable<TCategoryId>
        where TPaneId : IEquatable<TPaneId>
        where TSourceId : IEquatable<TSourceId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
        /// <summary>
        /// 类别标识。
        /// </summary>
        [Display(Name = nameof(CategoryId), ResourceType = typeof(AbstractContentResource))]
        public virtual TCategoryId CategoryId { get; set; }

        /// <summary>
        /// 窗格标识。
        /// </summary>
        [Display(Name = nameof(PaneId), ResourceType = typeof(AbstractContentResource))]
        public virtual TPaneId PaneId { get; set; }

        /// <summary>
        /// 来源标识。
        /// </summary>
        [Display(Name = nameof(CategoryId), ResourceType = typeof(AbstractContentResource))]
        public virtual TSourceId SourceId { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        [Display(Name = nameof(Title), ResourceType = typeof(AbstractContentResource))]
        public virtual string Title { get; set; }

        /// <summary>
        /// 副标题。
        /// </summary>
        [Display(Name = nameof(Subtitle), ResourceType = typeof(AbstractContentResource))]
        public virtual string Subtitle { get; set; }

        /// <summary>
        /// 引用源。
        /// </summary>
        [Display(Name = nameof(Reference), ResourceType = typeof(AbstractContentResource))]
        public virtual string Reference { get; set; }


        /// <summary>
        /// 除主键外的唯一索引相等比较（参见实体映射的唯一索引配置）。
        /// </summary>
        /// <param name="other">给定的 <see cref="ContentUnit{TGenId, TCategoryId, TPaneId, TSourceId, TPublishedBy}"/>。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public virtual bool Equals(ContentUnit<TGenId, TCategoryId, TPaneId, TSourceId, TPublishedBy> other)
            => other.IsNotNull() && CategoryId.Equals(other.CategoryId) && Title == other.Title;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is ContentUnit<TGenId, TCategoryId, TPaneId, TSourceId, TPublishedBy> other) && Equals(other);


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回 32 位整数。</returns>
        public override int GetHashCode()
            => CategoryId.GetHashCode() ^ Title.CompatibleGetHashCode();


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => $"{base.ToString()};{nameof(CategoryId)}={CategoryId};{nameof(Title)}={Title}";
    }
}
