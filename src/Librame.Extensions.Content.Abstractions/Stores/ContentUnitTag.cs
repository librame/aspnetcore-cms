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
using System.ComponentModel.DataAnnotations;

namespace Librame.Extensions.Content.Stores
{
    using Data.Stores;
    using Content.Resources;

    /// <summary>
    /// 内容单元标签。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUnitId">指定的单元标识类型。</typeparam>
    /// <typeparam name="TTagId">指定的标签标识类型。</typeparam>
    public class ContentUnitTag<TIncremId, TUnitId, TTagId> : AbstractIdentifier<TIncremId>
        where TIncremId : IEquatable<TIncremId>
        where TUnitId : IEquatable<TUnitId>
        where TTagId : IEquatable<TTagId>
    {
        /// <summary>
        /// 单元标识。
        /// </summary>
        [Display(Name = nameof(UnitId), ResourceType = typeof(AbstractContentResource))]
        public virtual TUnitId UnitId { get; set; }

        /// <summary>
        /// 标签标识。
        /// </summary>
        [Display(Name = nameof(TagId), ResourceType = typeof(AbstractContentResource))]
        public virtual TTagId TagId { get; set; }
    }
}
