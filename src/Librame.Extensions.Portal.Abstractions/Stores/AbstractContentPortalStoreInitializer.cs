#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Librame.Extensions.Portal.Stores
{
    using Content.Options;
    using Content.Stores;
    using Data.Accessors;
    using Data.Stores;
    using Portal.Options;
    using Portal.Services;

    /// <summary>
    /// 抽象内容门户存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public abstract class AbstractContentPortalStoreInitializer<TAccessor, TGenId, TIncremId, TUserId, TPublishedBy>
        : AbstractContentPortalStoreInitializer<TAccessor,
            ContentCategory<TIncremId, TPublishedBy>,
            ContentSource<TIncremId, TPublishedBy>,
            ContentClaim<TIncremId, TPublishedBy>,
            ContentTag<TIncremId, TPublishedBy>,
            ContentUnit<TGenId, TIncremId, TIncremId, TPublishedBy>,
            ContentUnitClaim<TIncremId, TGenId, TIncremId, TPublishedBy>,
            ContentUnitTag<TIncremId, TGenId, TIncremId>,
            ContentUnitVisitCount<TGenId>,
            ContentPane<TIncremId, TPublishedBy>,
            ContentPaneUnit<TIncremId, TIncremId, TGenId, TPublishedBy>,
            PortalEditor<TGenId, TUserId, TPublishedBy>,
            PortalInternalUser<TGenId, TPublishedBy>,
            TGenId, TIncremId, TUserId, TPublishedBy>
        where TAccessor : class, IAccessor
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
        /// <summary>
        /// 构造一个抽象内容门户存储初始化器。
        /// </summary>
        /// <param name="portalInitializationOptions">给定的 <see cref="PortalStoreInitializationOptions"/>。</param>
        /// <param name="passwordHashService">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="initializationOptions">给定的 <see cref="ContentStoreInitializationOptions"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected AbstractContentPortalStoreInitializer(PortalStoreInitializationOptions portalInitializationOptions,
            IPasswordHashService<PortalInternalUser<TGenId, TPublishedBy>> passwordHashService,
            ContentStoreInitializationOptions initializationOptions,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(portalInitializationOptions, passwordHashService, initializationOptions,
                  identifierGenerator, validator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 抽象内容门户存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
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
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public abstract class AbstractContentPortalStoreInitializer<TAccessor, TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit, TEditor, TInternalUser, TGenId, TIncremId, TUserId, TPublishedBy>
        : AbstractContentStoreInitializer<TAccessor, TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit, TGenId, TIncremId, TPublishedBy>
        where TAccessor : class, IAccessor
        where TCategory : ContentCategory<TIncremId, TPublishedBy>
        where TSource : ContentSource<TIncremId, TPublishedBy>
        where TClaim : ContentClaim<TIncremId, TPublishedBy>
        where TTag : ContentTag<TIncremId, TPublishedBy>
        where TUnit : ContentUnit<TGenId, TIncremId, TIncremId, TPublishedBy>
        where TUnitClaim : ContentUnitClaim<TIncremId, TGenId, TIncremId, TPublishedBy>
        where TUnitTag : ContentUnitTag<TIncremId, TGenId, TIncremId>
        where TUnitVisitCount : ContentUnitVisitCount<TGenId>
        where TPane : ContentPane<TIncremId, TPublishedBy>
        where TPaneUnit : ContentPaneUnit<TIncremId, TIncremId, TGenId, TPublishedBy>
        where TEditor : PortalEditor<TGenId, TUserId, TPublishedBy>
        where TInternalUser : PortalInternalUser<TGenId, TPublishedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
        /// <summary>
        /// 构造一个抽象内容门户存储初始化器。
        /// </summary>
        /// <param name="portalInitializationOptions">给定的 <see cref="PortalStoreInitializationOptions"/>。</param>
        /// <param name="passwordHashService">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="initializationOptions">给定的 <see cref="ContentStoreInitializationOptions"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected AbstractContentPortalStoreInitializer(PortalStoreInitializationOptions portalInitializationOptions,
            IPasswordHashService<TInternalUser> passwordHashService,
            ContentStoreInitializationOptions initializationOptions,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(initializationOptions, identifierGenerator, validator, loggerFactory)
        {
            PortalInitializationOptions = portalInitializationOptions.NotNull(nameof(portalInitializationOptions));
            PasswordHashService = passwordHashService.NotNull(nameof(passwordHashService));

            CurrentInternalUsers = new List<TInternalUser>();
        }


        /// <summary>
        /// 初始化选项。
        /// </summary>
        protected PortalStoreInitializationOptions PortalInitializationOptions { get; }

        /// <summary>
        /// 密码哈希服务。
        /// </summary>
        protected IPasswordHashService<TInternalUser> PasswordHashService { get; }

        /// <summary>
        /// 门户存储标识符生成器。
        /// </summary>
        protected AbstractPortalStoreIdentifierGenerator<TGenId> PortalIdentifierGenerator
            => IdentifierGenerator as AbstractPortalStoreIdentifierGenerator<TGenId>;


        /// <summary>
        /// 当前内置用户列表。
        /// </summary>
        protected IList<TInternalUser> CurrentInternalUsers { get; }


        /// <summary>
        /// 获取用户标识。
        /// </summary>
        /// <param name="internalUserId">给定的内置用户标识。</param>
        /// <returns>返回 <typeparamref name="TUserId"/>。</returns>
        protected abstract TUserId GetUserId(TGenId internalUserId);


        /// <summary>
        /// 初始化核心。
        /// </summary>
        /// <param name="stores">给定的 <see cref="IStoreHub"/>。</param>
        protected override void InitializeCore(IStoreHub stores)
        {
            base.InitializeCore(stores);

            if (stores is IPortalStoreHub<TEditor, TInternalUser> portalStores)
            {
                InitializeInternalUsers(portalStores);

                InitializeEditors(portalStores);
            }
        }

        /// <summary>
        /// 初始化内置用户集合。
        /// </summary>
        /// <param name="portalStores">给定的门户存储中心。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeInternalUsers(IPortalStoreHub<TEditor, TInternalUser> portalStores)
        {
            foreach (var name in PortalInitializationOptions.DefaultInternalUserNames)
            {
                if (!TryGetInternalUser(name, out var internalUser))
                {
                    internalUser = typeof(TInternalUser).EnsureCreate<TInternalUser>();

                    internalUser.Id = PortalIdentifierGenerator.GenerateEditorIdAsync().ConfigureAndResult();

                    internalUser.Name = name;

                    internalUser.PopulateCreationAsync(Clock);

                    internalUser.PasswordHash = PasswordHashService.HashPassword(internalUser,
                        PortalInitializationOptions.DefaultPassword);

                    portalStores.TryCreate(internalUser);

                    RequiredSaveChanges = true;
                }

                CurrentInternalUsers.Add(internalUser);
            }

            // TryGetInternalUser
            bool TryGetInternalUser(string name, out TInternalUser internalUser)
            {
                internalUser = portalStores.InternalUsers.FirstOrDefault(p => p.Name == name);
                return internalUser.IsNotNull();
            }
        }

        /// <summary>
        /// 初始化编者集合。
        /// </summary>
        /// <param name="portalStores">给定的门户存储中心。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeEditors(IPortalStoreHub<TEditor, TInternalUser> portalStores)
        {
            foreach (var pair in PortalInitializationOptions.DefaultEditors)
            {
                if (!TryGetEditor(pair.Key, out var editor))
                {
                    editor = typeof(TEditor).EnsureCreate<TEditor>();

                    editor.Id = PortalIdentifierGenerator.GenerateEditorIdAsync().ConfigureAndResult();
                    editor.UserId = GetUserId(CurrentInternalUsers[0].Id);

                    editor.Name = pair.Key;
                    editor.Description = pair.Value;

                    editor.PopulateCreationAsync(Clock);

                    portalStores.TryCreate(editor);

                    RequiredSaveChanges = true;
                }
            }

            // TryGetEditor
            bool TryGetEditor(string editorName, out TEditor editor)
            {
                editor = portalStores.Editors.FirstOrDefault(p => p.Name == editorName);
                return editor.IsNotNull();
            }
        }

    }
}
