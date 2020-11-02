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
    /// 单元标签模型类型。
    /// </summary>
    public class UnitTagModelType : IdentifierModelTypeBase<UnitTagModel>
    {
        /// <summary>
        /// 构造一个 <see cref="UnitTagModelType"/>。
        /// </summary>
        public UnitTagModelType()
            : base()
        {
            Field(f => f.Unit, type: typeof(UnitModelType), nullable: true);
            Field(f => f.Tag, type: typeof(TagModelType), nullable: true);
        }

    }
}
