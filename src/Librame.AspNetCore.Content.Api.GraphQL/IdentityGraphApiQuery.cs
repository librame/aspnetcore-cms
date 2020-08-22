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
using System.Linq;

namespace Librame.AspNetCore.Content.Api
{
    using AspNetCore.Api;
    using AspNetCore.Content.Api.Models;
    using AspNetCore.Content.Api.Types;
    using Extensions;
    using Extensions.Data.Collections;
    using Extensions.Content.Accessors;
    using Extensions.Content.Builders;
    using Extensions.Content.Stores;
    using Extensions.Data.Accessors;
    using System.Collections.Generic;

    /// <summary>
    /// 内容图形 API 查询。
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
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
    public class ContentGraphApiQuery<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit, TGenId, TIncremId, TPublishedBy>
        : GraphApiQueryBase
        where TCategory : ContentCategory<TIncremId, TPublishedBy>
        where TSource : ContentSource<TIncremId, TPublishedBy>
        where TClaim : ContentClaim<TIncremId, TIncremId, TPublishedBy>
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
        /// <summary>
        /// 构造一个内容图形 API 查询。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        /// <param name="dependency">给定的 <see cref="ContentBuilderDependency"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public ContentGraphApiQuery(IAccessor accessor,
            ContentBuilderDependency dependency,
            ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            ContentAccessor = accessor.CastTo<IAccessor,
                IContentAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit>>(nameof(accessor));

            Dependency = dependency.NotNull(nameof(dependency));

