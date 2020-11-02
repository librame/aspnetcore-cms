#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Librame.AspNetCore.Content.Api.Models
{
    using Extensions;
    using Extensions.Content.Stores;

    /// <summary>
    /// 内容 API 模型静态扩展。
    /// </summary>
    public static class ContentApiModelExtensions
    {
        /// <summary>
        /// 解析生成式标识。
        /// </summary>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <param name="id">给定的标识字符串。</param>
        /// <returns>返回 <typeparamref name="TGenId"/>。</returns>
        public static TGenId ParseGenId<TGenId>(this string id)
        {
            var targetType = typeof(TGenId);

            object value = targetType.Name switch
            {
                "Guid" => Guid.Parse(id),
                "String" => id,
                "Int64" => long.Parse(id, CultureInfo.InvariantCulture),

                _ => new NotSupportedException($"Unsupported generative identification type '{targetType}'")
            };

            return (TGenId)value;
        }

        /// <summary>
        /// 解析增量式标识。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <param name="id">给定的标识字符串。</param>
        /// <returns>返回 <typeparamref name="TIncremId"/>。</returns>
        public static TIncremId ParseIncremId<TIncremId>(this string id)
        {
            var targetType = typeof(TIncremId);

            object value = targetType.Name switch
            {
                "SByte" => sbyte.Parse(id, CultureInfo.InvariantCulture),
                "Byte" => byte.Parse(id, CultureInfo.InvariantCulture),
                "Int16" => short.Parse(id, CultureInfo.InvariantCulture),
                "UInt16" => ushort.Parse(id, CultureInfo.InvariantCulture),
                "Int32" => int.Parse(id, CultureInfo.InvariantCulture),
                "UInt32" => uint.Parse(id, CultureInfo.InvariantCulture),
                "Int64" => long.Parse(id, CultureInfo.InvariantCulture),
                "UInt64" => ulong.Parse(id, CultureInfo.InvariantCulture),

                _ => new NotSupportedException($"Unsupported incremental identification type '{targetType}'")
            };

            return (TIncremId)value;
        }


        #region Category

        /// <summary>
        /// 来自类别模型。
        /// </summary>
        /// <typeparam name="TCategory">指定的类别类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="CategoryModel"/>。</param>
        /// <param name="findParentIdFactory">根据父级模型名称查找父级类别的工厂方法（可选；默认优先解析父级模型标识）。</param>
        /// <returns>返回 <typeparamref name="TCategory"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static TCategory FromModel<TCategory, TIncremId, TCreatedBy>
            (this CategoryModel model, Func<string, TIncremId> findParentIdFactory = null)
            where TCategory : ContentCategory<TIncremId, TCreatedBy>
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (model.IsNull())
                return null;

            var category = typeof(TCategory).EnsureCreate<TCategory>();
            category.Name = model.Name;
            category.Description = model.Description;

            if (model.Parent.IsNotNull())
                category.ParentId = GetParentId();

            return category;

            // 获取父标识
            TIncremId GetParentId()
            {
                // 优先解析父级模型标识
                if (model.Parent.Id.IsNotEmpty())
                    return model.Parent.Id.ParseIncremId<TIncremId>();

                findParentIdFactory.NotNull(nameof(findParentIdFactory));
                return findParentIdFactory.Invoke(model.Parent.Name);
            }
        }

        /// <summary>
        /// 转为类别模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="category">给定的 <see cref="ContentCategory{TIncremId, TCreatedBy}"/>。</param>
        /// <param name="findParentFactory">根据父级标识查找父级类别的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="CategoryModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static CategoryModel ToModel<TIncremId, TCreatedBy>
            (this ContentCategory<TIncremId, TCreatedBy> category,
            Func<TIncremId, ContentCategory<TIncremId, TCreatedBy>> findParentFactory = null)
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (category.IsNull())
                return null;

            var model = new CategoryModel();
            model.UpdateModel(category);

            if (!category.ParentId.Equals(default))
            {
                var parent = findParentFactory?.Invoke(category.ParentId);
                model.Parent = parent.ToModel(findParentFactory);
            }

            return model;
        }

        /// <summary>
        /// 更新类别模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="CategoryModel"/>。</param>
        /// <param name="category">给定的 <see cref="ContentCategory{TIncremId, TCreatedBy}"/>。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static void UpdateModel<TIncremId, TCreatedBy>
            (this CategoryModel model, ContentCategory<TIncremId, TCreatedBy> category)
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            model.NotNull(nameof(model));
            category.NotNull(nameof(category));

            if (model.Name != category.Name)
                model.Name = category.Name;

            if (model.Description != category.Description)
                model.Description = category.Description;

            model.Populate(category);
        }

        #endregion


        #region Claim

        /// <summary>
        /// 来自声明模型。
        /// </summary>
        /// <typeparam name="TClaim">指定的声明类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCategoryId">指定的类别标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="ClaimModel"/>。</param>
        /// <param name="findCategoryIdFactory">根据名称查找类别标识的工厂方法。</param>
        /// <returns>返回 <typeparamref name="TClaim"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static TClaim FromModel<TClaim, TIncremId, TCategoryId, TCreatedBy>
            (this ClaimModel model, Func<string, TCategoryId> findCategoryIdFactory)
            where TClaim : ContentClaim<TIncremId, TCategoryId, TCreatedBy>
            where TIncremId : IEquatable<TIncremId>
            where TCategoryId : IEquatable<TCategoryId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (model.IsNull())
                return null;

            var claim = typeof(TClaim).EnsureCreate<TClaim>();
            claim.Name = model.Name;
            claim.Description = model.Description;

            if (model.Category.IsNotNull())
                claim.CategoryId = GetCategoryId();

            // 获取类别标识
            TCategoryId GetCategoryId()
            {
                // 优先解析类别模型标识
                if (model.Category.Id.IsNotEmpty())
                    return model.Category.Id.ParseIncremId<TCategoryId>();

                findCategoryIdFactory.NotNull(nameof(findCategoryIdFactory));
                return findCategoryIdFactory.Invoke(model.Category.Name);
            }

            return claim;
        }

        /// <summary>
        /// 转为声明模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCategoryId">指定的类别标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="claim">给定的 <see cref="ContentClaim{TIncremId, TCategoryId, TCreatedBy}"/>。</param>
        /// <param name="findCategoryFactory">根据类别标识查找类别的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="ClaimModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static ClaimModel ToModel<TIncremId, TCategoryId, TCreatedBy>
            (this ContentClaim<TIncremId, TCategoryId, TCreatedBy> claim,
            Func<TCategoryId, ContentCategory<TCategoryId, TCreatedBy>> findCategoryFactory = null)
            where TIncremId : IEquatable<TIncremId>
            where TCategoryId : IEquatable<TCategoryId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (claim.IsNull())
                return null;

            var model = new ClaimModel();
            model.UpdateModel(claim);

            if (!claim.CategoryId.Equals(default))
            {
                var category = findCategoryFactory?.Invoke(claim.CategoryId);
                model.Category = category.ToModel(findCategoryFactory);
            }

            return model;
        }

        /// <summary>
        /// 更新声明模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的声明增量式标识类型。</typeparam>
        /// <typeparam name="TCategoryId">指定的类别标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者声明。</typeparam>
        /// <param name="model">给定的 <see cref="ClaimModel"/>。</param>
        /// <param name="claim">给定的 <see cref="ContentClaim{TIncremId, TCategoryId, TCreatedBy}"/>。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static void UpdateModel<TIncremId, TCategoryId, TCreatedBy>
            (this ClaimModel model, ContentClaim<TIncremId, TCategoryId, TCreatedBy> claim)
            where TIncremId : IEquatable<TIncremId>
            where TCategoryId : IEquatable<TCategoryId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            model.NotNull(nameof(model));
            claim.NotNull(nameof(claim));

            if (model.Name != claim.Name)
                model.Name = claim.Name;

            if (model.Description != claim.Description)
                model.Description = claim.Description;

            model.Populate(claim);
        }

        #endregion


        #region Pane

        /// <summary>
        /// 来自窗格模型。
        /// </summary>
        /// <typeparam name="TPane">指定的窗格类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="PaneModel"/>。</param>
        /// <param name="findParentIdFactory">根据父级模型名称查找父级窗格的工厂方法（可选；默认优先解析父级模型标识）。</param>
        /// <returns>返回 <typeparamref name="TPane"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static TPane FromModel<TPane, TIncremId, TCreatedBy>
            (this PaneModel model, Func<string, TIncremId> findParentIdFactory = null)
            where TPane : ContentPane<TIncremId, TCreatedBy>
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (model.IsNull())
                return null;

            var pane = typeof(TPane).EnsureCreate<TPane>();
            pane.Name = model.Name;
            pane.Description = model.Description;

            if (model.Parent.IsNotNull())
                pane.ParentId = GetParentId();

            return pane;

            // 获取父标识
            TIncremId GetParentId()
            {
                // 优先解析父级模型标识
                if (model.Parent.Id.IsNotEmpty())
                    return model.Parent.Id.ParseIncremId<TIncremId>();

                findParentIdFactory.NotNull(nameof(findParentIdFactory));
                return findParentIdFactory.Invoke(model.Parent.Name);
            }
        }

        /// <summary>
        /// 转为窗格模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="pane">给定的 <see cref="ContentPane{TIncremId, TCreatedBy}"/>。</param>
        /// <param name="findParentFactory">根据父级标识查找父级窗格的工厂方法（可选）。</param>
        /// <param name="paneClaims">给定的 <see cref="IReadOnlyList{PaneClaimModel}"/>（可选）。</param>
        /// <returns>返回 <see cref="PaneModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static PaneModel ToModel<TIncremId, TCreatedBy>
            (this ContentPane<TIncremId, TCreatedBy> pane,
            Func<TIncremId, ContentPane<TIncremId, TCreatedBy>> findParentFactory = null,
            IReadOnlyList<PaneClaimModel> paneClaims = null)
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (pane.IsNull())
                return null;

            var model = new PaneModel();
            model.UpdateModel(pane, paneClaims);

            if (!pane.ParentId.Equals(default))
            {
                var parent = findParentFactory?.Invoke(pane.ParentId);
                model.Parent = parent.ToModel(findParentFactory);
            }

            return model;
        }

        /// <summary>
        /// 更新窗格模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="PaneModel"/>。</param>
        /// <param name="pane">给定的 <see cref="ContentPane{TIncremId, TCreatedBy}"/>。</param>
        /// <param name="paneClaims">给定的 <see cref="IReadOnlyList{PaneClaimModel}"/>（可选）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static void UpdateModel<TIncremId, TCreatedBy>
            (this PaneModel model, ContentPane<TIncremId, TCreatedBy> pane,
            IReadOnlyList<PaneClaimModel> paneClaims = null)
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            model.NotNull(nameof(model));
            pane.NotNull(nameof(pane));

            if (model.Name != pane.Name)
                model.Name = pane.Name;

            if (model.Description != pane.Description)
                model.Description = pane.Description;

            if (model.Icon != pane.Icon)
                model.Icon = pane.Icon;

            if (model.More != pane.More)
                model.More = pane.More;

            if (paneClaims.IsNotNull())
                model.PaneClaims = paneClaims;

            model.Populate(pane);
        }

        #endregion


        #region Source

        /// <summary>
        /// 来自来源模型。
        /// </summary>
        /// <typeparam name="TSource">指定的来源类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="SourceModel"/>。</param>
        /// <param name="findParentIdFactory">根据父级模型名称查找父级来源的工厂方法（可选；默认优先解析父级模型标识）。</param>
        /// <returns>返回 <typeparamref name="TSource"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static TSource FromModel<TSource, TIncremId, TCreatedBy>
            (this SourceModel model, Func<string, TIncremId> findParentIdFactory = null)
            where TSource : ContentSource<TIncremId, TCreatedBy>
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (model.IsNull())
                return null;

            var source = typeof(TSource).EnsureCreate<TSource>();
            source.Name = model.Name;
            source.Description = model.Description;

            if (model.Parent.IsNotNull())
                source.ParentId = GetParentId();

            return source;

            // 获取父标识
            TIncremId GetParentId()
            {
                // 优先解析父级模型标识
                if (model.Parent.Id.IsNotEmpty())
                    return model.Parent.Id.ParseIncremId<TIncremId>();

                findParentIdFactory.NotNull(nameof(findParentIdFactory));
                return findParentIdFactory.Invoke(model.Parent.Name);
            }
        }

        /// <summary>
        /// 转为来源模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="source">给定的 <see cref="ContentSource{TIncremId, TCreatedBy}"/>。</param>
        /// <param name="findParentFactory">根据父级标识查找父级来源的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="SourceModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static SourceModel ToModel<TIncremId, TCreatedBy>
            (this ContentSource<TIncremId, TCreatedBy> source,
            Func<TIncremId, ContentSource<TIncremId, TCreatedBy>> findParentFactory = null)
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (source.IsNull())
                return null;

            var model = new SourceModel();
            model.UpdateModel(source);

            if (!source.ParentId.Equals(default))
            {
                var parent = findParentFactory?.Invoke(source.ParentId);
                model.Parent = parent.ToModel(findParentFactory);
            }

            return model;
        }

        /// <summary>
        /// 更新来源模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="SourceModel"/>。</param>
        /// <param name="source">给定的 <see cref="ContentSource{TIncremId, TCreatedBy}"/>。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static void UpdateModel<TIncremId, TCreatedBy>
            (this SourceModel model, ContentSource<TIncremId, TCreatedBy> source)
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            model.NotNull(nameof(model));
            source.NotNull(nameof(source));

            if (model.Name != source.Name)
                model.Name = source.Name;

            if (model.Description != source.Description)
                model.Description = source.Description;

            if (model.Website != source.Website)
                model.Website = source.Website;

            if (model.Weblogo != source.Weblogo)
                model.Weblogo = source.Weblogo;

            model.Populate(source);
        }

        #endregion


        /// <summary>
        /// 将标签转为标签模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="tag">给定的 <see cref="ContentTag{TIncremId, TCreatedBy}"/>。</param>
        /// <returns>返回 <see cref="TagModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static TagModel ToModel<TIncremId, TCreatedBy>
            (this ContentTag<TIncremId, TCreatedBy> tag)
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (tag.IsNull())
                return null;

            return new TagModel
            {
                Id = tag.Id.ToString(),
                Name = tag.Name,
                CreatedTime = tag.CreatedTime.ToString(CultureInfo.InvariantCulture),
                CreatedBy = tag.CreatedBy.ToString()
            };
        }

        /// <summary>
        /// 将单元转为单元模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TCategoryId">指定的类别标识类型。</typeparam>
        /// <typeparam name="TPaneId">指定的窗格标识类型。</typeparam>
        /// <typeparam name="TSourceId">指定的来源标识类型。</typeparam>
        /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
        /// <param name="unit">给定的 <see cref="ContentUnit{TGenId, TCategoryId, TPaneId, TSourceId, TPublishedBy}"/>。</param>
        /// <param name="category">给定的 <see cref="CategoryModel"/>（可选）。</param>
        /// <param name="pane">指定的 <see cref="PaneModel"/>（可选）。</param>
        /// <param name="source">给定的 <see cref="SourceModel"/>（可选）。</param>
        /// <param name="unitVisitCount">给定的 <see cref="UnitVisitCountModel"/>（可选）。</param>
        /// <param name="unitClaims">给定的 <see cref="IReadOnlyList{UnitClaimModel}"/>（可选）。</param>
        /// <param name="unitTags">给定的 <see cref="IReadOnlyList{UnitTagModel}"/>（可选）。</param>
        /// <returns>返回 <see cref="UnitModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static UnitModel ToModel<TGenId, TCategoryId, TPaneId, TSourceId, TPublishedBy>
            (this ContentUnit<TGenId, TCategoryId, TPaneId, TSourceId, TPublishedBy> unit,
            CategoryModel category = null, PaneModel pane = null,
            SourceModel source = null, UnitVisitCountModel unitVisitCount = null,
            IReadOnlyList<UnitClaimModel> unitClaims = null, IReadOnlyList<UnitTagModel> unitTags = null)
            where TGenId : IEquatable<TGenId>
            where TCategoryId : IEquatable<TCategoryId>
            where TPaneId : IEquatable<TPaneId>
            where TSourceId : IEquatable<TSourceId>
            where TPublishedBy : IEquatable<TPublishedBy>
        {
            if (unit.IsNull())
                return null;

            return new UnitModel
            {
                Id = unit.Id.ToString(),
                Title = unit.Title,
                Subtitle = unit.Subtitle,
                Reference = unit.Reference,
                PublishedAs = unit.PublishedAs,
                PublishedTime = unit.PublishedTime.ToString(CultureInfo.InvariantCulture),
                PublishedBy = unit.PublishedBy.ToString(),
                CreatedTime = unit.CreatedTime.ToString(CultureInfo.InvariantCulture),
                CreatedBy = unit.CreatedBy.ToString(),

                Category = category,
                Pane = pane,
                Source = source,
                UnitVisitCount = unitVisitCount,
                UnitClaims = unitClaims,
                UnitTags = unitTags
            };
        }

        /// <summary>
        /// 将单元声明转为单元声明模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
        /// <param name="unitClaim">给定的 <see cref="ContentUnitClaim{TIncremId, TGenId, TIncremId, TPublishedBy}"/>。</param>
        /// <param name="unit">给定的 <see cref="UnitModel"/>（可选）。</param>
        /// <param name="claim">给定的 <see cref="ClaimModel"/>（可选）。</param>
        /// <returns>返回 <see cref="ClaimModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static UnitClaimModel ToModel<TIncremId, TGenId, TPublishedBy>
            (this ContentUnitClaim<TIncremId, TGenId, TIncremId, TPublishedBy> unitClaim,
            UnitModel unit = null, ClaimModel claim = null)
            where TGenId : IEquatable<TGenId>
            where TIncremId : IEquatable<TIncremId>
            where TPublishedBy : IEquatable<TPublishedBy>
        {
            if (unitClaim.IsNull())
                return null;

            return new UnitClaimModel
            {
                Id = unitClaim.Id.ToString(),
                Unit = unit,
                Claim = claim,
                ClaimValue = unitClaim.ClaimValue
            };
        }

        /// <summary>
        /// 将单元标签转为单元标签模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <param name="unitTag">给定的 <see cref="ContentUnitTag{TIncremId, TGenId, TIncremId}"/>。</param>
        /// <param name="tag">给定的 <see cref="TagModel"/>（可选）。</param>
        /// <returns>返回 <see cref="ClaimModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static UnitTagModel ToModel<TIncremId, TGenId>
            (this ContentUnitTag<TIncremId, TGenId, TIncremId> unitTag, TagModel tag = null)
            where TGenId : IEquatable<TGenId>
            where TIncremId : IEquatable<TIncremId>
        {
            if (unitTag.IsNull())
                return null;

            return new UnitTagModel
            {
                Id = unitTag.Id.ToString(),
                Tag = tag
            };
        }

        /// <summary>
        /// 将标签转为标签模型。
        /// </summary>
        /// <typeparam name="TUnitId">指定的单元标识类型。</typeparam>
        /// <param name="visitCount">给定的 <see cref="ContentUnitVisitCount{TUnitId}"/>。</param>
        /// <returns>返回 <see cref="TagModel"/>。</returns>
        public static UnitVisitCountModel ToModel<TUnitId>
            (this ContentUnitVisitCount<TUnitId> visitCount)
            where TUnitId : IEquatable<TUnitId>
        {
            if (visitCount.IsNull())
                return null;

            return new UnitVisitCountModel
            {
                RetweetCount = visitCount.RetweetCount.ToString(CultureInfo.InvariantCulture),
                ObjectorCount = visitCount.ObjectorCount.ToString(CultureInfo.InvariantCulture),
                SupporterCount = visitCount.SupporterCount.ToString(CultureInfo.InvariantCulture),
                FavoriteCount = visitCount.FavoriteCount.ToString(CultureInfo.InvariantCulture),

                VisitCount = visitCount.VisitCount.ToString(CultureInfo.InvariantCulture),
                VisitorCount = visitCount.VisitorCount.ToString(CultureInfo.InvariantCulture)
            };
        }

        /// <summary>
        /// 将窗格单元转为窗格单元模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TPaneId">指定的窗格标识类型。</typeparam>
        /// <typeparam name="TUnitId">指定的单元标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="paneClaim">给定的 <see cref="ContentPaneClaim{TIncremId, TPaneId, TUnitId, TCreatedBy}"/>。</param>
        /// <param name="pane">给定的 <see cref="PaneModel"/>（可选）。</param>
        /// <param name="claim">给定的 <see cref="ClaimModel"/>（可选）。</param>
        /// <returns>返回 <see cref="PaneClaimModel"/>。</returns>
        public static PaneClaimModel ToModel<TIncremId, TPaneId, TUnitId, TCreatedBy>
            (this ContentPaneClaim<TIncremId, TPaneId, TUnitId, TCreatedBy> paneClaim,
            PaneModel pane = null, ClaimModel claim = null)
            where TIncremId : IEquatable<TIncremId>
            where TPaneId : IEquatable<TPaneId>
            where TUnitId : IEquatable<TUnitId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (paneClaim.IsNull())
                return null;

            return new PaneClaimModel
            {
                Id = paneClaim.Id.ToString(),
                Pane = pane,
                Claim = claim,
                ClaimValue = paneClaim.ClaimValue
            };
        }

    }
}
