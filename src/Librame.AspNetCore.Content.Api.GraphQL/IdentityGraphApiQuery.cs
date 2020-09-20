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
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Librame.AspNetCore.Content.Api
{
    using AspNetCore.Api;
    using AspNetCore.Content.Api.Models;
    using AspNetCore.Content.Api.Types;
    using Extensions;
    using Extensions.Content.Accessors;
    using Extensions.Content.Builders;
    using Extensions.Content.Stores;
    using Extensions.Data.Accessors;
    using Extensions.Data.Collections;

    /// <summary>
    /// 内容图形 API 查询。
    /// </summary>
    /// <typeparam name="TCategory">指定的类别类型。</typeparam>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    /// <typeparam name="TClaim">指定的声明类型。</typeparam>
    /// <typeparam name="TTag">指定的标签类型。</typeparam>
    /// <typeparam name="TUnit">指定的单元类型。</typeparam>
    /// <typeparam name="TUnitClaim">指定的单元声明类型。</typeparam>
    /// <typeparam name="TUnitTag">指定的单元标签类型。</typeparam>
    /// <typeparam name="TUnitVisitCount">指定的单元访问计数类型。</typeparam>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TPaneClaim">指定的窗格声明类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public class ContentGraphApiQuery<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim, TGenId, TIncremId, TPublishedBy>
        : GraphApiQueryBase
        where TCategory : ContentCategory<TIncremId, TPublishedBy>
        where TSource : ContentSource<TIncremId, TPublishedBy>
        where TClaim : ContentClaim<TIncremId, TIncremId, TPublishedBy>
        where TTag : ContentTag<TIncremId, TPublishedBy>
        where TUnit : ContentUnit<TGenId, TIncremId, TIncremId, TIncremId, TPublishedBy>
        where TUnitClaim : ContentUnitClaim<TIncremId, TGenId, TIncremId, TPublishedBy>
        where TUnitTag : ContentUnitTag<TIncremId, TGenId, TIncremId>
        where TUnitVisitCount : ContentUnitVisitCount<TGenId>
        where TPane : ContentPane<TIncremId, TPublishedBy>
        where TPaneClaim : ContentPaneClaim<TIncremId, TIncremId, TIncremId, TPublishedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TPublishedBy : IEquatable<TPublishedBy>
    {
        /// <summary>
        /// 构造一个内容图形 API 查询。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        /// <param name="dependency">给定的 <see cref="ContentBuilderDependency"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public ContentGraphApiQuery(IAccessor accessor, ContentBuilderDependency dependency,
            ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            ContentAccessor = accessor.CastTo<IAccessor,
                IContentAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim>>(nameof(accessor));

            Dependency = dependency.NotNull(nameof(dependency));

            AddCategoryTypeFields();
            AddSourceTypeFields();
            AddClaimTypeFields();
            AddTagTypeFields();
            AddUnitTypeFields();
            AddPaneTypeFields();
            AddPaneUnitTypeFields();
        }


        /// <summary>
        /// 身份访问器。
        /// </summary>
        protected IContentAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim> ContentAccessor { get; }

        /// <summary>
        /// 构建器依赖。
        /// </summary>
        protected ContentBuilderDependency Dependency { get; }


        private void AddCategoryTypeFields()
        {
            // { pageCategories(index: 1, size: 10, search: "") { id name description createdTime createdBy [parent{...} }] }
            Field<ListGraphType<CategoryType>>
            (
                name: "pageCategories",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "index" },
                    new QueryArgument<IntGraphType> { Name = "size" },
                    new QueryArgument<StringGraphType> { Name = "search" }
                ),
                resolve: context =>
                {
                    var index = context.GetArgument<int>("index");
                    var size = context.GetArgument<int>("size");
                    var search = context.GetArgument<string>("search");

                    var query = ContentAccessor.Categories.AsQueryable();

                    if (search.IsNotEmpty())
                        query = query.Where(p => p.Name.Contains(search, StringComparison.InvariantCulture));

                    if (index > 0 && size > 0)
                    {
                        return query.AsPagingByIndex(ordered =>
                        {
                            return ordered.OrderBy(k => k.CreatedTimeTicks);
                        },
                        index, size).SelectPaging(category =>
                        {
                            return category.ToModel<TCategory, TIncremId, TPublishedBy>(parentId =>
                            {
                                return ContentAccessor.Categories.Find(parentId);
                            });
                        });
                    }

                    return query.ToList().Select(category =>
                    {
                        return category.ToModel<TCategory, TIncremId, TPublishedBy>(parentId =>
                        {
                            return ContentAccessor.Categories.Find(parentId);
                        });
                    });
                }
            );

            // { categories(parentId) { id name description createdTime createdBy [parent{...}] } }
            Field<ListGraphType<CategoryType>>
            (
                name: "categories",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "parentId" }
                ),
                resolve: context =>
                {
                    var parentId = context.GetArgument<TIncremId>("parentId");

                    return ContentAccessor.Categories
                        .Where(p => p.ParentId.Equals(parentId))
                        .ToList()
                        .Select(category =>
                        {
                            return category.ToModel<TCategory, TIncremId, TPublishedBy>(parentId =>
                            {
                                return ContentAccessor.Categories.Find(parentId);
                            });
                        });
                }
            );

            // { categoryId(id: "") { id name description createdTime createdBy [parent{...}] } }
            Field<CategoryType>
            (
                name: "categoryId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TIncremId>("id");
                    var category = ContentAccessor.Categories.Find(id);

                    return category.ToModel<TCategory, TIncremId, TPublishedBy>(parentId =>
                    {
                        return ContentAccessor.Categories.Find(parentId);
                    });
                }
            );

            // { categoryName(name: "") { id name description createdTime createdBy [parent{...}] } }
            Field<CategoryType>
            (
                name: "categoryName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    var category = ContentAccessor.Categories.FirstOrDefault(p => p.Name == name);

                    return category.ToModel<TCategory, TIncremId, TPublishedBy>(parentId =>
                    {
                        return ContentAccessor.Categories.Find(parentId);
                    });
                }
            );
        }

        private void AddSourceTypeFields()
        {
            // { pageSources(index: 1, size: 10, trace: false, search: "") { id name description website weblogo createdTime createdBy [parent{...}] } }
            Field<ListGraphType<SourceType>>
            (
                name: "pageSources",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "index" },
                    new QueryArgument<IntGraphType> { Name = "size" },
                    new QueryArgument<BooleanGraphType> { Name = "trace" },
                    new QueryArgument<StringGraphType> { Name = "search" }
                ),
                resolve: context =>
                {
                    var index = context.GetArgument<int>("index");
                    var size = context.GetArgument<int>("size");
                    var trace = context.GetArgument<bool>("trace");
                    var search = context.GetArgument<string>("search");

                    var query = ContentAccessor.Sources.AsQueryable();

                    if (search.IsNotEmpty())
                        query = query.Where(p => p.Name.Contains(search, StringComparison.InvariantCulture));

                    if (index > 0 && size > 0)
                    {
                        return query.AsPagingByIndex(ordered =>
                        {
                            return ordered.OrderBy(k => k.CreatedTimeTicks);
                        },
                        index, size).SelectPaging(s => ToSourceModel(s, trace));
                    }

                    return query.ToList().Select(s => ToSourceModel(s, trace));
                }
            );

            // { sources(parentId, trace: false) { id name description website weblogo createdTime createdBy [parent{...}] } }
            Field<ListGraphType<SourceType>>
            (
                name: "sources",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "parentId" },
                    new QueryArgument<BooleanGraphType> { Name = "trace" }
                ),
                resolve: context =>
                {
                    var parentId = context.GetArgument<TIncremId>("parentId");
                    var trace = context.GetArgument<bool>("trace");

                    return ContentAccessor.Sources
                        .Where(p => p.ParentId.Equals(parentId))
                        .ToList()
                        .Select(s => ToSourceModel(s, trace));
                }
            );

            // { sourceId(id: "", trace: false) { id name description website weblogo createdTime createdBy [parent{...}] } }
            Field<SourceType>
            (
                name: "sourceId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" },
                    new QueryArgument<BooleanGraphType> { Name = "trace" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TIncremId>("id");
                    var trace = context.GetArgument<bool>("trace");

                    var source = ContentAccessor.Sources.Find(id);

                    return ToSourceModel(source, trace);
                }
            );

            // { sourceName(name: "", trace: false) { id name description website weblogo createdTime createdBy [parent{...}] } }
            Field<SourceType>
            (
                name: "sourceName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<BooleanGraphType> { Name = "trace" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    var trace = context.GetArgument<bool>("trace");

                    var source = ContentAccessor.Sources.FirstOrDefault(p => p.Name == name);

                    return ToSourceModel(source, trace);
                }
            );
        }

        private void AddClaimTypeFields()
        {
            // { pageClaims(index: 1, size: 10, includeCategory: false, search: "") { id name description createdTime createdBy [category{...}] } }
            Field<ListGraphType<ClaimType>>
            (
                name: "pageClaims",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "index" },
                    new QueryArgument<IntGraphType> { Name = "size" },
                    new QueryArgument<BooleanGraphType> { Name = "includeCategory" },
                    new QueryArgument<StringGraphType> { Name = "search" }
                ),
                resolve: context =>
                {
                    var index = context.GetArgument<int>("index");
                    var size = context.GetArgument<int>("size");
                    var includeCategory = context.GetArgument<bool>("includeCategory");
                    var search = context.GetArgument<string>("search");

                    var query = ContentAccessor.Claims.AsQueryable();

                    if (search.IsNotEmpty())
                        query = query.Where(p => p.Name.Contains(search, StringComparison.InvariantCulture));

                    if (index > 0 && size > 0)
                    {
                        var claims = query.AsPagingByIndex(ordered =>
                        {
                            return ordered.OrderBy(k => k.CreatedTimeTicks);
                        },
                        index, size);

                        return ToClaimModels(claims, includeCategory);
                    }

                    return ToClaimModels(query.ToList(), includeCategory);
                }
            );

            // { claims(categoryId, includeCategory: false) { id name description createdTime createdBy [category{...}] } }
            Field<ListGraphType<ClaimType>>
            (
                name: "claims",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "categoryId" }
                ),
                resolve: context =>
                {
                    var categoryId = context.GetArgument<TIncremId>("categoryId");
                    var includeCategory = context.GetArgument<bool>("includeCategory");

                    var claims = ContentAccessor.Claims
                        .Where(p => p.CategoryId.Equals(categoryId))
                        .ToList();

                    return ToClaimModels(claims, includeCategory);
                }
            );

            // { claimId(id: "", includeCategory: false) { id name description createdTime createdBy [category{...}] } }
            Field<ClaimType>
            (
                name: "claimId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TIncremId>("id");
                    var includeCategory = context.GetArgument<bool>("includeCategory");

                    var claim = ContentAccessor.Claims.Find(id);

                    return ToClaimModel(claim, includeCategory);
                }
            );

            // { claimName(name: "", includeCategory: false) { id name description createdTime createdBy [category{...}] } }
            Field<ClaimType>
            (
                name: "claimName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    var includeCategory = context.GetArgument<bool>("includeCategory");

                    var claim = ContentAccessor.Claims.FirstOrDefault(p => p.Name == name);

                    return ToClaimModel(claim, includeCategory);
                }
            );
        }

        private void AddTagTypeFields()
        {
            // { pageTags(index: 1, size: 10, search: "") { id name createdTime createdBy } }
            Field<ListGraphType<TagType>>
            (
                name: "pageTags",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "index" },
                    new QueryArgument<IntGraphType> { Name = "size" },
                    new QueryArgument<StringGraphType> { Name = "search" }
                ),
                resolve: context =>
                {
                    var index = context.GetArgument<int>("index");
                    var size = context.GetArgument<int>("size");
                    var search = context.GetArgument<string>("search");

                    var query = ContentAccessor.Tags.AsQueryable();

                    if (search.IsNotEmpty())
                        query = query.Where(p => p.Name.Contains(search, StringComparison.InvariantCulture));

                    if (index > 0 && size > 0)
                    {
                        return query.AsPagingByIndex(ordered =>
                        {
                            return ordered.OrderBy(k => k.CreatedTimeTicks);
                        },
                        index, size).SelectPaging(s => s.ToModel());
                    }

                    return query.ToList().Select(s => s.ToModel());
                }
            );

            // { tagId(id: "") { id name createdTime createdBy } }
            Field<TagType>
            (
                name: "tagId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TIncremId>("id");
                    return ContentAccessor.Tags.Find(id).ToModel();
                }
            );

            // { tagName(name: "") { id name createdTime createdBy } }
            Field<TagType>
            (
                name: "tagName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    return ContentAccessor.Tags.FirstOrDefault(p => p.Name == name).ToModel();
                }
            );
        }

        private void AddUnitTypeFields()
        {
            // { pageUnits(index: 1, size: 10, includeVisitCount: false, search: "") { id title subtitle tags reference publishedAs publishedTime publishedBy createdTime createdBy [unitVisitCount{...}] } }
            Field<ListGraphType<UnitType>>
            (
                name: "pageUnits",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "index" },
                    new QueryArgument<IntGraphType> { Name = "size" },
                    new QueryArgument<BooleanGraphType> { Name = "includeVisitCount" },
                    new QueryArgument<StringGraphType> { Name = "search" }
                ),
                resolve: context =>
                {
                    var index = context.GetArgument<int>("index");
                    var size = context.GetArgument<int>("size");
                    var includeVisitCount = context.GetArgument<bool>("includeVisitCount");
                    var search = context.GetArgument<string>("search");

                    var query = ContentAccessor.Units.AsQueryable();

                    if (search.IsNotEmpty())
                        query = query.Where(p => p.Title.Contains(search, StringComparison.InvariantCulture));

                    if (index > 0 && size > 0)
                    {
                        return query.AsPagingByIndex(ordered =>
                        {
                            return ordered.OrderBy(k => k.CreatedTimeTicks);
                        },
                        index, size).SelectPaging(s => ToUnitModel(s, includeVisitCount));
                    }

                    return query.ToList().Select(s => ToUnitModel(s, includeVisitCount));
                }
            );

            // { unitId(id: "", includeCategory: false, includeSource: false, includeVisitCount: false, includeClaims: false, includeTags: false) { id title subtitle tags reference publishedAs publishedTime publishedBy createdTime createdBy [category{...}] [source{...}] [unitVisitCount{...}] [unitClaims[{...}]] [unitTags[{...}]] } }
            Field<UnitType>
            (
                name: "unitId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" },
                    new QueryArgument<BooleanGraphType> { Name = "includeCategory" },
                    new QueryArgument<BooleanGraphType> { Name = "includeSource" },
                    new QueryArgument<BooleanGraphType> { Name = "includeVisitCount" },
                    new QueryArgument<BooleanGraphType> { Name = "includeClaims" },
                    new QueryArgument<BooleanGraphType> { Name = "includeTags" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TIncremId>("id");
                    var includeCategory = context.GetArgument<bool>("includeCategory");
                    var includeSource = context.GetArgument<bool>("includeSource");
                    var includeVisitCount = context.GetArgument<bool>("includeVisitCount");
                    var includeClaims = context.GetArgument<bool>("includeClaims");
                    var includeTags = context.GetArgument<bool>("includeTags");

                    var unit = ContentAccessor.Units.Find(id);

                    return ToUnitModel(unit, includeCategory, includeSource, includeVisitCount, includeClaims, includeTags);
                }
            );

            // { unitTitle(title: "", includeCategory: false, includeSource: false, includeVisitCount: false, includeClaims: false, includeTags: false) { id title subtitle tags reference publishedAs publishedTime publishedBy createdTime createdBy [category{...}] [source{...}] [unitVisitCount{...}] [unitClaims[{...}]] [unitTags[{...}]] } }
            Field<UnitType>
            (
                name: "unitTitle",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "title" },
                    new QueryArgument<BooleanGraphType> { Name = "includeCategory" },
                    new QueryArgument<BooleanGraphType> { Name = "includeSource" },
                    new QueryArgument<BooleanGraphType> { Name = "includeVisitCount" },
                    new QueryArgument<BooleanGraphType> { Name = "includeClaims" },
                    new QueryArgument<BooleanGraphType> { Name = "includeTags" }
                ),
                resolve: context =>
                {
                    var title = context.GetArgument<string>("title");
                    var includeCategory = context.GetArgument<bool>("includeCategory");
                    var includeSource = context.GetArgument<bool>("includeSource");
                    var includeVisitCount = context.GetArgument<bool>("includeVisitCount");
                    var includeClaims = context.GetArgument<bool>("includeClaims");
                    var includeTags = context.GetArgument<bool>("includeTags");

                    var unit = ContentAccessor.Units.FirstOrDefault(p => p.Title == title);

                    return ToUnitModel(unit, includeCategory, includeSource, includeVisitCount, includeClaims, includeTags);
                }
            );
        }

        private void AddPaneTypeFields()
        {
            // { pagePanes(index: 1, size: 10, trace: false, includeClaims: false, search: "") { id name description icon more createdTime createdBy [parent{...}] [paneClaims[{...}]] } }
            Field<ListGraphType<PaneType>>
            (
                name: "pagePanes",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "index" },
                    new QueryArgument<IntGraphType> { Name = "size" },
                    new QueryArgument<BooleanGraphType> { Name = "trace" },
                    new QueryArgument<BooleanGraphType> { Name = "includeClaims" },
                    new QueryArgument<StringGraphType> { Name = "search" }
                ),
                resolve: context =>
                {
                    var index = context.GetArgument<int>("index");
                    var size = context.GetArgument<int>("size");
                    var trace = context.GetArgument<bool>("trace");
                    var includeClaims = context.GetArgument<bool>("includeClaims");
                    var search = context.GetArgument<string>("search");

                    var query = ContentAccessor.Panes.AsQueryable();

                    if (search.IsNotEmpty())
                        query = query.Where(p => p.Name.Contains(search, StringComparison.InvariantCulture));

                    if (index > 0 && size > 0)
                    {
                        return query.AsPagingByIndex(ordered =>
                        {
                            return ordered.OrderBy(k => k.CreatedTimeTicks);
                        },
                        index, size).SelectPaging(s => ToPaneModel(s, trace, includeClaims));
                    }

                    return query.ToList().Select(s => ToPaneModel(s, trace, includeClaims));
                }
            );

            // { panes(parentId, trace: false, includeClaims: false) { id name description icon more createdTime createdBy [parent{...}] [paneClaims[{...}]] } }
            Field<ListGraphType<PaneType>>
            (
                name: "panes",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "parentId" },
                    new QueryArgument<BooleanGraphType> { Name = "trace" },
                    new QueryArgument<BooleanGraphType> { Name = "includeClaims" }
                ),
                resolve: context =>
                {
                    var parentId = context.GetArgument<TIncremId>("parentId");
                    var trace = context.GetArgument<bool>("trace");
                    var includeClaims = context.GetArgument<bool>("includeClaims");

                    return ContentAccessor.Panes
                        .Where(p => p.ParentId.Equals(parentId))
                        .ToList().Select(s => ToPaneModel(s, trace, includeClaims));
                }
            );

            // { paneId(id: "", trace: false, includeClaims: false) { id name description icon more createdTime createdBy [parent{...}] [paneClaims[{...}]] } }
            Field<PaneType>
            (
                name: "paneId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" },
                    new QueryArgument<BooleanGraphType> { Name = "trace" },
                    new QueryArgument<BooleanGraphType> { Name = "includeClaims" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TIncremId>("id");
                    var trace = context.GetArgument<bool>("trace");
                    var includeClaims = context.GetArgument<bool>("includeClaims");

                    var pane = ContentAccessor.Panes.FirstOrDefault(p => p.Id.Equals(id));

                    return ToPaneModel(pane, trace, includeClaims);
                }
            );

            // { paneName(name: "", trace: false, includeClaims: false) { id name description icon more createdTime createdBy [parent{...}] [paneClaims[{...}]] } }
            Field<PaneType>
            (
                name: "paneName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<BooleanGraphType> { Name = "trace" },
                    new QueryArgument<BooleanGraphType> { Name = "includeClaims" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    var trace = context.GetArgument<bool>("trace");
                    var includeClaims = context.GetArgument<bool>("includeClaims");

                    var pane = ContentAccessor.Panes.FirstOrDefault(p => p.Name == name);

                    return ToPaneModel(pane, trace, includeClaims);
                }
            );
        }

        private void AddPaneUnitTypeFields()
        {
            // { index(names: null/"快讯,焦点...") { pane{...} units[{...}] } }
            Field<ListGraphType<PaneUnitType>>
            (
                name: "index",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "names" }
                ),
                resolve: context =>
                {
                    var trace = context.GetArgument<bool>("trace");
                    var includeClaims = context.GetArgument<bool>("includeClaims");

                    var panes = new List<TPane>();

                    var names = context.GetArgument<string>("names");
                    if (names.IsNotEmpty())
                    {
                        if (names.Contains(',', StringComparison.InvariantCulture))
                        {
                            var items = names.Split(',')
                                .Select(s => ContentAccessor.Panes.FirstOrDefault(p => p.Name == s))
                                .ToList();

                            panes.AddRange(items);
                        }
                        else
                        {
                            var item = ContentAccessor.Panes.FirstOrDefault(p => p.Name == names);
                            panes.Add(item);
                        }
                    }

                    return panes.Select(s => ToPaneUnitModel(s));
                }
            );
        }


        /// <summary>
        /// 转为来源模型（支持对父级进行追溯）。
        /// </summary>
        /// <param name="source">给定的 <typeparamref name="TSource"/>。</param>
        /// <param name="trace">是否对父级进行追溯。</param>
        /// <returns>返回 <see cref="SourceModel"/>。</returns>
        protected virtual SourceModel ToSourceModel(TSource source, bool trace)
        {
            if (source.IsNull())
                return null;

            return LoadParent(source.ToModel(), source.ParentId);

            // LoadParent
            SourceModel LoadParent(SourceModel model, TIncremId parentId)
            {
                if (!parentId.Equals(default))
                {
                    var parent = ContentAccessor.Sources.Find(parentId);
                    if (parent.IsNotNull())
                    {
                        model.Parent = parent.ToModel();

                        if (trace)
                            LoadParent(model.Parent, parent.ParentId);
                    }
                }

                return model;
            }
        }


        /// <summary>
        /// 转为声明模型集合（支持包含类别）。
        /// </summary>
        /// <param name="claims">给定的 <see cref="IPageable{TClaim}"/>。</param>
        /// <param name="includeCategory">包含类别模型。</param>
        /// <returns>返回 <see cref="IPageable{ClaimModel}"/>。</returns>
        protected virtual IPageable<ClaimModel> ToClaimModels(IPageable<TClaim> claims, bool includeCategory)
        {
            if (claims.IsNull())
                return null;

            Dictionary<TIncremId, TCategory> categories = null;
            if (includeCategory)
            {
                categories = claims.Select(s => s.CategoryId).Distinct()
                    .Select(s => ContentAccessor.Categories.Find(s))
                    .ToDictionary(k => k.Id, es => es);
            }

            return claims.SelectPaging(s =>
            {
                var model = s.ToModel();
                model.Category = categories?[s.CategoryId].ToModel();

                return model;
            });
        }

        /// <summary>
        /// 转为声明模型集合（支持包含类别）。
        /// </summary>
        /// <param name="claims">给定的 <see cref="IEnumerable{TClaim}"/>。</param>
        /// <param name="includeCategory">包含类别模型。</param>
        /// <returns>返回 <see cref="IEnumerable{ClaimModel}"/>。</returns>
        protected virtual IEnumerable<ClaimModel> ToClaimModels(IEnumerable<TClaim> claims, bool includeCategory)
        {
            if (claims.IsNull())
                return null;

            Dictionary<TIncremId, TCategory> categories = null;
            if (includeCategory)
            {
                categories = claims.Select(s => s.CategoryId).Distinct()
                    .Select(s => ContentAccessor.Categories.Find(s))
                    .ToDictionary(k => k.Id, es => es);
            }

            return claims.Select(s =>
            {
                var model = s.ToModel();

                return model;
            });
        }

        /// <summary>
        /// 转为声明模型（支持包含类别）。
        /// </summary>
        /// <param name="claim">给定的 <typeparamref name="TClaim"/>。</param>
        /// <param name="includeCategory">包含类别模型。</param>
        /// <returns>返回 <see cref="ClaimModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        protected virtual ClaimModel ToClaimModel(TClaim claim, bool includeCategory)
        {
            var model = claim.ToModel();

            if (model.IsNotNull() && includeCategory)
                model.Category = ContentAccessor.Categories.Find(claim.CategoryId).ToModel();

            return model;
        }


        /// <summary>
        /// 转为单元模型（支持包含访问计数）。
        /// </summary>
        /// <param name="unit">给定的 <typeparamref name="TUnit"/>。</param>
        /// <param name="includeVisitCount">包含访问计数模型。</param>
        /// <returns>返回 <see cref="UnitModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        protected virtual UnitModel ToUnitModel(TUnit unit, bool includeVisitCount)
        {
            var model = unit.ToModel();

            if (model.IsNotNull() && includeVisitCount)
                model.UnitVisitCount = ContentAccessor.UnitVisitCounts.Find(unit.Id).ToModel();

            return model;
        }

        /// <summary>
        /// 转为单元模型（支持包含类别、来源、访问计数、声明、标签等模型）。
        /// </summary>
        /// <param name="unit">给定的 <typeparamref name="TUnit"/>。</param>
        /// <param name="includeCategory">包含类别模型。</param>
        /// <param name="includeSource">包含来源模型。</param>
        /// <param name="includeVisitCount">包含访问计数模型集合。</param>
        /// <param name="includeClaims">包含声明模型集合。</param>
        /// <param name="includeTags">包含标签模型集合。</param>
        /// <returns>返回 <see cref="UnitModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        protected virtual UnitModel ToUnitModel(TUnit unit, bool includeCategory,
            bool includeSource, bool includeVisitCount, bool includeClaims, bool includeTags)
        {
            var model = unit.ToModel();
            if (model.IsNull())
                return null;

            if (includeCategory)
                model.Category = ContentAccessor.Categories.Find(unit.CategoryId).ToModel();

            if (includeSource)
                model.Source = ContentAccessor.Sources.Find(unit.SourceId).ToModel();

            if (includeVisitCount)
                model.UnitVisitCount = ContentAccessor.UnitVisitCounts.Find(unit.Id).ToModel();

            if (includeClaims)
            {
                var unitClaims = ContentAccessor.UnitClaims
                    .Where(p => p.UnitId.Equals(unit.Id))
                    .ToList();

                if (unitClaims.IsNotEmpty())
                {
                    var claims = unitClaims.Select(s => s.ClaimId).Distinct()
                        .Select(s => ContentAccessor.Claims.Find(s))
                        .ToDictionary(ks => ks.Id, es => es.ToModel());

                    model.UnitClaims = unitClaims
                        .Select(s => s.ToModel(claim: claims[s.ClaimId]))
                        .ToList();
                }
            }

            if (includeTags)
            {
                var unitTags = ContentAccessor.UnitTags
                    .Where(p => p.UnitId.Equals(unit.Id))
                    .ToList();

                if (unitTags.IsNotEmpty())
                {
                    var tags = unitTags.Select(s => s.TagId).Distinct()
                    .Select(s => ContentAccessor.Tags.Find(s))
                    .ToDictionary(ks => ks.Id, es => es.ToModel());

                    model.UnitTags = unitTags
                        .Select(s => s.ToModel(tags[s.TagId]))
                        .ToList();
                }
            }

            return model;
        }


        /// <summary>
        /// 转为窗格模型（支持对父级进行追溯）。
        /// </summary>
        /// <param name="pane">给定的 <typeparamref name="TPane"/>。</param>
        /// <param name="trace">是否对父级进行追溯。</param>
        /// <param name="includeClaims">包含声明模型集合。</param>
        /// <returns>返回 <see cref="PaneModel"/>。</returns>
        protected virtual PaneModel ToPaneModel(TPane pane, bool trace, bool includeClaims)
        {
            if (pane.IsNull())
                return null;

            return LoadParent(pane.ToModel(), pane.ParentId);

            // LoadParent
            PaneModel LoadParent(PaneModel model, TIncremId parentId)
            {
                if (!parentId.Equals(default))
                {
                    var parent = ContentAccessor.Panes.Find(parentId);
                    if (parent.IsNotNull())
                    {
                        model.Parent = parent.ToModel();

                        if (trace)
                            LoadParent(model.Parent, parent.ParentId);
                    }
                }

                if (includeClaims)
                {
                    var paneClaims = ContentAccessor.PaneClaims
                        .Where(p => p.PaneId.Equals(pane.Id))
                        .ToList();

                    if (paneClaims.IsNotEmpty())
                    {
                        var claims = paneClaims.Select(s => s.ClaimId).Distinct()
                            .Select(s => ContentAccessor.Claims.Find(s))
                            .ToDictionary(ks => ks.Id, es => es.ToModel());

                        model.PaneClaims = paneClaims
                            .Select(s => s.ToModel(claim: claims[s.ClaimId]))
                            .ToList();
                    }
                }

                return model;
            }
        }

        /// <summary>
        /// 转为窗格单元模型（支持对父级进行追溯）。
        /// </summary>
        /// <param name="pane">给定的 <typeparamref name="TPane"/>。</param>
        /// <returns>返回 <see cref="PaneUnitModel"/>。</returns>
        protected virtual PaneUnitModel ToPaneUnitModel(TPane pane)
        {
            var paneModel = ToPaneModel(pane, trace: false, includeClaims: true);
            if (paneModel.IsNull())
                return null;

            var query = ContentAccessor.Units.AsQueryable()
                .Where(p => p.PaneId.Equals(pane.Id));

            var totalClaim = paneModel.PaneClaims[paneModel.PaneClaims.Count - 1];
            if (totalClaim.IsNotNull())
                query = query.Take(int.Parse(totalClaim.ClaimValue, CultureInfo.InvariantCulture));

            return new PaneUnitModel
            {
                Pane = paneModel,
                Units = query.ToList().Select(s => ToUnitModel(s, includeVisitCount: true)).ToList()
            };
        }

    }
}
