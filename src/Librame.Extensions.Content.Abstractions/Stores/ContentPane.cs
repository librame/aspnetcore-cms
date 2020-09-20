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
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Content.Stores
{
    using Content.Resources;
    using Core.Identifiers;
    using Data.Resources;
    using Data.Stores;

    /// <summary>
    /// 窗格。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    [Description("窗格")]
    public class ContentPane<TIncremId, TCreatedBy> : AbstractIdentifierEntityCreation<TIncremId, TCreatedBy>,
        IParentIdentifier<TIncremId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 父标识。
        /// </summary>
        [Display(Name = nameof(ParentId), GroupName = "GlobalGroup", ResourceType = typeof(AbstractEntityResource))]
        public virtual TIncremId ParentId { get; set; }

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
        /// 图标。
        /// </summary>
        [Display(Name = nameof(Icon), ResourceType = typeof(AbstractContentResource))]
        public virtual string Icon { get; set; }

        /// <summary>
        /// 更多。
        /// </summary>
        [Display(Name = nameof(More), ResourceType = typeof(AbstractContentResource))]
        public virtual string More { get; set; }


        /// <summary>
        /// 获取对象标识。
        /// </summary>
        /// <returns>返回标识（兼容各种引用与值类型标识）。</returns>
        public virtual object GetObjectParentId()
            => ParentId;

        /// <summary>
        /// 异步获取父对象标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含对象父标识（兼容各种引用与值类型标识）的异步操作。</returns>
        public virtual ValueTask<object> GetObjectParentIdAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunOrCancelValueAsync(() => (object)ParentId);


        /// <summary>
        /// 设置对象标识。
        /// </summary>
        /// <param name="newParentId">给定的新对象标识。</param>
        /// <returns>返回标识（兼容各种引用与值类型标识）。</returns>
        public virtual object SetObjectParentId(object newParentId)
        {
            ParentId = newParentId.CastTo<object, TIncremId>(nameof(newParentId));
            return newParentId;
        }

        /// <summary>
        /// 异步设置父对象标识。
        /// </summary>
        /// <param name="newParentId">给定的父标识对象。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含对象父标识（兼容各种引用与值类型标识）的异步操作。</returns>
        public virtual ValueTask<object> SetObjectParentIdAsync(object newParentId,
            CancellationToken cancellationToken = default)
        {
            var realNewParentId = newParentId.CastTo<object, TIncremId>(nameof(newParentId));

            return cancellationToken.RunOrCancelValueAsync(() =>
            {
                ParentId = realNewParentId;
                return newParentId;
            });
        }


        /// <summary>
        /// 除主键外的唯一索引相等比较（参见实体映射的唯一索引配置）。
        /// </summary>
        /// <param name="other">给定的 <see cref="ContentPane{TIncremId, TCreatedBy}"/>。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public virtual bool Equals(ContentPane<TIncremId, TCreatedBy> other)
            => other.IsNotNull() && ParentId.Equals(other.ParentId) && Name == other.Name;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is ContentPane<TIncremId, TCreatedBy> other) && Equals(other);


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回 32 位整数。</returns>
        public override int GetHashCode()
            => ParentId.GetHashCode() ^ Name.CompatibleGetHashCode();


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => $"{base.ToString()};{nameof(ParentId)}={ParentId};{nameof(Name)}={Name}";
    }
}
