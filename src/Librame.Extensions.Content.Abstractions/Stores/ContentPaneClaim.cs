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

namespace Librame.Extensions.Content.Stores
{
    using Content.Resources;
    using Data.Stores;

    /// <summary>
    /// 窗格声明。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TPaneId">指定的窗格标识类型。</typeparam>
    /// <typeparam name="TClaimId">指定的声明标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    [Description("窗格声明")]
    public class ContentPaneClaim<TIncremId, TPaneId, TClaimId, TCreatedBy> : AbstractIdentifierEntityCreation<TIncremId, TCreatedBy>
        where TIncremId : IEquatable<TIncremId>
        where TPaneId : IEquatable<TPaneId>
        where TClaimId : IEquatable<TClaimId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 窗格标识。
        /// </summary>
        [Display(Name = nameof(PaneId), ResourceType = typeof(AbstractContentResource))]
        public virtual TPaneId PaneId { get; set; }

        /// <summary>
        /// 声明标识。
        /// </summary>
        [Display(Name = nameof(ClaimId), ResourceType = typeof(AbstractContentResource))]
        public virtual TClaimId ClaimId { get; set; }

        /// <summary>
        /// 窗格声明值。
        /// </summary>
        [Display(Name = nameof(ClaimValue), ResourceType = typeof(AbstractContentResource))]
        public virtual string ClaimValue { get; set; }
    }
}
