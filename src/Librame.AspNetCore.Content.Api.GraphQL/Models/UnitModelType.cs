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

namespace Librame.AspNetCore.Content.Api.Models
{
    using AspNetCore.Api.Models;

    /// <summary>
    /// 单元模型类型。
    /// </summary>
    public class UnitModelType : PublicationIdentifierModelTypeBase<UnitModel>
    {
        /// <summary>
        /// 构造一个 <see cref="UnitModelType"/>。
        /// </summary>
        public UnitModelType()
            : base()
        {
            Field(f => f.Title);
            Field(f => f.Subtitle);
            Field(f => f.Reference);

            Field(f => f.Category, type: typeof(CategoryModelType), nullable: true);
            Field(f => f.Pane, type: typeof(PaneModelType), nullable: true);
            Field(f => f.Source, type: typeof(SourceModelType), nullable: true);
            Field(f => f.UnitVisitCount, type: typeof(UnitVisitCountModelType), nullable: true);
            Field(f => f.UnitClaims, type: typeof(ListGraphType<UnitClaimModelType>), nullable: true);
            Field(f => f.UnitTags, type: typeof(ListGraphType<UnitTagModelType>), nullable: true);
        }

    }
}
