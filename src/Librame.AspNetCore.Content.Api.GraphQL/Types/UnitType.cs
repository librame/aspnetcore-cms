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

namespace Librame.AspNetCore.Content.Api.Types
{
    using AspNetCore.Api.Types;
    using AspNetCore.Content.Api.Models;

    /// <summary>
    /// 单元类型。
    /// </summary>
    public class UnitType : ApiTypeBase<UnitModel>
    {
        /// <summary>
        /// 构造一个单元类型。
        /// </summary>
        public UnitType()
            : base()
        {
            Field(f => f.Id);
            Field(f => f.Title);
            Field(f => f.Subtitle);
            Field(f => f.Tags);
            Field(f => f.Reference);
            Field(f => f.PublishedAs);
            Field(f => f.PublishedTime);
            Field(f => f.PublishedBy);
            Field(f => f.CreatedTime);
            Field(f => f.CreatedBy);

            Field(f => f.Category, type: typeof(CategoryType));
            Field(f => f.Source, type: typeof(SourceType));
            Field(f => f.UnitVisitCount, type: typeof(UnitVisitCountType));
            Field(f => f.UnitClaims, type: typeof(ListGraphType<UnitClaimType>));
            Field(f => f.UnitTags, type: typeof(ListGraphType<UnitTagType>));
        }

    }
}
