#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;

namespace Librame.AspNetCore.Content.Api.Models
{
    using AspNetCore.Api.Models;

    /// <summary>
    /// 单元模型。
    /// </summary>
    public class UnitModel : AbstractPublicationIdentifierModel
    {
        /// <summary>
        /// 标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 副标题。
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// 引用源。
        /// </summary>
        public string Reference { get; set; }


        /// <summary>
        /// 类别模型。
        /// </summary>
        public CategoryModel Category { get; set; }

        /// <summary>
        /// 窗格模型。
        /// </summary>
        public PaneModel Pane { get; set; }

        /// <summary>
        /// 来源模型。
        /// </summary>
        public SourceModel Source { get; set; }

        /// <summary>
        /// 单元访问计数模型。
        /// </summary>
        public UnitVisitCountModel UnitVisitCount { get; set; }

        /// <summary>
        /// 单元声明模型列表。
        /// </summary>
        public IReadOnlyList<UnitClaimModel> UnitClaims { get; set; }

        /// <summary>
        /// 单元标签模型列表。
        /// </summary>
        public IReadOnlyList<UnitTagModel> UnitTags { get; set; }
    }
}
