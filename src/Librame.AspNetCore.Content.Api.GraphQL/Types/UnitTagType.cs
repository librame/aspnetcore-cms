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
    /// 单元标签类型。
    /// </summary>
    public class UnitTagType : ApiTypeBase<UnitTagModel>
    {
        /// <summary>
        /// 构造一个单元标签类型。
        /// </summary>
        public UnitTagType()
            : base()
        {
            Field(f => f.Tag, type: typeof(TagType));
        }

    }
}
