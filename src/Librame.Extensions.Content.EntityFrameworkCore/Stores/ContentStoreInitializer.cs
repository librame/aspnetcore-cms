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

namespace Librame.Extensions.Content.Stores
{
    using Content.Accessors;
    using Content.Options;
    using Data.Accessors;
    using Data.Stores;
    using Data.Validators;

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
        where TAccessor : class, IAccessor
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
        /// <summary>
        /// 构造一个内容存储初始化器。
        /// </summary>
        /// <param name="initializationOptions">给定的 <see cref="ContentStoreInitializationOptions"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected ContentStoreInitializer(ContentStoreInitializationOptions initializationOptions,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(initializationOptions, identifierGenerator, validator, loggerFactory)
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
        private readonly TIncremId _defaultIncremId = default;


        /// <summary>
        /// 构造一个内容存储初始化器。
        /// </summary>
        /// <param name="initializationOptions">给定的 <see cref="ContentStoreInitializationOptions"/>。</param>
        /// <param name="identityGenerator">给定的 <see cref="IStoreIdentityGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected ContentStoreInitializer(ContentStoreInitializationOptions initializationOptions,
            IStoreIdentityGenerator identityGenerator,
            IDataInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(identityGenerator, validator, loggerFactory)
        {
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
        /// <value>返回 <see cref="IContentStoreIdentityGenerator{TGenId}"/>。</value>
        protected IContentStoreIdentityGenerator<TGenId> ContentIdentityGenerator
            => IdentityGenerator as IContentStoreIdentityGenerator<TGenId>;


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
        {

        }

        /// <summary>
        /// 将生成式标识发表为查询参数值。
        /// </summary>
        /// <param name="id">给定的 <typeparamref name="TGenId"/>。</param>
        /// <param name="createdTime">给定的创建时间。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string PublishedAsQueryValue(TGenId id, DateTimeOffset createdTime)
        {

        }


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
        /// 初始化声明集合。
        /// </summary>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
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
        /// 初始化标签集合。
        /// </summary>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
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
        /// 初始化单元集合。
        /// </summary>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeUnits()
        {
            var tagIndex = 0;
            var tag = CurrentTags[tagIndex];

            var categoryIndex = 0;
            foreach (var category in CurrentCategories)
            {
                var claimIndex = 0;
                foreach (var claim in CurrentClaims)
                {
                    var title = $"测试{category.Name}{claim.Name}标题";

                    if (!TryGetUnit(category.Id, title, out var unit))
                    {
                        unit = typeof(TUnit).EnsureCreate<TUnit>();

                        unit.Id = ContentIdentityGenerator.GenerateUnitIdAsync().ConfigureAndResult();

                        unit.CategoryId = category.Id.Equals(_defaultIncremId)
                            ? ProgressiveIncremId(categoryIndex)
                            : category.Id;
                        
                        unit.Title = title;
                        unit.Subtitle = $"测试{category.Name}{claim.Name}副标题";
                        unit.Tags = $"测试,{category.Name}";

                        unit.PopulateCreationAsync(Clock).ConfigureAndResult();

                        //unit.Reference = "/Reference";
                        unit.PublishedAs = $"/unit/{PublishedAsQueryValue(unit.Id, unit.CreatedTime)}";

                        contentStores.TryCreate(unit);

                        // 添加正文声明
                        var unitClaim = typeof(TUnitClaim).EnsureCreate<TUnitClaim>();

                        unitClaim.UnitId = unit.Id;

                        unitClaim.ClaimId = claim.Id.Equals(_defaultIncremId)
                            ? ProgressiveIncremId(claimIndex)
                            : claim.Id;

                        unitClaim.ClaimValue = $"测试{category.Name}{claim.Name}内容1。";

                        unitClaim.PopulateCreationAsync(Clock).ConfigureAndResult();

                        contentStores.TryCreate(unitClaim);

                        // 添加标签
                        var unitTag = typeof(TUnitTag).EnsureCreate<TUnitTag>();

                        unitTag.UnitId = unit.Id;

                        unitTag.TagId = tag.Id.Equals(_defaultIncremId)
                            ? ProgressiveIncremId(tagIndex)
                            : tag.Id;

                        contentStores.TryCreate(unitTag);

                        // 添加访问计数
                        var unitVisitCount = typeof(TUnitVisitCount).EnsureCreate<TUnitVisitCount>();

                        unitVisitCount.UnitId = unit.Id;

                        contentStores.TryCreate(unitVisitCount);

                        if (!RequiredSaveChanges)
                            RequiredSaveChanges = true;
                    }

                    CurrentUnits.Add(unit);

                    claimIndex++;
                }

                categoryIndex++;
            }

            // TryGetUnit
            bool TryGetUnit(TIncremId categoryId, string title, out TUnit unit)
            {
                unit = contentStores.Units.FirstOrDefault(p => p.CategoryId.Equals(categoryId) && p.Title == title);
                return unit.IsNotNull();
            }
        }


        /// <summary>
        /// 初始化窗格集合。
        /// </summary>
        protected virtual void InitializePanes()
        {
            var defaultPanes = InitializationOptions.DefaultPanes;
            var allUnitIds = CurrentUnits.Select(s => s.Id).ToList();
            var offset = allUnitIds.Count / defaultPanes.Count;

            var paneIndex = 0;
            foreach (var pair in defaultPanes)
            {
                if (!TryGetPane(pair.Key, out var pane))
                {
                    (string parentName, string description) = pair.Value;

                    pane = typeof(TPane).EnsureCreate<TPane>();

                    pane.Name = pair.Key;
                    pane.ParentId = GetParentId(parentName);
                    pane.Description = description;

                    pane.PopulateCreationAsync(Clock);

                    contentStores.TryCreate(pane);

                    // 添加窗格单元
                    var unitIds = paneIndex < offset - 1 ? allUnitIds.Take(offset) : allUnitIds.Skip(offset);
                    foreach (var unitId in unitIds)
                    {
                        var paneUnit = typeof(TPaneUnit).EnsureCreate<TPaneUnit>();

                        paneUnit.UnitId = unitId;

                        paneUnit.PaneId = pane.Id.Equals(_defaultIncremId)
                            ? ProgressiveIncremId(paneIndex)
                            : pane.Id;

                        paneUnit.PopulateCreationAsync(Clock);

                        contentStores.TryCreate(paneUnit);
                    }
                    
                    RequiredSaveChanges = true;
                }

                CurrentPanes.Add(pane);
                paneIndex++;
            }

            // TryGetPane
            bool TryGetPane(string paneName, out TPane pane)
            {
                pane = contentStores.Panes.FirstOrDefault(p => p.Name == paneName);
                return pane.IsNotNull();
            }

            // GetParentId
            TIncremId GetParentId(string parentName)
                => parentName.IsNotEmpty() && TryGetPane(parentName, out var pane) ? pane.ParentId : default;
        }

    }
}
