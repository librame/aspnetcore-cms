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
    /// 声明模型。
    /// </summary>
    public class ClaimModel : AbstractCreationIdentifierModel
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// 类别模型。
        /// </summary>
        public CategoryModel Category { get; set; }
    }
}
