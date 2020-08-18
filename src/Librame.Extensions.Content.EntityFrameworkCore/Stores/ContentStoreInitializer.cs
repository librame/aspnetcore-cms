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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Content.Stores
{
    using Content.Accessors;
    using Content.Builders;
    using Content.Options;
    using Data.Accessors;
    using Data.Stores;
    using Data.Validators;

    /// <summary>
    /// 内容存储初始化器。
    /// </summary>
    public class ContentStoreInitializer : ContentStoreInitializer<ContentDbContextAccessor>
    {
        /// <summary>
        /// 构造一个内容存储初始化器。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{ContentBuilderOptions}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public ContentStoreInitializer(IOptions<ContentBuilderOptions> options,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(options, validator, generator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 内容存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class ContentStoreInitializer<TAccessor> : ContentStoreInitializer<TAccessor, Guid, int, Guid>
        where TAccessor : class, IContentAccessor, IDataAccessor
    {
        /// <summary>
        /// 构造一个内容存储初始化器。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{ContentBuilderOptions}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public ContentStoreInitializer(IOptions<ContentBuilderOptions> options,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(options?.Value.Stores.Initialization, validator, generator, loggerFactory)
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

    }


    /// <summary>
    /// 内容存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public class ContentStoreInitializer<TAccessor, TGenId, TIncremId, TPublishedBy>
        : ContentStoreInitializer<TAccessor,
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
            TGenId, TIncremId, TPublishedBy>
        where TAccessor : class, IContentAccessor<TGenId, TIncremId, TPublishedBy>,
            IDataAccessor<TGenId, TIncremId, TPublishedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
        /// <summary>
        /// 构造一个内容存储初始化器。
        /// </summary>
        /// <param name="initializationOptions">给定的 <see cref="ContentStoreInitializationOptions"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected ContentStoreInitializer(ContentStoreInitializationOptions initializationOptions,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(initializationOptions, validator, generator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 内容存储初始化器。
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
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public class ContentStoreInitializer<TAccessor, TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit, TGenId, TIncremId, TPublishedBy>
        : DataStoreInitializer<TAccessor, TGenId, TIncremId, TPublishedBy>
        where TAccessor : class, IContentAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit>,
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
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
        private readonly TIncremId _defaultIncremId;


        /// <summary>
        /// 构造一个内容存储初始化器。
        /// </summary>
        /// <param name="initializationOptions">给定的 <see cref="ContentStoreInitializationOptions"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected ContentStoreInitializer(ContentStoreInitializationOptions initializationOptions,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(validator, generator, loggerFactory)
        {
            _defaultIncremId = default;
            InitializationOptions = initializationOptions.NotNull(nameof(initializationOptions));
        }


        /// <summary>
        /// 初始化选项。
        /// </summary>
        /// <value>返回 <see cref="ContentStoreInitializationOptions"/>。</value>
        protected ContentStoreInitializationOptions InitializationOptions { get; }

        /// <summary>
        /// 内容标识生成器。
        /// </summary>
        /// <value>返回 <see cref="IContentStoreIdentificationGenerator{TGenId}"/>。</value>
        protected IContentStoreIdentificationGenerator<TGenId> ContentGenerator
            => Generator as IContentStoreIdentificationGenerator<TGenId>;


        /// <summary>
        /// 当前分类列表。
        /// </summary>
        protected IReadOnlyList<TCategory> CurrentCategories { get; set; }

        /// <summary>
        /// 当前来源列表。
        /// </summary>
        protected IReadOnlyList<TSource> CurrentSources { get; set; }

        /// <summary>
        /// 当前声明列表。
        /// </summary>
        protected IReadOnlyList<TClaim> CurrentClaims { get; set; }

        /// <summary>
        /// 当前标签列表。
        /// </summary>
        protected IReadOnlyList<TTag> CurrentTags { get; set; }

        /// <summary>
        /// 当前单元列表。
        /// </summary>
        protected IReadOnlyList<TUnit> CurrentUnits { get; set; }

        /// <summary>
        /// 当前窗格列表。
        /// </summary>
        protected IReadOnlyList<TPane> CurrentPanes { get; set; }


        /// <summary>
        /// 累加增量标识。
        /// </summary>
        /// <param name="index">给定的索引。</param>
        /// <returns>返回 <typeparamref name="TIncremId"/>。</returns>
        protected virtual TIncremId ProgressiveIncremId(int index)
            => throw new NotImplementedException();

        /// <summary>
        /// 将生成式标识发表为查询参数值。
        /// </summary>
        /// <param name="id">给定的 <typeparamref name="TGenId"/>。</param>
        /// <param name="createdTime">给定的创建时间。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string PublishedAsQueryValue(TGenId id, DateTimeOffset createdTime)
            => throw new NotImplementedException();


        /// <summary>
        /// 初始化存储集合。
        /// </summary>
        protected override void InitializeStores()
        {
            base.InitializeStores();

            InitializeCategories();

            InitializeSources();

            InitializeClaims();

            InitializeTags();

            InitializeUnits();

            InitializePanes();
        }

        /// <summary>
        /// 异步初始化存储集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        protected override async Task InitializeStoresAsync(CancellationToken cancellationToken)
        {
            await base.InitializeStoresAsync(cancellationToken).ConfigureAwait();

            await InitializeCategoriesAsync(cancellationToken).ConfigureAwait();

            await InitializeSourcesAsync(cancellationToken).ConfigureAwait();

            await InitializeClaimsAsync(cancellationToken).ConfigureAwait();

            await InitializeTagsAsync(cancellationToken).ConfigureAwait();

            await InitializeUnitsAsync(cancellationToken).ConfigureAwait();

            await InitializePanesAsync(cancellationToken).ConfigureAwait();
        }


        /// <summary>
        /// 初始化分类集合。
        /// </summary>
        protected virtual void InitializeCategories()
        {
            if (CurrentCategories.IsEmpty())
            {
                var categoryType = typeof(TCategory);

                CurrentCategories = InitializationOptions.DefaultCategories.Select(pair =>
                {
                    var category = categoryType.EnsureCreate<TCategory>();

                    category.Name = pair.Key;
                    category.ParentId = GetParentId(pair.Value.parentName);
                    category.Description = pair.Value.description;

                    category.PopulateCreation(Clock);

                    return category;
                })
                .ToList();
            }

            Accessor.CategoriesManager.TryAddRange(p => p.Equals(CurrentCategories[0]),
                () => CurrentCategories,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                });

            // GetParentId
            TIncremId GetParentId(string parentName)
            {
                if (parentName.IsEmpty())
                    return default;

                var category = Accessor.Categories.FirstOrDefault(p => p.Name == parentName);
                return category.IsNotNull() ? category.Id : default;
            }
        }

        /// <summary>
        /// 异步初始化分类集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        protected virtual Task InitializeCategoriesAsync(CancellationToken cancellationToken)
        {
            if (CurrentCategories.IsEmpty())
            {
                var categoryType = typeof(TCategory);

                CurrentCategories = InitializationOptions.DefaultCategories.Select(pair =>
                {
                    var category = categoryType.EnsureCreate<TCategory>();

                    category.Name = pair.Key;
                    category.ParentId = GetParentId(pair.Value.parentName);
                    category.Description = pair.Value.description;

                    return category;
                })
                .ToList();

                CurrentCategories.ForEach(async category =>
                {
                    await category.PopulateCreationAsync(Clock, cancellationToken).ConfigureAwait();
                });
            }

            return Accessor.CategoriesManager.TryAddRangeAsync(p => p.Equals(CurrentCategories[0]),
                () => CurrentCategories,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                },
                cancellationToken);

            // GetParentId
            TIncremId GetParentId(string parentName)
            {
                if (parentName.IsEmpty())
                    return default;

                var category = Accessor.Categories.FirstOrDefault(p => p.Name == parentName);
                return category.IsNotNull() ? category.Id : default;
            }
        }


        /// <summary>
        /// 初始化来源集合。
        /// </summary>
        protected virtual void InitializeSources()
        {
            if (CurrentSources.IsEmpty())
            {
                var sourceType = typeof(TSource);

                CurrentSources = InitializationOptions.DefaultSources.Select(pair =>
                {
                    var source = sourceType.EnsureCreate<TSource>();

                    source.Name = pair.Key;
                    source.ParentId = GetParentId(pair.Value.parentName);
                    source.Description = pair.Value.description;

                    source.PopulateCreation(Clock);

                    return source;
                })
                .ToList();
            }

            Accessor.SourcesManager.TryAddRange(p => p.Equals(CurrentSources[0]),
                () => CurrentSources,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                });

            // GetParentId
            TIncremId GetParentId(string parentName)
            {
                if (parentName.IsEmpty())
                    return default;

                var source = Accessor.Sources.FirstOrDefault(p => p.Name == parentName);
                return source.IsNotNull() ? source.Id : default;
            }
        }

        /// <summary>
        /// 异步初始化来源集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        protected virtual Task InitializeSourcesAsync(CancellationToken cancellationToken)
        {
            if (CurrentSources.IsEmpty())
            {
                var sourceType = typeof(TSource);

                CurrentSources = InitializationOptions.DefaultSources.Select(pair =>
                {
                    var source = sourceType.EnsureCreate<TSource>();

                    source.Name = pair.Key;
                    source.ParentId = GetParentId(pair.Value.parentName);
                    source.Description = pair.Value.description;

                    return source;
                })
                .ToList();

                CurrentCategories.ForEach(async category =>
                {
                    await category.PopulateCreationAsync(Clock, cancellationToken).ConfigureAwait();
                });
            }

            return Accessor.SourcesManager.TryAddRangeAsync(p => p.Equals(CurrentSources[0]),
                () => CurrentSources,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                },
                cancellationToken);

            // GetParentId
            TIncremId GetParentId(string parentName)
            {
                if (parentName.IsEmpty())
                    return default;

                var source = Accessor.Sources.FirstOrDefault(p => p.Name == parentName);
                return source.IsNotNull() ? source.Id : default;
            }
        }


        /// <summary>
        /// 初始化声明集合。
        /// </summary>
        protected virtual void InitializeClaims()
        {
            if (CurrentClaims.IsEmpty())
            {
                var claimType = typeof(TClaim);

                CurrentClaims = InitializationOptions.DefaultClaims.Select(pair =>
                {
                    var claim = claimType.EnsureCreate<TClaim>();

                    claim.Name = pair.Key;
                    claim.Description = pair.Value;

                    claim.PopulateCreation(Clock);

                    return claim;
                })
                .ToList();
            }

            Accessor.ClaimsManager.TryAddRange(p => p.Equals(CurrentClaims[0]),
                () => CurrentClaims,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                });
        }

        /// <summary>
        /// 异步初始化声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        protected virtual Task InitializeClaimsAsync(CancellationToken cancellationToken)
        {
            if (CurrentClaims.IsEmpty())
            {
                var claimType = typeof(TClaim);

                CurrentClaims = InitializationOptions.DefaultClaims.Select(pair =>
                {
                    var claim = claimType.EnsureCreate<TClaim>();

                    claim.Name = pair.Key;
                    claim.Description = pair.Value;

                    return claim;
                })
                .ToList();

                CurrentClaims.ForEach(async claim =>
                {
                    await claim.PopulateCreationAsync(Clock, cancellationToken).ConfigureAwait();
                });
            }

            return Accessor.ClaimsManager.TryAddRangeAsync(p => p.Equals(CurrentClaims[0]),
                () => CurrentClaims,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                },
                cancellationToken);
        }


        /// <summary>
        /// 初始化标签集合。
        /// </summary>
        protected virtual void InitializeTags()
        {
            if (CurrentTags.IsEmpty())
            {
                var tagType = typeof(TTag);

                CurrentTags = InitializationOptions.DefaultTags.Select(name =>
                {
                    var tag = tagType.EnsureCreate<TTag>();

                    tag.Name = name;

                    tag.PopulateCreation(Clock);

                    return tag;
                })
                .ToList();
            }

            Accessor.TagsManager.TryAddRange(p => p.Equals(CurrentTags[0]),
                () => CurrentTags,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                });
        }

        /// <summary>
        /// 初始化标签集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        protected virtual Task InitializeTagsAsync(CancellationToken cancellationToken)
        {
            if (CurrentTags.IsEmpty())
            {
                var tagType = typeof(TTag);

                CurrentTags = InitializationOptions.DefaultTags.Select(name =>
                {
                    var tag = tagType.EnsureCreate<TTag>();

                    tag.Name = name;

                    return tag;
                })
                .ToList();

                CurrentTags.ForEach(async tag =>
                {
                    await tag.PopulateCreationAsync(Clock, cancellationToken).ConfigureAwait();
                });
            }

            return Accessor.TagsManager.TryAddRangeAsync(p => p.Equals(CurrentTags[0]),
                () => CurrentTags,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                },
                cancellationToken);
        }


        /// <summary>
        /// 初始化单元集合。
        /// </summary>
        protected virtual void InitializeUnits()
        {
            if (CurrentUnits.IsEmpty())
            {
                var unitType = typeof(TUnit);
                var units = new List<TUnit>();

                var tagIndex = 0;
                var tag = CurrentTags[tagIndex];

                var categoryIndex = 0;
                foreach (var category in CurrentCategories)
                {
                    foreach (var claim in CurrentClaims)
                    {
                        var unit = unitType.EnsureCreate<TUnit>();

                        unit.Id = ContentGenerator.GenerateUnitId();

                        unit.CategoryId = category.Id.Equals(_defaultIncremId)
                            ? ProgressiveIncremId(categoryIndex)
                            : category.Id;

                        unit.Title = $"测试{category.Name}{claim.Name}标题";
                        unit.Subtitle = $"测试{category.Name}{claim.Name}副标题";
                        unit.Tags = $"测试,{category.Name}";

                        //unit.Reference = "/Reference";
                        unit.PublishedAs = $"/unit/{PublishedAsQueryValue(unit.Id, unit.CreatedTime)}";

                        unit.PopulateCreation(Clock);

                        units.Add(unit);
                    }

                    categoryIndex++;
                }

                CurrentUnits = units;
            }

            Accessor.UnitsManager.TryAddRange(p => p.Equals(CurrentUnits[0]),
                () =>
                {
                    var unitClaimType = typeof(TUnitClaim);
                    var unitTagType = typeof(TUnitTag);
                    var unitVisitCountType = typeof(TUnitVisitCount);

                    foreach (var unit in CurrentUnits)
                    {
                        // AddUnitClaims
                        var claimIndex = 0;
                        foreach (var claim in CurrentClaims)
                        {
                            var unitClaim = unitClaimType.EnsureCreate<TUnitClaim>();

                            unitClaim.UnitId = unit.Id;
                            unitClaim.ClaimId = claim.Id.Equals(_defaultIncremId)
                                ? ProgressiveIncremId(claimIndex)
                                : claim.Id;

                            unitClaim.ClaimValue = unit.Title.Replace("标题", "声明值", StringComparison.InvariantCulture);

                            unitClaim.PopulateCreation(Clock);

                            Accessor.UnitClaims.Add(unitClaim);
                            claimIndex++;
                        }

                        // AddUnitTags
                        var tagIndex = 0;
                        foreach (var tag in CurrentTags)
                        {
                            var unitTag = unitTagType.EnsureCreate<TUnitTag>();

                            unitTag.UnitId = unit.Id;
                            unitTag.TagId = tag.Id.Equals(_defaultIncremId)
                                ? ProgressiveIncremId(tagIndex)
                                : tag.Id;

                            Accessor.UnitTags.Add(unitTag);
                            tagIndex++;
                        }

                        // 添加访问计数
                        var unitVisitCount = unitVisitCountType.EnsureCreate<TUnitVisitCount>();

                        unitVisitCount.UnitId = unit.Id;

                        Accessor.UnitVisitCounts.Add(unitVisitCount);
                    }

                    return CurrentUnits;
                },
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                });
        }

        /// <summary>
        /// 初始化单元集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        protected virtual async Task InitializeUnitsAsync(CancellationToken cancellationToken)
        {
            if (CurrentUnits.IsEmpty())
            {
                var unitType = typeof(TUnit);
                var units = new List<TUnit>();

                var tagIndex = 0;
                var tag = CurrentTags[tagIndex];

                var categoryIndex = 0;
                foreach (var category in CurrentCategories)
                {
                    foreach (var claim in CurrentClaims)
                    {
                        var unit = unitType.EnsureCreate<TUnit>();

                        unit.Id = await ContentGenerator.GenerateUnitIdAsync(cancellationToken).ConfigureAwait();

                        unit.CategoryId = category.Id.Equals(_defaultIncremId)
                            ? ProgressiveIncremId(categoryIndex)
                            : category.Id;

                        unit.Title = $"测试{category.Name}{claim.Name}标题";
                        unit.Subtitle = $"测试{category.Name}{claim.Name}副标题";
                        unit.Tags = $"测试,{category.Name}";

                        //unit.Reference = "/Reference";
                        unit.PublishedAs = $"/unit/{PublishedAsQueryValue(unit.Id, unit.CreatedTime)}";

                        await unit.PopulateCreationAsync(Clock, cancellationToken).ConfigureAwait();

                        units.Add(unit);
                    }

                    categoryIndex++;
                }

                CurrentUnits = units;
            }

            await Accessor.UnitsManager.TryAddRangeAsync(p => p.Equals(CurrentUnits[0]),
                async () =>
                {
                    var unitClaimType = typeof(TUnitClaim);
                    var unitTagType = typeof(TUnitTag);
                    var unitVisitCountType = typeof(TUnitVisitCount);

                    foreach (var unit in CurrentUnits)
                    {
                        // AddUnitClaims
                        var claimIndex = 0;
                        foreach (var claim in CurrentClaims)
                        {
                            var unitClaim = unitClaimType.EnsureCreate<TUnitClaim>();

                            unitClaim.UnitId = unit.Id;
                            unitClaim.ClaimId = claim.Id.Equals(_defaultIncremId)
                                ? ProgressiveIncremId(claimIndex)
                                : claim.Id;

                            unitClaim.ClaimValue = unit.Title.Replace("标题", "声明值", StringComparison.InvariantCulture);

                            await unitClaim.PopulateCreationAsync(Clock, cancellationToken).ConfigureAwait();

                            await Accessor.UnitClaims.AddAsync(unitClaim).ConfigureAwait();
                            claimIndex++;
                        }

                        // AddUnitTags
                        var tagIndex = 0;
                        foreach (var tag in CurrentTags)
                        {
                            var unitTag = unitTagType.EnsureCreate<TUnitTag>();

                            unitTag.UnitId = unit.Id;
                            unitTag.TagId = tag.Id.Equals(_defaultIncremId)
                                ? ProgressiveIncremId(tagIndex)
                                : tag.Id;

                            await Accessor.UnitTags.AddAsync(unitTag).ConfigureAwait();
                            tagIndex++;
                        }

                        // 添加访问计数
                        var unitVisitCount = unitVisitCountType.EnsureCreate<TUnitVisitCount>();

                        unitVisitCount.UnitId = unit.Id;

                        await Accessor.UnitVisitCounts.AddAsync(unitVisitCount).ConfigureAwait();
                    }

                    return CurrentUnits;
                },
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                },
                cancellationToken).ConfigureAwait();
        }


        /// <summary>
        /// 初始化窗格集合。
        /// </summary>
        protected virtual void InitializePanes()
        {
            var defaultPanes = InitializationOptions.DefaultPanes;

            if (CurrentPanes.IsEmpty())
            {
                var paneType = typeof(TPane);

                CurrentPanes = defaultPanes.Select(pair =>
                {
                    var pane = paneType.EnsureCreate<TPane>();

                    pane.Name = pair.Key;
                    pane.ParentId = GetParentId(pair.Value.parentName);
                    pane.Description = pair.Value.description;

                    pane.PopulateCreation(Clock);

                    return pane;
                })
                .ToList();
            }

            Accessor.PanesManager.TryAddRange(p => p.Equals(CurrentPanes[0]),
                () =>
                {
                    // AddPaneUnits
                    var paneUnitType = typeof(TPaneUnit);

                    var allUnitIds = CurrentUnits.Select(s => s.Id).ToList();
                    var paneUnitIdsCount = allUnitIds.Count / defaultPanes.Count;

                    var paneIndex = 0;
                    foreach (var pane in CurrentPanes)
                    {
                        for (var i = 0; i < paneUnitIdsCount; i++)
                        {
                            var paneUnit = paneUnitType.EnsureCreate<TPaneUnit>();

                            var unitIndex = paneIndex * paneUnitIdsCount + i;
                            paneUnit.UnitId = CurrentUnits[unitIndex].Id;

                            paneUnit.PaneId = pane.Id.Equals(_defaultIncremId)
                                ? ProgressiveIncremId(paneIndex)
                                : pane.Id;

                            paneUnit.PopulateCreation(Clock);

                            Accessor.PaneUnits.Add(paneUnit);
                        }

                        paneIndex++;
                    }

                    return CurrentPanes;
                },
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                });

            // GetParentId
            TIncremId GetParentId(string parentName)
            {
                if (parentName.IsEmpty())
                    return default;

                var pane = Accessor.Panes.FirstOrDefault(p => p.Name == parentName);
                return pane.IsNotNull() ? pane.Id : default;
            }
        }

        /// <summary>
        /// 初始化窗格集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        protected virtual Task InitializePanesAsync(CancellationToken cancellationToken)
        {
            var defaultPanes = InitializationOptions.DefaultPanes;

            if (CurrentPanes.IsEmpty())
            {
                var paneType = typeof(TPane);

                CurrentPanes = defaultPanes.Select(pair =>
                {
                    var pane = paneType.EnsureCreate<TPane>();

                    pane.Name = pair.Key;
                    pane.ParentId = GetParentId(pair.Value.parentName);
                    pane.Description = pair.Value.description;

                    return pane;
                })
                .ToList();

                CurrentPanes.ForEach(async pane =>
                {
                    await pane.PopulateCreationAsync(Clock, cancellationToken).ConfigureAwait();
                });
            }

            return Accessor.PanesManager.TryAddRangeAsync(p => p.Equals(CurrentPanes[0]),
                async () =>
                {
                    // AddPaneUnits
                    var paneUnitType = typeof(TPaneUnit);

                    var allUnitIds = CurrentUnits.Select(s => s.Id).ToList();
                    var paneUnitIdsCount = allUnitIds.Count / defaultPanes.Count;

                    var paneIndex = 0;
                    foreach (var pane in CurrentPanes)
                    {
                        for (var i = 0; i < paneUnitIdsCount; i++)
                        {
                            var paneUnit = paneUnitType.EnsureCreate<TPaneUnit>();

                            var unitIndex = paneIndex * paneUnitIdsCount + i;
                            paneUnit.UnitId = CurrentUnits[unitIndex].Id;

                            paneUnit.PaneId = pane.Id.Equals(_defaultIncremId)
                                ? ProgressiveIncremId(paneIndex)
                                : pane.Id;

                            await paneUnit.PopulateCreationAsync(Clock, cancellationToken).ConfigureAwait();

                            await Accessor.PaneUnits.AddAsync(paneUnit, cancellationToken).ConfigureAwait();
                        }

                        paneIndex++;
                    }

                    return CurrentPanes;
                },
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                },
                cancellationToken);

            // GetParentId
            TIncremId GetParentId(string parentName)
            {
                if (parentName.IsEmpty())
                    return default;

                var pane = Accessor.Panes.FirstOrDefault(p => p.Name == parentName);
                return pane.IsNotNull() ? pane.Id : default;
            }
        }

    }
}
