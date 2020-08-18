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

namespace Librame.Extensions.Portal.Accessors
{
    using Content.Accessors;

    /// <summary>
    /// 内容门户访问器接口。
    /// </summary>
    public interface IContentPortalAccessor : IContentAccessor, IPortalAccessor
    {
    }


    /// <summary>
    /// 内容门户访问器接口。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public interface IContentPortalAccessor<TGenId, TIncremId, TUserId, TCreatedBy>
        : IContentAccessor<TGenId, TIncremId, TCreatedBy>,
            IPortalAccessor<TGenId, TIncremId, TUserId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
    }


    /// <summary>
    /// 内容门户访问器接口。
    /// </summary>
    /// <typeparam name="TCategory">指定的内容分类类型。</typeparam>
    /// <typeparam name="TSource">指定的内容来源类型。</typeparam>
    /// <typeparam name="TClaim">指定的内容声明类型。</typeparam>
    /// <typeparam name="TTag">指定的内容标签类型。</typeparam>
    /// <typeparam name="TUnit">指定的内容单元类型。</typeparam>
    /// <typeparam name="TUnitClaim">指定的内容单元声明类型。</typeparam>
    /// <typeparam name="TUnitTag">指定的内容单元标签类型。</typeparam>
    /// <typeparam name="TUnitVisitCount">指定的内容单元访问计数类型。</typeparam>
    /// <typeparam name="TPane">指定的内容窗格类型。</typeparam>
    /// <typeparam name="TPaneUnit">指定的内容单元类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TInternalUser">指定的内置用户类型。</typeparam>
    public interface IContentPortalAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit, TEditor, TInternalUser>
        : IContentAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit>,
            IPortalAccessor<TEditor, TInternalUser>
        where TCategory : class
        where TSource : class
        where TClaim : class
        where TTag : class
        where TUnit : class
        where TUnitClaim : class
        where TUnitTag : class
        where TUnitVisitCount : class
        where TPane : class
        where TPaneUnit : class
        where TEditor : class
        where TInternalUser : class
    {
    }
}
