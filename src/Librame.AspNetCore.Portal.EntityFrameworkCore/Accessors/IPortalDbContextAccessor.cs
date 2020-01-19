#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using System;

namespace Librame.AspNetCore.Portal
{
    using Extensions.Data;

    /// <summary>
    /// 门户数据库上下文访问器接口。
    /// </summary>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    public interface IPortalDbContextAccessor<TUserId> : IPortalDbContextAccessor<PortalPane<int, int>, string, int, TUserId>
        where TUserId : IEquatable<TUserId>
    {
    }


    /// <summary>
    /// 门户数据库上下文访问器接口。
    /// </summary>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    public interface IPortalDbContextAccessor<TPane, TGenId, TIncremId, TUserId> : IPortalDbContextAccessor<PortalCategory<TIncremId>, PortalEditor<TGenId, TUserId>, PortalEditorTitle<TIncremId, TGenId>, TPane, PortalSource<TIncremId, TIncremId>, PortalSubject<TGenId, TIncremId>, PortalSubjectBody<TIncremId, TGenId>, PortalTag<TGenId>, PortalTagReference<TIncremId, TGenId>>
        where TPane : class
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
    {
    }


    /// <summary>
    /// 门户数据库上下文访问器接口。
    /// </summary>
    /// <typeparam name="TCategory">指定的分类类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TEditorTitle">指定的编者头衔类型。</typeparam>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TSubjectBody">指定的专题主体类型。</typeparam>
    /// <typeparam name="TTag">指定的标签类型。</typeparam>
    /// <typeparam name="TTagReference">指定的标签引用类型。</typeparam>
    public interface IPortalDbContextAccessor<TCategory, TEditor, TEditorTitle, TPane, TSource, TSubject, TSubjectBody, TTag, TTagReference> : IAccessor
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
        /// <summary>
        /// 分类数据集。
        /// </summary>
        DbSet<TCategory> Categories { get; set; }

        /// <summary>
        /// 编者数据集。
        /// </summary>
        DbSet<TEditor> Editors { get; set; }

        /// <summary>
        /// 编者头衔数据集。
        /// </summary>
        DbSet<TEditorTitle> EditorTitles { get; set; }

        /// <summary>
        /// 窗格数据集。
        /// </summary>
        DbSet<TPane> Panes { get; set; }

        /// <summary>
        /// 来源数据集。
        /// </summary>
        DbSet<TSource> Sources { get; set; }

        /// <summary>
        /// 专题数据集。
        /// </summary>
        DbSet<TSubject> Subjects { get; set; }

        /// <summary>
        /// 专题主体数据集。
        /// </summary>
        DbSet<TSubjectBody> SubjectBodies { get; set; }

        /// <summary>
        /// 标签数据集。
        /// </summary>
        DbSet<TTag> Tags { get; set; }

        /// <summary>
        /// 标签引用数据集。
        /// </summary>
        DbSet<TTagReference> TagReferences { get; set; }
    }
}
