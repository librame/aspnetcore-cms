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

namespace Librame.Extensions.Content.Stores
{
    using Content.Resources;
    using Data.Stores;

    /// <summary>
    /// 内容窗格单元。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TPaneId">指定的窗格标识类型。</typeparam>
    /// <typeparam name="TUnitId">指定的单元标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    [Description("内容窗格单元")]
    public class ContentPaneUnit<TIncremId, TPaneId, TUnitId, TCreatedBy> : AbstractIdentifierEntityCreation<TIncremId, TCreatedBy>
        where TIncremId : IEquatable<TIncremId>
        where TPaneId : IEquatable<TPaneId>
        where TUnitId : IEquatable<TUnitId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 窗格标识。
        /// </summary>
        [Display(Name = nameof(PaneId), ResourceType = typeof(AbstractContentResource))]
        public virtual TPaneId PaneId { get; set; }

        /// <summary>
        /// 单元标识。
        /// </summary>
        [Display(Name = nameof(TUnitId), ResourceType = typeof(AbstractContentResource))]
        public virtual TUnitId UnitId { get; set; }


        /// <summary>
        /// 除主键外的唯一索引相等比较（参见实体映射的唯一索引配置）。
        /// </summary>
        /// <param name="other">给定的 <see cref="ContentPaneUnit{TIncremId, TPaneId, TUnitId, TCreatedBy}"/>。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public virtual bool Equals(ContentPaneUnit<TIncremId, TPaneId, TUnitId, TCreatedBy> other)
            => other.IsNotNull() && PaneId.Equals(other.PaneId) && UnitId.Equals(other.UnitId);

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is ContentPaneUnit<TIncremId, TPaneId, TUnitId, TCreatedBy> other) && Equals(other);


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回 32 位整数。</returns>
        public override int GetHashCode()
            => PaneId.GetHashCode() ^ UnitId.GetHashCode();


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => $"{base.ToString()};{nameof(PaneId)}={PaneId};{nameof(UnitId)}={UnitId}";
    }
}
