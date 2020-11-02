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
    /// 门户内置用户。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    [Description("门户内置用户")]
    public class PortalInternalUser<TGenId, TCreatedBy> : AbstractEntityCreation<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 名称。
        /// </summary>
        [Display(Name = nameof(Name), ResourceType = typeof(AbstractPortalResource))]
        public virtual string Name { get; set; }

        /// <summary>
        /// 密码哈希。
        /// </summary>
        public virtual string PasswordHash { get; set; }
    }
}
