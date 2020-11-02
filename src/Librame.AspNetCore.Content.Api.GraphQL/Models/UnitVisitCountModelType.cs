#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Content.Api.Models
{
    using AspNetCore.Api.Models;

    /// <summary>
    /// 单元访问计数模型类型。
    /// </summary>
    public class UnitVisitCountModelType : ModelTypeBase<UnitVisitCountModel>
    {
        /// <summary>
        /// 构造一个 <see cref="UnitVisitCountModelType"/>。
        /// </summary>
        public UnitVisitCountModelType()
            : base()
        {
            Field(f => f.RetweetCount);
            Field(f => f.ObjectorCount);
            Field(f => f.SupporterCount);
            Field(f => f.FavoriteCount);

            Field(f => f.VisitCount);
            Field(f => f.VisitorCount);

            Field(f => f.Unit, type: typeof(UnitModelType), nullable: true);
        }

    }
}
