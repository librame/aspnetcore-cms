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

        #region Category

        /// <summary>
        /// 来自类别模型。
        /// </summary>
        /// <typeparam name="TCategory">指定的类别类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="CategoryModel"/>。</param>
        /// <param name="findParentFactory">根据父级模型查找父级类别的工厂方法（可选；默认优先尝试转换父级标识）。</param>
        /// <returns>返回 <typeparamref name="TCategory"/>。</returns>
        public static TCategory FromModel<TCategory, TIncremId, TCreatedBy>
            (this CategoryModel model, Func<string, TCategory> findParentFactory = null)
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
            {
                if (model.Parent.Id.IsNotEmpty())
                {
                    category.ParentId = model.Parent.Id.ToIncremId<TIncremId>();
                }
                else
                {
                    var parent = findParentFactory?.Invoke(model.Parent.Name);
                    if (parent.IsNotNull())
                        category.ParentId = parent.Id;
                }
            }

            return category;
        }

        /// <summary>
        /// 转为类别模型。
        /// </summary>
        /// <typeparam name="TCategory">指定的类别类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="category">给定的 <typeparamref name="TCategory"/>。</param>
        /// <param name="findParentFactory">根据父级标识查找父级类别的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="CategoryModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static CategoryModel ToModel<TCategory, TIncremId, TCreatedBy>
            (this TCategory category, Func<TIncremId, TCategory> findParentFactory = null)
            where TCategory : ContentCategory<TIncremId, TCreatedBy>
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (category.IsNull())
                return null;

            var model = new CategoryModel
            {
                Name = category.Name,
                Description = category.Description
            };
            model.UpdateModel<TCategory, TIncremId, TCreatedBy>(category);

            if (!category.ParentId.Equals(default))
            {
                var parent = findParentFactory?.Invoke(category.ParentId);
                model.Parent = parent.ToModel<TCategory, TIncremId, TCreatedBy>(findParentFactory);
            }

            return model;
        }

        /// <summary>
        /// 更新类别模型。
        /// </summary>
        /// <typeparam name="TCategory">指定的类别类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="CategoryModel"/>。</param>
        /// <param name="category">给定的 <typeparamref name="TCategory"/>。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static void UpdateModel<TCategory, TIncremId, TCreatedBy>
            (this CategoryModel model, TCategory category)
            where TCategory : ContentCategory<TIncremId, TCreatedBy>
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
            {
                var parent = findCategoryIdFactory?.Invoke(model.Category.Name);
                if (parent.IsNotNull())
                    parent.ParentId = parent.Id;
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
        /// <returns>返回 <see cref="ClaimModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static ClaimModel ToModel<TIncremId, TCategoryId, TCreatedBy>
            (this ContentClaim<TIncremId, TCategoryId, TCreatedBy> claim)
            where TIncremId : IEquatable<TIncremId>
            where TCategoryId : IEquatable<TCategoryId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (claim.IsNull())
                return null;

            return new ClaimModel
            {
                Id = claim.Id.ToString(),
                Name = claim.Name,
                Description = claim.Description,
                CreatedTime = claim.CreatedTime.ToString(CultureInfo.InvariantCulture),
                CreatedBy = claim.CreatedBy.ToString()
            };
        }

        #endregion


        /// <summary>
        /// 将窗格转为窗格模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="pane">给定的 <see cref="ContentPane{TIncremId, TCreatedBy}"/>。</param>
        /// <returns>返回 <see cref="PaneModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static PaneModel ToModel<TIncremId, TCreatedBy>
            (this ContentPane<TIncremId, TCreatedBy> pane)
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (pane.IsNull())
                return null;

            return new PaneModel
            {
                Id = pane.Id.ToString(),
                Name = pane.Name,
                Description = pane.Description,
                Icon = pane.Icon,
                More = pane.More,
                CreatedTime = pane.CreatedTime.ToString(CultureInfo.InvariantCulture),
                CreatedBy = pane.CreatedBy.ToString()
            };
        }

        /// <summary>
        /// 将来源转为来源模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="source">给定的 <see cref="ContentSource{TIncremId, TCreatedBy}"/>。</param>
        /// <returns>返回 <see cref="SourceModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static SourceModel ToModel<TIncremId, TCreatedBy>
            (this ContentSource<TIncremId, TCreatedBy> source)
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (source.IsNull())
                return null;

            return new SourceModel
            {
                Id = source.Id.ToString(),
                Name = source.Name,
                Description = source.Description,
                Website = source.Website,
                Weblogo = source.Weblogo,
                CreatedTime = source.CreatedTime.ToString(CultureInfo.InvariantCulture),
                CreatedBy = source.CreatedBy.ToString()
            };
        }

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
