#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Content.Api.Types
{
    using AspNetCore.Api.Types;
    using AspNetCore.Content.Api.Models;

    /// <summary>
    /// 单元访问计数类型。
    /// </summary>
    public class UnitVisitCountType : ApiTypeBase<UnitVisitCountModel>
    {
        /// <summary>
        /// 构造一个单元访问计数类型。
        /// </summary>
        public UnitVisitCountType()
            : base()
        {
            Field(f => f.RetweetCount);
            Field(f => f.ObjectorCount);
            Field(f => f.SupporterCount);
            Field(f => f.FavoriteCount);

            Field(f => f.VisitCount);
            Field(f => f.VisitorCount);

            Field(f => f.Unit, type: typeof(UnitType), nullable: true);
        }

    }
}