            AddCategoryTypeFields();
            AddSourceTypeFields();
            AddClaimTypeFields();
            AddTagTypeFields();
            AddUnitTypeFields();
            AddPaneTypeFields();
        }


        /// <summary>
        /// 构建器依赖。
        /// </summary>
        protected ContentBuilderDependency Dependency { get; }

        /// <summary>
        /// 身份访问器。
        /// </summary>
        protected IContentAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit> ContentAccessor { get; }


        private void AddCategoryTypeFields()
        {
            // { pageCategories(index: 1, size: 10, search: "") { id name description createdTime createdBy } }
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
                        index, size).SelectPaging(s => ToCategoryModelWithParent(s));
                    }

                    return query.ToList().Select(s => ToCategoryModelWithParent(s));
                }
            );

            // { categories(parentId) { id name description createdTime createdBy } }
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
                        .Select(s => ToCategoryModelWithParent(s));
                }
            );

            // { categoryId(id: "") { id name description createdTime createdBy parent{...} } }
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

                    return ToCategoryModelWithParent(category);
                }
            );

            // { categoryName(name: "") { id name description createdTime createdBy parent{...} } }
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

                    return ToCategoryModelWithParent(category);
                }
            );
        }

        private void AddSourceTypeFields()
        {
            // { pageSources(index: 1, size: 10, search: "") { id name description website weblogo createdTime createdBy } }
            Field<ListGraphType<SourceType>>
            (
                name: "pageSources",
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

                    var query = ContentAccessor.Sources.AsQueryable();

                    if (search.IsNotEmpty())
                        query = query.Where(p => p.Name.Contains(search, StringComparison.InvariantCulture));

                    if (index > 0 && size > 0)
                    {
                        return query.AsPagingByIndex(ordered =>
                        {
                            return ordered.OrderBy(k => k.CreatedTimeTicks);
                        },
                        index, size).SelectPaging(s => ToSourceModelWithParent(s));
                    }

                    return query.ToList().Select(s => ToSourceModelWithParent(s));
                }
            );

            // { sources(parentId) { id name description website weblogo createdTime createdBy } }
            Field<ListGraphType<SourceType>>
            (
                name: "sources",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "parentId" }
                ),
                resolve: context =>
                {
                    var parentId = context.GetArgument<TIncremId>("parentId");

                    return ContentAccessor.Sources
                        .Where(p => p.ParentId.Equals(parentId))
                        .ToList()
                        .Select(s => ToSourceModelWithParent(s));
                }
            );

            // { sourceId(id: "") { id name description website weblogo createdTime createdBy parent{...} } }
            Field<SourceType>
            (
                name: "sourceId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TIncremId>("id");
                    var source = ContentAccessor.Sources.Find(id);

                    return ToSourceModelWithParent(source);
                }
            );

            // { sourceName(name: "") { id name description website weblogo createdTime createdBy parent{...} } }
            Field<SourceType>
            (
                name: "sourceName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    var source = ContentAccessor.Sources.FirstOrDefault(p => p.Name == name);

                    return ToSourceModelWithParent(source);
                }
            );
        }

        private void AddClaimTypeFields()
        {
            // { pageClaims(index: 1, size: 10, search: "") { id name description createdTime createdBy category{...} } }
            Field<ListGraphType<ClaimType>>
            (
                name: "pageClaims",
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

                        return ToClaimModels(claims);
                    }

                    return ToClaimModels(query.ToList());
                }
            );

            // { claims(categoryId) { id name description createdTime createdBy category{...} } }
            Field<ListGraphType<ClaimType>>
            (
                name: "claims",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "categoryId" }
                ),
                resolve: context =>
                {
                    var categoryId = context.GetArgument<TIncremId>("categoryId");

                    var claims = ContentAccessor.Claims
                        .Where(p => p.CategoryId.Equals(categoryId))
                        .ToList();

                    return ToClaimModels(claims);
                }
            );

            // { claimId(id: "") { id name description createdTime createdBy category{...} } }
            Field<ClaimType>
            (
                name: "claimId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TIncremId>("id");
                    var claim = ContentAccessor.Claims.Find(id);

                    return ToClaimModel(claim);
                }
            );

            // { claimName(name: "") { id name description createdTime createdBy category{...} } }
            Field<ClaimType>
            (
                name: "claimName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    var claim = ContentAccessor.Claims.FirstOrDefault(p => p.Name == name);

                    return ToClaimModel(claim);
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
            // { pageUnits(index: 1, size: 10, search: "") { id categoryId sourceId title subtitle tags reference publishedAs publishedTime publishedBy createdTime createdBy } }
            Field<ListGraphType<UnitType>>
            (
                name: "pageUnits",
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

                    var query = ContentAccessor.Units.AsQueryable();

                    if (search.IsNotEmpty())
                        query = query.Where(p => p.Title.Contains(search, StringComparison.InvariantCulture));

                    if (index > 0 && size > 0)
                    {
                        return query.AsPagingByIndex(ordered =>
                        {
                            return ordered.OrderBy(k => k.CreatedTimeTicks);
                        },
                        index, size);
                    }

                    return query.ToList();
                }
            );

            // { unitId(id: "") { id categoryId sourceId title subtitle tags reference publishedAs publishedTime publishedBy createdTime createdBy } }
            Field<UnitType>
            (
                name: "unitId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TIncremId>("id");
                    return ContentAccessor.Units.Find(id);
                }
            );

            // { unitName(title: "") { id categoryId sourceId title subtitle tags reference publishedAs publishedTime publishedBy createdTime createdBy } }
            Field<UnitType>
            (
                name: "unitTitle",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "title" }
                ),
                resolve: context =>
                {
                    var title = context.GetArgument<string>("title");
                    return ContentAccessor.Units.FirstOrDefault(p => p.Title == title);
                }
            );
        }

        private void AddPaneTypeFields()
        {
            // { pagePanes(index: 1, size: 10, search: "") { id parentId name description icon more createdTime createdBy } }
            Field<ListGraphType<PaneType<TPane, TIncremId, TPublishedBy>>>
            (
                name: "pagePanes",
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

                    var query = ContentAccessor.Panes.AsQueryable();

                    if (search.IsNotEmpty())
                        query = query.Where(p => p.Name.Contains(search, StringComparison.InvariantCulture));

                    if (index > 0 && size > 0)
                    {
                        return query.AsPagingByIndex(ordered =>
                        {
                            return ordered.OrderBy(k => k.CreatedTimeTicks);
                        },
                        index, size);
                    }

                    return query.ToList();
                }
            );

            // { panes(parentId) { id parentId name description icon more createdTime createdBy } }
            Field<ListGraphType<PaneType<TPane, TIncremId, TPublishedBy>>>
            (
                name: "panes",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "parentId" }
                ),
                resolve: context =>
                {
                    var parentId = context.GetArgument<TIncremId>("parentId");

                    return ContentAccessor.Panes
                        .Where(p => p.ParentId.Equals(parentId))
                        .ToList();
                }
            );

            // { paneId(id: "") { id parentId name description icon more createdTime createdBy } }
            Field<PaneType<TPane, TIncremId, TPublishedBy>>
            (
                name: "paneId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TIncremId>("id");
                    return ContentAccessor.Panes.FirstOrDefault(p => p.Id.Equals(id));
                }
            );

            // { paneName(name: "") { id parentId name description icon more createdTime createdBy } }
            Field<PaneType<TPane, TIncremId, TPublishedBy>>
            (
                name: "paneName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    return ContentAccessor.Panes.FirstOrDefault(p => p.Name == name);
                }
            );
        }


        /// <summary>
        /// 转为带有父级分类的模型。
        /// </summary>
        /// <param name="category">给定的 <typeparamref name="TCategory"/>。</param>
        /// <returns>返回 <see cref="CategoryModel"/>。</returns>
        protected virtual CategoryModel ToCategoryModelWithParent(TCategory category)
        {
            if (category.IsNull())
                return null;

            return LoadParent(category.ToModel(), category.ParentId);

            // LoadParent
            CategoryModel LoadParent(CategoryModel model, TIncremId parentId)
            {
                if (!parentId.Equals(default))
                {
                    var parent = ContentAccessor.Categories.Find(parentId);
                    if (parent.IsNotNull())
                    {
                        model.Parent = parent.ToModel();

                        // 循环加载
                        LoadParent(model.Parent, parent.ParentId);
                    }
                }

                return model;
            }
        }

        /// <summary>
        /// 转为带有父级来源的模型。
        /// </summary>
        /// <param name="source">给定的 <typeparamref name="TSource"/>。</param>
        /// <returns>返回 <see cref="SourceModel"/>。</returns>
        protected virtual SourceModel ToSourceModelWithParent(TSource source)
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

                        // 循环加载
                        LoadParent(model.Parent, parent.ParentId);
                    }
                }

                return model;
            }
        }

        /// <summary>
        /// 尝试加载关联声明集合（支持包含分类集合）。
        /// </summary>
        /// <param name="claims">给定的 <see cref="IPageable{TClaim}"/>。</param>
        /// <param name="includeCategories">包含分类模型集合（可选；默认不包含）。</param>
        /// <returns>返回 <see cref="IPageable{ClaimModel}"/>。</returns>
        protected virtual IPageable<ClaimModel> ToClaimModels(IPageable<TClaim> claims,
            bool includeCategories = false)
        {
            if (claims.IsNull())
                return null;

            Dictionary<TIncremId, TCategory> categories = null;
            if (includeCategories)
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
        /// 转为声明模型集合（支持包含分类集合）。
        /// </summary>
        /// <param name="claims">给定的 <see cref="IEnumerable{TClaim}"/>。</param>
        /// <param name="includeCategories">包含分类模型集合（可选；默认不包含）。</param>
        /// <returns>返回 <see cref="IEnumerable{ClaimModel}"/>。</returns>
        protected virtual IEnumerable<ClaimModel> ToClaimModels(IEnumerable<TClaim> claims,
            bool includeCategories = false)
        {
            if (claims.IsNull())
                return null;

            Dictionary<TIncremId, TCategory> categories = null;
            if (includeCategories)
            {
                categories = claims.Select(s => s.CategoryId).Distinct()
                    .Select(s => ContentAccessor.Categories.Find(s))
                    .ToDictionary(k => k.Id, es => es);
            }

            return claims.Select(s =>
            {
                var model = s.ToModel();
                model.Category = categories?[s.CategoryId].ToModel();

                return model;
            });
        }

        /// <summary>
        /// 转为声明模型（支持包含分类）。
        /// </summary>
        /// <param name="claim">给定的 <typeparamref name="TClaim"/>。</param>
        /// <param name="includeCategory">包含分类模型（可选；默认包含）。</param>
        /// <returns>返回 <see cref="ClaimModel"/>。</returns>
        protected virtual ClaimModel ToClaimModel(TClaim claim, bool includeCategory = true)
        {
            if (claim.IsNull())
                return null;

            var model = claim.ToModel();

            if (includeCategory)
                model.Category = ContentAccessor.Categories.Find(claim.CategoryId).ToModel();

            return model;
        }

        /// <summary>
        /// 转为单元模型集合（支持包含单元集合）
        /// </summary>
        /// <param name="units">给定的 <see cref="IEnumerable{TUnit}"/>。</param>
        /// <param name="includeVisitCount">包含访问计数模型集合（可选；默认不包含）。</param>
        /// <returns>返回 <see cref="IEnumerable{UnitModel}"/>。</returns>
        protected virtual IEnumerable<UnitModel> ToUnitModels(IEnumerable<TUnit> units,
            bool includeVisitCount = false)
        {
            if (units.IsNull())
                return null;

            return units.Select(s =>
            {
                var model = s.ToModel();

                if (includeVisitCount)
                    model.UnitVisitCount = ContentAccessor.UnitVisitCounts.Find(s.Id).ToModel();

                return model;
            });
        }

        /// <summary>
        /// 转为单元模型（支持包含分类）。
        /// </summary>
        /// <param name="unit">给定的 <typeparamref name="TUnit"/>。</param>
        /// <param name="includeCategory">包含分类模型（可选；默认包含）。</param>
        /// <param name="includeSource">包含来源模型（可选；默认包含）。</param>
        /// <param name="includeVisitCount">包含访问计数模型集合（可选；默认包含）。</param>
        /// <param name="includeClaims">包含声明模型集合（可选；默认包含）。</param>
        /// <param name="includeTags">包含标签模型集合（可选；默认包含）。</param>
        /// <returns>返回 <see cref="UnitModel"/>。</returns>
        protected virtual UnitModel ToUnitModel(TUnit unit, bool includeCategory = true,
            bool includeSource = true, bool includeVisitCount = true,
            bool includeClaims = true, bool includeTags = true)
        {
            if (unit.IsNull())
                return null;

            var model = unit.ToModel();

            if (includeCategory)
                model.Category = ContentAccessor.Categories.Find(unit.CategoryId).ToModel();

            if (includeSource)
                model.Source = ContentAccessor.Sources.Find(unit.SourceId).ToModel();

            if (includeVisitCount)
                model.UnitVisitCount = ContentAccessor.UnitVisitCounts.Find(unit.Id).ToModel();

            Dictionary<TIncremId, ClaimModel> claims = null;
            if (includeClaims)
            {
                var unitClaims = ContentAccessor.UnitClaims
                    .Where(p => p.UnitId.Equals(unit.Id))
                    .ToList();

                claims = unitClaims.Select(s => s.ClaimId).Distinct()
                    .Select(s => ContentAccessor.Claims.Find(s))
                    .ToDictionary(ks => ks.Id, es => es.ToModel());

                model.UnitClaims = unitClaims
                    .Select(s => s.ToModel(claims[s.ClaimId]))
                    .ToList();
            }

            Dictionary<TIncremId, TagModel> tags = null;
            if (includeTags)
            {
                var unitTags = ContentAccessor.UnitTags
                    .Where(p => p.UnitId.Equals(unit.Id))
                    .ToList();

                tags = unitTags.Select(s => s.TagId).Distinct()
                    .Select(s => ContentAccessor.Tags.Find(s))
                    .ToDictionary(ks => ks.Id, es => es.ToModel());

                model.UnitTags = unitTags
                    .Select(s => s.ToModel(tags[s.TagId]))
                    .ToList();
            }

            return model;
        }

    }
}
