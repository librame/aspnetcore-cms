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
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Portal.Stores
{
    using Data.Accessors;
    using Data.Stores;
    using Data.Validators;
    using Portal.Accessors;
    using Portal.Builders;
    using Portal.Options;
    using Portal.Services;

    /// <summary>
    /// 门户存储初始化器。
    /// </summary>
    public class PortalStoreInitializer : PortalStoreInitializer<PortalDbContextAccessor>
    {
        /// <summary>
        /// 构造一个门户存储初始化器。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{PortalBuilderOptions}"/>。</param>
        /// <param name="passwordHash">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public PortalStoreInitializer(IOptions<PortalBuilderOptions> options,
            IPasswordHashService<PortalInternalUser<Guid, Guid>> passwordHash,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(options, passwordHash, validator, generator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 门户存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class PortalStoreInitializer<TAccessor> : PortalStoreInitializer<TAccessor, Guid, int, Guid, Guid>
        where TAccessor : class, IPortalAccessor, IDataAccessor
    {
        /// <summary>
        /// 构造一个门户存储初始化器。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{PortalBuilderOptions}"/>。</param>
        /// <param name="passwordHash">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public PortalStoreInitializer(IOptions<PortalBuilderOptions> options,
            IPasswordHashService<PortalInternalUser<Guid, Guid>> passwordHash,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(options?.Value.Stores.Initialization, passwordHash, validator, generator, loggerFactory)
        {
        }


        /// <summary>
        /// 获取用户标识。
        /// </summary>
        /// <param name="internalUserId">给定的内置用户标识。</param>
        /// <returns>返回 <see cref="Guid"/>。</returns>
        protected override Guid GetUserId(Guid internalUserId)
            => internalUserId;

    }


    /// <summary>
    /// 门户存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class PortalStoreInitializer<TAccessor, TGenId, TIncremId, TUserId, TCreatedBy>
        : PortalStoreInitializer<TAccessor,
            PortalEditor<TGenId, TUserId, TCreatedBy>,
            PortalInternalUser<TGenId, TCreatedBy>,
            TGenId, TIncremId, TUserId, TCreatedBy>
        where TAccessor : class, IPortalAccessor<TGenId, TIncremId, TUserId, TCreatedBy>,
            IDataAccessor<TGenId, TIncremId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个门户存储初始化器。
        /// </summary>
        /// <param name="initializationOptions">给定的 <see cref="PortalStoreInitializationOptions"/>。</param>
        /// <param name="passwordHash">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected PortalStoreInitializer(PortalStoreInitializationOptions initializationOptions,
            IPasswordHashService<PortalInternalUser<TGenId, TCreatedBy>> passwordHash,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(initializationOptions, passwordHash, validator, generator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 门户存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TInternalUser">指定的内置用户类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class PortalStoreInitializer<TAccessor, TEditor, TInternalUser, TGenId, TIncremId, TUserId, TCreatedBy>
        : DataStoreInitializer<TAccessor, TGenId, TIncremId, TCreatedBy>
        where TAccessor : class, IPortalAccessor<TEditor, TInternalUser>,
            IDataAccessor<TGenId, TIncremId, TCreatedBy>
        where TEditor : PortalEditor<TGenId, TUserId, TCreatedBy>
        where TInternalUser : PortalInternalUser<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个门户存储初始化器。
        /// </summary>
        /// <param name="initializationOptions">给定的 <see cref="PortalStoreInitializationOptions"/>。</param>
        /// <param name="passwordHash">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected PortalStoreInitializer(PortalStoreInitializationOptions initializationOptions,
            IPasswordHashService<TInternalUser> passwordHash,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(validator, generator, loggerFactory)
        {
            InitializationOptions = initializationOptions.NotNull(nameof(initializationOptions));
            PasswordHash = passwordHash.NotNull(nameof(passwordHash));
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
        protected IPasswordHashService<TInternalUser> PasswordHash { get; }

        /// <summary>
        /// 门户存储标识生成器。
        /// </summary>
        /// <value>返回 <see cref="IPortalStoreIdentificationGenerator{TGenId}"/>。</value>
        protected IPortalStoreIdentificationGenerator<TGenId> PortalGenerator
            => Generator as IPortalStoreIdentificationGenerator<TGenId>;


        /// <summary>
        /// 当前内置用户列表。
        /// </summary>
        protected IReadOnlyList<TInternalUser> CurrentInternalUsers { get; set; }

        /// <summary>
        /// 当前编者列表。
        /// </summary>
        protected IReadOnlyList<TEditor> CurrentEditors { get; set; }


        /// <summary>
        /// 获取用户标识。
        /// </summary>
        /// <param name="internalUserId">给定的内置用户标识。</param>
        /// <returns>返回 <typeparamref name="TUserId"/>。</returns>
        protected virtual TUserId GetUserId(TGenId internalUserId)
            => throw new NotImplementedException();


        /// <summary>
        /// 初始化存储集合。
        /// </summary>
        protected override void InitializeStores()
        {
            base.InitializeStores();

            InitializeInternalUsers();

            InitializeEditors();
        }

        /// <summary>
        /// 异步初始化存储集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        protected override async Task InitializeStoresAsync(CancellationToken cancellationToken)
        {
            await base.InitializeStoresAsync(cancellationToken).ConfigureAwait();

            await InitializeInternalUsersAsync(cancellationToken).ConfigureAwait();

            await InitializeEditorsAsync(cancellationToken).ConfigureAwait();
        }


        /// <summary>
        /// 初始化内置用户集合。
        /// </summary>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeInternalUsers()
        {
            if (CurrentInternalUsers.IsEmpty())
            {
                var internalUserType = typeof(TInternalUser);

                CurrentInternalUsers = InitializationOptions.DefaultInternalUserNames.Select(name =>
                {
                    var internalUser = internalUserType.EnsureCreate<TInternalUser>();

                    internalUser.Name = name;

                    internalUser.Id = PortalGenerator.GenerateEditorId();

                    internalUser.PopulateCreation(Clock);

                    internalUser.PasswordHash = PasswordHash.HashPassword(internalUser,
                        InitializationOptions.DefaultPassword);

                    return internalUser;
                })
                .ToList();
            }

            Accessor.InternalUsersManager.TryAddRange(p => p.Equals(CurrentInternalUsers[0]),
                () => CurrentInternalUsers,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                });
        }

        /// <summary>
        /// 异步初始化内置用户集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual Task InitializeInternalUsersAsync(CancellationToken cancellationToken)
        {
            if (CurrentInternalUsers.IsEmpty())
            {
                var internalUserType = typeof(TInternalUser);

                CurrentInternalUsers = InitializationOptions.DefaultInternalUserNames.Select(name =>
                {
                    var internalUser = internalUserType.EnsureCreate<TInternalUser>();

                    internalUser.Name = name;

                    return internalUser;
                })
                .ToList();

                CurrentInternalUsers.ForEach(async internalUser =>
                {
                    internalUser.Id = await PortalGenerator.GenerateEditorIdAsync(cancellationToken).ConfigureAwait();

                    await internalUser.PopulateCreationAsync(Clock, cancellationToken).ConfigureAwait();

                    internalUser.PasswordHash = PasswordHash.HashPassword(internalUser,
                        InitializationOptions.DefaultPassword);
                });
            }

            return Accessor.InternalUsersManager.TryAddRangeAsync(p => p.Equals(CurrentInternalUsers[0]),
                () => CurrentInternalUsers,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                },
                cancellationToken);
        }


        /// <summary>
        /// 初始化编者集合。
        /// </summary>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeEditors()
        {
            if (CurrentEditors.IsEmpty())
            {
                var editorType = typeof(TEditor);

                CurrentEditors = InitializationOptions.DefaultEditors.Select(pair =>
                {
                    var editor = editorType.EnsureCreate<TEditor>();

                    editor.Id = PortalGenerator.GenerateEditorId();
                    editor.UserId = GetUserId(CurrentInternalUsers[0].Id);

                    editor.Name = pair.Key;
                    editor.Description = pair.Value;

                    editor.PopulateCreation(Clock);

                    return editor;
                })
                .ToList();
            }

            Accessor.EditorsManager.TryAddRange(p => p.Equals(CurrentEditors[0]),
                () => CurrentEditors,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                });
        }

        /// <summary>
        /// 异步初始化编者集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual Task InitializeEditorsAsync(CancellationToken cancellationToken)
        {
            if (CurrentEditors.IsEmpty())
            {
                var editorType = typeof(TEditor);

                CurrentEditors = InitializationOptions.DefaultEditors.Select(pair =>
                {
                    var editor = editorType.EnsureCreate<TEditor>();

                    editor.UserId = GetUserId(CurrentInternalUsers[0].Id);

                    editor.Name = pair.Key;
                    editor.Description = pair.Value;

                    return editor;
                })
                .ToList();

                CurrentEditors.ForEach(async editor =>
                {
                    editor.Id = await PortalGenerator.GenerateEditorIdAsync(cancellationToken).ConfigureAwait();

                    await editor.PopulateCreationAsync(Clock, cancellationToken).ConfigureAwait();
                });
            }

            return Accessor.EditorsManager.TryAddRangeAsync(p => p.Equals(CurrentEditors[0]),
                () => CurrentEditors,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                },
                cancellationToken);
        }

    }
}
