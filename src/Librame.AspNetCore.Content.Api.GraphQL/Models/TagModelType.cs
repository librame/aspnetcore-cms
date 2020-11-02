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
    /// 标签模型类型。
    /// </summary>
    public class TagModelType : CreationIdentifierModelTypeBase<TagModel>
    {
        /// <summary>
        /// 构造一个 <see cref="TagModelType"/>。
        /// </summary>
        public TagModelType()
            : base()
        {
            Field(f => f.Name);
        }

    }
}
