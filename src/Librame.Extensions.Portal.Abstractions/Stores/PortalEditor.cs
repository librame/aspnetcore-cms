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

namespace Librame.Extensions.Portal.Stores
{
    using Data.Stores;
    using Portal.Resources;

    /// <summary>
    /// 门户编者。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    [Description("门户编者")]
    public class PortalEditor<TGenId, TUserId, TCreatedBy> : AbstractEntityCreation<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 用户标识。
        /// </summary>
        [Display(Name = nameof(UserId), ResourceType = typeof(AbstractPortalResource))]
        public virtual TUserId UserId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        [Display(Name = nameof(Name), ResourceType = typeof(AbstractPortalResource))]
        public virtual string Name { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        [Display(Name = nameof(Description), ResourceType = typeof(AbstractPortalResource))]
        public virtual string Description { get; set; }

        /// <summary>
        /// 肖像。
        /// </summary>
        [Display(Name = nameof(Name), ResourceType = typeof(AbstractPortalResource))]
        public virtual string Portrait { get; set; }
    }
}
