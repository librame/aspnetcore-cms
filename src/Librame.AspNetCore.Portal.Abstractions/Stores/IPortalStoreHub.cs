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

namespace Librame.AspNetCore.Portal
{
    using Extensions.Data;

    /// <summary>
    /// 门户存储中心接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    public interface IPortalStoreHub<TAccessor, TUserId> : IPortalStoreHub<TAccessor, PortalPane<int, int>, string, int, TUserId>
        where TAccessor : IAccessor
        where TUserId : IEquatable<TUserId>
    {
    }


    /// <summary>
    /// 门户存储中心接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    public interface IPortalStoreHub<TAccessor, TPane, TGenId, TIncremId, TUserId>
        : IPortalStoreHub<TAccessor, PortalCategory<TIncremId>, PortalEditor<TGenId, TUserId>, PortalEditorTitle<TIncremId, TGenId>, TPane, PortalSource<TIncremId, TIncremId>, PortalSubject<TGenId, TIncremId>, PortalSubjectBody<TIncremId, TGenId>, PortalTag<TGenId>, PortalTagReference<TIncremId, TGenId>>
        where TAccessor : IAccessor
        where TPane : class
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
    {
    }


    /// <summary>
    /// 门户存储中心接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TCategory">指定的分类类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TEditorTitle">指定的编者头衔类型。</typeparam>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TSubjectBody">指定的专题主体类型。</typeparam>
    /// <typeparam name="TTag">指定的标签类型。</typeparam>
    /// <typeparam name="TTagReference">指定的标签引用类型。</typeparam>
    public interface IPortalStoreHub<TAccessor, TCategory, TEditor, TEditorTitle, TPane, TSource, TSubject, TSubjectBody, TTag, TTagReference>
        : IStoreHub<TAccessor>
        , ICategoryStore<TAccessor, TCategory>
        , IEditorStore<TAccessor, TEditor, TEditorTitle>
        , IPaneStore<TAccessor, TPane>
        , ISourceStore<TAccessor, TSource>
        , ISubjectStore<TAccessor, TSubject, TSubjectBody>
        , ITagStore<TAccessor, TTag, TTagReference>
        where TAccessor : IAccessor
        where TCategory : class
        where TEditor : class
        where TEditorTitle : class
        where TPane : class
        where TSource : class
        where TSubject : class
        where TSubjectBody : class
        where TTag : class
        where TTagReference : class
    {
    }
}
