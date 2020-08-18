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
    using Content.Accessors;
    using Content.Builders;
    using Content.Options;
    using Content.Stores;
    using Data.Accessors;
    using Data.Stores;
    using Data.Validators;
    using Portal.Accessors;
    using Portal.Builders;
    using Portal.Options;
    using Portal.Services;

    /// <summary>
    /// 内容门户存储初始化器。
    /// </summary>
    public class ContentPortalStoreInitializer : ContentPortalStoreInitializer<ContentPortalDbContextAccessor>
    {
        /// <summary>
        /// 构造一个内容门户存储初始化器。
        /// </summary>
        /// <param name="contentOptions">给定的 <see cref="IOptions{ContentBuilderOptions}"/>。</param>
        /// <param name="portalOptions">给定的 <see cref="IOptions{PortalBuilderOptions}"/>。</param>
        /// <param name="passwordHash">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public ContentPortalStoreInitializer(IOptions<ContentBuilderOptions> contentOptions,
            IOptions<PortalBuilderOptions> portalOptions,
            IPasswordHashService<PortalInternalUser<Guid, Guid>> passwordHash,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(contentOptions, portalOptions, passwordHash, validator, generator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 内容门户存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class ContentPortalStoreInitializer<TAccessor> : ContentPortalStoreInitializer<TAccessor, Guid, int, Guid, Guid>
        where TAccessor : class, IContentAccessor, IPortalAccessor, IDataAccessor
    {
        /// <summary>
        /// 构造一个内容门户存储初始化器。
        /// </summary>
        /// <param name="contentOptions">给定的 <see cref="IOptions{ContentBuilderOptions}"/>。</param>
        /// <param name="portalOptions">给定的 <see cref="IOptions{PortalBuilderOptions}"/>。</param>
        /// <param name="passwordHash">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public ContentPortalStoreInitializer(IOptions<ContentBuilderOptions> contentOptions,
            IOptions<PortalBuilderOptions> portalOptions,
            IPasswordHashService<PortalInternalUser<Guid, Guid>> passwordHash,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(contentOptions?.Value.Stores.Initialization, portalOptions?.Value.Stores.Initialization, passwordHash,
                  validator, generator, loggerFactory)
        {
        }


        /// <summary>
        /// 累加增量标识。
        /// </summary>
        /// <param name="index">给定的索引。</param>
        /// <returns>返回整数。</returns>
        protected override int ProgressiveIncremId(int index)
            => ++index;

        /// <summary>
        /// 将生成式标识发表为查询参数值。
        /// </summary>
        /// <param name="id">给定的 <see cref="Guid"/>。</param>
        /// <param name="createdTime">给定的创建时间。</param>
        /// <returns>返回字符串。</returns>
        protected override string PublishedAsQueryValue(Guid id, DateTimeOffset createdTime)
            => id.AsShortString(createdTime);

        /// <summary>
        /// 获取用户标识。
        /// </summary>
        /// <param name="internalUserId">给定的内置用户标识。</param>
        /// <returns>返回 <see cref="Guid"/>。</returns>
        protected override Guid GetUserId(Guid internalUserId)
            => internalUserId;

    }


    /// <summary>
    /// 内容门户存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public class ContentPortalStoreInitializer<TAccessor, TGenId, TIncremId, TUserId, TPublishedBy>
        : ContentPortalStoreInitializer<TAccessor,
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
        where TAccessor : class, IContentAccessor<TGenId, TIncremId, TPublishedBy>,
            IPortalAccessor<TGenId, TIncremId, TUserId, TPublishedBy>,
            IDataAccessor<TGenId, TIncremId, TPublishedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TUserId : IEquatable<TUserId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
        /// <summary>
        /// 构造一个内容门户存储初始化器。
        /// </summary>
        /// <param name="contentInitializationOptions">给定的 <see cref="ContentStoreInitializationOptions"/>。</param>
        /// <param name="portalInitializationOptions">给定的 <see cref="PortalStoreInitializationOptions"/>。</param>
        /// <param name="passwordHash">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected ContentPortalStoreInitializer(ContentStoreInitializationOptions contentInitializationOptions,
            PortalStoreInitializationOptions portalInitializationOptions,
            IPasswordHashService<PortalInternalUser<TGenId, TPublishedBy>> passwordHash,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(contentInitializationOptions, portalInitializationOptions, passwordHash,
                  validator, generator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 内容门户存储初始化器。
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
    public class ContentPortalStoreInitializer<TAccessor, TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit, TEditor, TInternalUser, TGenId, TIncremId, TUserId, TPublishedBy>
        : ContentStoreInitializer<TAccessor, TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit, TGenId, TIncremId, TPublishedBy>
        where TAccessor : class, IContentAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit>,
            IPortalAccessor<TEditor, TInternalUser>,
            IDataAccessor<TGenId, TIncremId, TPublishedBy>
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
        /// 构造一个内容门户存储初始化器。
        /// </summary>
        /// <param name="contentInitializationOptions">给定的 <see cref="ContentStoreInitializationOptions"/>。</param>
        /// <param name="portalInitializationOptions">给定的 <see cref="PortalStoreInitializationOptions"/>。</param>
        /// <param name="passwordHash">给定的 <see cref="IPasswordHashService{TInternalUser}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected ContentPortalStoreInitializer(ContentStoreInitializationOptions contentInitializationOptions,
            PortalStoreInitializationOptions portalInitializationOptions, IPasswordHashService<TInternalUser> passwordHash,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(contentInitializationOptions, validator, generator, loggerFactory)
        {
            PortalInitializationOptions = portalInitializationOptions.NotNull(nameof(portalInitializationOptions));
            PasswordHash = passwordHash.NotNull(nameof(passwordHash));
        }


        /// <summary>
        /// 门户初始化选项。
        /// </summary>
        protected PortalStoreInitializationOptions PortalInitializationOptions { get; }

        /// <summary>
        /// 密码哈希服务。
        /// </summary>
        protected IPasswordHashService<TInternalUser> PasswordHash { get; }

        /// <summary>
        /// 门户存储标识生成器。
        /// </summary>
        /// <value>返回 <see cref="IPortalStoreIdentificationGenerator{TGenId}"/>。</value>
        protected IPortalStoreIdentificationGenerator<TGenId> PortalGenerator { get; }


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

                CurrentInternalUsers = PortalInitializationOptions.DefaultInternalUserNames.Select(name =>
                {
                    var internalUser = internalUserType.EnsureCreate<TInternalUser>();

                    internalUser.Name = name;

                    internalUser.Id = PortalGenerator.GenerateEditorId();

                    internalUser.PopulateCreation(Clock);

                    internalUser.PasswordHash = PasswordHash.HashPassword(internalUser,
                        PortalInitializationOptions.DefaultPassword);

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

                CurrentInternalUsers = PortalInitializationOptions.DefaultInternalUserNames.Select(name =>
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
                        PortalInitializationOptions.DefaultPassword);
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

                CurrentEditors = PortalInitializationOptions.DefaultEditors.Select(pair =>
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

                CurrentEditors = PortalInitializationOptions.DefaultEditors.Select(pair =>
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
