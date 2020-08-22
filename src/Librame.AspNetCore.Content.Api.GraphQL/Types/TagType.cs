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
    /// 标签类型。
    /// </summary>
    public class TagType : ApiTypeBase<TagModel>
    {
        /// <summary>
        /// 构造一个标签类型。
        /// </summary>
        public TagType()
            : base()
        {
            Field(f => f.Id);
            Field(f => f.Name);
            Field(f => f.CreatedTime);
            Field(f => f.CreatedBy);
        }

    }
}
