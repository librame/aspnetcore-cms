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
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Content.Api
{
    using AspNetCore.Api;
    using AspNetCore.Content.Api.Models;
    using AspNetCore.Content.Api.Types;
    using Extensions;
    using Extensions.Content.Accessors;
    using Extensions.Content.Builders;
    using Extensions.Content.Stores;
    using Extensions.Core.Services;
    using Extensions.Data.Accessors;
    using Extensions.Data.Collections;

    /// <summary>
    /// 内容图形 API 变化。
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
    public class ContentGraphApiMutation<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim, TGenId, TIncremId, TPublishedBy> : GraphApiMutationBase
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
        /// 构造一个内容图形 API 变化。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public ContentGraphApiMutation(IAccessor accessor, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            ContentAccessor = accessor.CastTo<IAccessor,
                IContentAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim>>(nameof(accessor));

            AddCategoryTypeField();
        }


        /// <summary>
        /// 内容访问器。
        /// </summary>
        protected IContentAccessor<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneClaim> ContentAccessor { get; }


        private void AddCategoryTypeField()
        {
            // "query": "mutation($category:CategoryInput!) { addCategory(category: $category) { id... }}",
            // "variables": {
            //     "category": {
            //         "name": "",
            //         "description": "",
            //         "parent": null
            //     }
            // }
            FieldAsync<CategoryType>
            (
                name: "addCategory",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CategoryInputType>> { Name = "category" }
                ),
                resolve: async context =>
                {
                    var model = context.GetArgument<CategoryModel>("category");
                    var category = model.FromModel<TCategory, TIncremId, TPublishedBy>(parentName
                        => ContentAccessor.Categories.FirstOrDefault(p => p.Name == parentName));

                    await ContentAccessor.CategoriesManager.TryAddAsync(p => p.Equals(category),
                        () => category,
                        addedPost =>
                        {
                            if (!ContentAccessor.RequiredSaveChanges)
                                ContentAccessor.RequiredSaveChanges = true;
                        })
                        .ConfigureAwait();

                    model.UpdateModel<TCategory, TIncremId, TPublishedBy>(category);

                    return model;
                }
            );
        }

    }
}
