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

namespace Librame.AspNetCore.Content.Api
{
    using Models;

    internal static class ContentApiModelComplexGraphTypeExtensions
    {
        public static ComplexGraphType<TModel> AddCategoryApiModelFields<TModel>(this ComplexGraphType<TModel> graphType)
            where TModel : CategoryApiModel
        {
            graphType.Field(f => f.Name);
            graphType.Field(f => f.Description, nullable: true);
            graphType.Field(f => f.Parent, nullable: true);

            return graphType;
        }

    }
}
