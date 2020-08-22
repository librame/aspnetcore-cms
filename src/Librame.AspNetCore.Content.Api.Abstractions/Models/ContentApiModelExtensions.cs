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
        /// 将内容分类转为分类模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="category">给定的 <see cref="ContentCategory{TIncremId, TCreatedBy}"/>。</param>
        /// <returns>返回 <see cref="CategoryModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static CategoryModel ToModel<TIncremId, TCreatedBy>
            (this ContentCategory<TIncremId, TCreatedBy> category)
            where TIncremId : IEquatable<TIncremId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (category.IsNull())
                return null;

            return new CategoryModel
            {
                Id = category.Id.ToString(),
                Name = category.Name,
                Description = category.Description,
                CreatedTime = category.CreatedTime.ToString(CultureInfo.InvariantCulture),
                CreatedBy = category.CreatedBy.ToString()
            };
        }

        /// <summary>
        /// 将内容声明转为声明模型。
        /// </summary>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
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

        /// <summary>
        /// 将内容窗格转为窗格模型。
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
        /// 将内容来源转为来源模型。
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
        /// 将内容标签转为标签模型。
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
        /// 将内容单元转为单元模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
        /// <typeparam name="TSourceId">指定的来源标识类型。</typeparam>
        /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
        /// <param name="unit">给定的 <see cref="ContentUnit{TGenId, TCategoryId, TSourceId, TPublishedBy}"/>。</param>
        /// <returns>返回 <see cref="UnitModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static UnitModel ToModel<TGenId, TCategoryId, TSourceId, TPublishedBy>
            (this ContentUnit<TGenId, TCategoryId, TSourceId, TPublishedBy> unit)
            where TGenId : IEquatable<TGenId>
            where TCategoryId : IEquatable<TCategoryId>
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
                Tags = unit.Tags,
                Reference = unit.Reference,
                PublishedAs = unit.PublishedAs,
                PublishedTime = unit.PublishedTime.ToString(CultureInfo.InvariantCulture),
                PublishedBy = unit.PublishedBy.ToString(),
                CreatedTime = unit.CreatedTime.ToString(CultureInfo.InvariantCulture),
                CreatedBy = unit.CreatedBy.ToString()
            };
        }

        /// <summary>
        /// 将内容单元声明转为单元声明模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
        /// <param name="unitClaim">给定的 <see cref="ContentUnitClaim{TIncremId, TGenId, TIncremId, TPublishedBy}"/>。</param>
        /// <param name="claim">给定的 <see cref="ClaimModel"/>。</param>
        /// <returns>返回 <see cref="UnitModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static UnitClaimModel ToModel<TIncremId, TGenId, TPublishedBy>
            (this ContentUnitClaim<TIncremId, TGenId, TIncremId, TPublishedBy> unitClaim, ClaimModel claim)
            where TGenId : IEquatable<TGenId>
            where TIncremId : IEquatable<TIncremId>
            where TPublishedBy : IEquatable<TPublishedBy>
        {
            if (unitClaim.IsNull())
                return null;

            return new UnitClaimModel
            {
                Claim = claim,
                Value = unitClaim.ClaimValue,
            };
        }

        /// <summary>
        /// 将内容单元标签转为单元标签模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <param name="unitTag">给定的 <see cref="ContentUnitTag{TIncremId, TGenId, TIncremId}"/>。</param>
        /// <param name="tag">给定的 <see cref="TagModel"/>。</param>
        /// <returns>返回 <see cref="UnitModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static UnitTagModel ToModel<TIncremId, TGenId>
            (this ContentUnitTag<TIncremId, TGenId, TIncremId> unitTag, TagModel tag)
            where TGenId : IEquatable<TGenId>
            where TIncremId : IEquatable<TIncremId>
        {
            if (unitTag.IsNull())
                return null;

            return new UnitTagModel
            {
                Tag = tag
            };
        }

        /// <summary>
        /// 将内容标签转为标签模型。
        /// </summary>
        /// <typeparam name="TUnitId">指定的内容单元标识类型。</typeparam>
        /// <param name="visitCount">给定的 <see cref="ContentUnitVisitCount{TUnitId}"/>。</param>
        /// <returns>返回 <see cref="TagModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
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

    }
}
