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
    /// 来源模型。
    /// </summary>
    public class SourceModel : AbstractCreationModel
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
        /// 网站。
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// 网标。
        /// </summary>
        public string Weblogo { get; set; }


        /// <summary>
        /// 父级模型。
        /// </summary>
        public SourceModel Parent { get; set; }
    }
}
