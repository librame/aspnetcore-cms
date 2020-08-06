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
    using Data.Stores;
    using Content.Resources;

    /// <summary>
    /// 内容单元声明。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUnitId">指定的内容单元标识类型。</typeparam>
    /// <typeparam name="TClaimId">指定的内容声明标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    [Description("内容单元声明")]
    public class ContentUnitClaim<TIncremId, TUnitId, TClaimId, TCreatedBy>
        : AbstractIdentifierEntityCreation<TIncremId, TCreatedBy>
        where TIncremId : IEquatable<TIncremId>
        where TUnitId : IEquatable<TUnitId>
        where TClaimId : IEquatable<TClaimId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 单元标识。
        /// </summary>
        [Display(Name = nameof(UnitId), ResourceType = typeof(AbstractContentResource))]
        public virtual TUnitId UnitId { get; set; }

        /// <summary>
        /// 声明标识。
        /// </summary>
        [Display(Name = nameof(ClaimId), ResourceType = typeof(AbstractContentResource))]
        public virtual TClaimId ClaimId { get; set; }

        /// <summary>
        /// 单元声明值。
        /// </summary>
        [Display(Name = nameof(ClaimValue), ResourceType = typeof(AbstractContentResource))]
        public virtual string ClaimValue { get; set; }
    }
}
