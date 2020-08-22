#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;
using System;

namespace Librame.AspNetCore.Content.Api.Types
{
    using AspNetCore.Api.Types;
    using Extensions.Content.Stores;

    /// <summary>
    /// 窗格类型。
    /// </summary>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class PaneType<TPane, TIncremId, TCreatedBy> : ApiTypeBase<TPane>
        where TPane : ContentPane<TIncremId, TCreatedBy>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个窗格类型。
        /// </summary>
        public PaneType()
            : base()
        {
            Field(f => f.Id, type: typeof(IdGraphType));
            Field(f => f.ParentId, type: typeof(IdGraphType));
            Field(f => f.Name);
            Field(f => f.Description);
            Field(f => f.Icon);
            Field(f => f.More);
            Field(f => f.CreatedTime);
            Field(f => f.CreatedBy, type: typeof(IdGraphType));
        }

    }
}
