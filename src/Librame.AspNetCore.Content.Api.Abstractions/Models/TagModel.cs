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
    /// <summary>
    /// 标签模型。
    /// </summary>
    public class TagModel : AbstractCreationModel
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }
    }
}
