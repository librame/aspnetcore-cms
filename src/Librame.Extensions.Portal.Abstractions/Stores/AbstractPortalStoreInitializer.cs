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
    using Data.Accessors;
    using Data.Stores;
    using Portal.Options;
    using Portal.Services;

    /// <summary>
    /// 抽象门户存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public abstract class AbstractPortalStoreInitializer<TAccessor, TGenId, TIncremId, TUserId, TCreatedBy>
        : AbstractPortalStoreInitializer<TAccessor,
            PortalEditor<TGenId, TUserId, TCreatedBy>,
            PortalInternalUser<TGenId, TCreatedBy>,
            TGenId, TIncremId, TUserId, TCreatedBy>
        where TAccessor : class, IAccessor
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个抽象门户存储初始化器。
        /// </summary>
        /// <param name="initializationOptions">给定的 <see cref="PortalStoreInitializationOptions"/>。</param>
        /// <param name="passwordHashService">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected AbstractPortalStoreInitializer(PortalStoreInitializationOptions initializationOptions,
            IPasswordHashService<PortalInternalUser<TGenId, TCreatedBy>> passwordHashService,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(initializationOptions, passwordHashService, identifierGenerator, validator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 抽象门户存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TInternalUser">指定的内置用户类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public abstract class AbstractPortalStoreInitializer<TAccessor, TEditor, TInternalUser, TGenId, TIncremId, TUserId, TCreatedBy>
        : AbstractDataStoreInitializer<TAccessor, TGenId, TIncremId, TCreatedBy>
        where TAccessor : class, IAccessor
        where TEditor : PortalEditor<TGenId, TUserId, TCreatedBy>
        where TInternalUser : PortalInternalUser<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个抽象门户存储初始化器。
        /// </summary>
        /// <param name="initializationOptions">给定的 <see cref="PortalStoreInitializationOptions"/>。</param>
        /// <param name="passwordHashService">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected AbstractPortalStoreInitializer(PortalStoreInitializationOptions initializationOptions,
            IPasswordHashService<TInternalUser> passwordHashService,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(identifierGenerator, validator, loggerFactory)
        {
            InitializationOptions = initializationOptions.NotNull(nameof(initializationOptions));
            PasswordHashService = passwordHashService.NotNull(nameof(passwordHashService));

            PortalIdentifierGenerator = identifierGenerator.CastTo<IStoreIdentifierGenerator,
                IPortalStoreIdentifierGenerator<TGenId>>(nameof(identifierGenerator));

            CurrentInternalUsers = new List<TInternalUser>();
        }


        /// <summary>
        /// 初始化选项。
        /// </summary>
        /// <value>返回 <see cref="PortalStoreInitializationOptions"/>。</value>
        protected PortalStoreInitializationOptions InitializationOptions { get; }

        /// <summary>
        /// 密码哈希服务。
        /// </summary>
        /// <value>返回 <see cref="IPasswordHashService{TInternalUser}"/>。</value>
        protected IPasswordHashService<TInternalUser> PasswordHashService { get; }

        /// <summary>
        /// 门户存储标识符生成器。
        /// </summary>
        /// <value>返回 <see cref="IPortalStoreIdentifierGenerator{TGenId}"/>。</value>
        protected IPortalStoreIdentifierGenerator<TGenId> PortalIdentifierGenerator { get; }


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
            foreach (var name in InitializationOptions.DefaultInternalUserNames)
            {
                if (!TryGetInternalUser(name, out var internalUser))
                {
                    internalUser = typeof(TInternalUser).EnsureCreate<TInternalUser>();

                    internalUser.Id = PortalIdentifierGenerator.GenerateEditorIdAsync().ConfigureAndResult();

                    internalUser.Name = name;

                    internalUser.PopulateCreationAsync(Clock);

                    internalUser.PasswordHash = PasswordHashService.HashPassword(internalUser,
                        InitializationOptions.DefaultPassword);

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
            foreach (var pair in InitializationOptions.DefaultEditors)
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
