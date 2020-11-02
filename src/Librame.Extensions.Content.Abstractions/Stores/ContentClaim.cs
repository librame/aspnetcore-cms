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
    using Data.Stores;

    /// <summary>
    /// 声明。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的类别标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    [Description("声明")]
    public class ContentClaim<TIncremId, TCategoryId, TCreatedBy> : AbstractEntityCreation<TIncremId, TCreatedBy>
        where TIncremId : IEquatable<TIncremId>
        where TCategoryId : IEquatable<TCategoryId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 类别标识。
        /// </summary>
        [Display(Name = nameof(CategoryId), ResourceType = typeof(AbstractContentResource))]
        public virtual TCategoryId CategoryId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        [Display(Name = nameof(Name), ResourceType = typeof(AbstractContentResource))]
        public virtual string Name { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        [Display(Name = nameof(Description), ResourceType = typeof(AbstractContentResource))]
        public virtual string Description { get; set; }


        /// <summary>
        /// 除主键外的唯一索引相等比较（参见实体映射的唯一索引配置）。
        /// </summary>
        /// <param name="other">给定的 <see cref="ContentClaim{TIncremId, TCategoryId, TCreatedBy}"/>。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public virtual bool Equals(ContentClaim<TIncremId, TCategoryId, TCreatedBy> other)
            => other.IsNotNull() && CategoryId.Equals(other.CategoryId) && Name == other.Name;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is ContentClaim<TIncremId, TCategoryId, TCreatedBy> other) && Equals(other);


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回 32 位整数。</returns>
        public override int GetHashCode()
            => CategoryId.GetHashCode() ^ Name.CompatibleGetHashCode();


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => $"{base.ToString()};{nameof(CategoryId)}={CategoryId};{nameof(Name)}={Name}";
    }
}
