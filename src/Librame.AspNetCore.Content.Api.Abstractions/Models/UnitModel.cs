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
    /// <summary>
    /// 单元模型。
    /// </summary>
    public class UnitModel
    {
        /// <summary>
        /// 标识。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 副标题。
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// 标签集合。
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 引用源。
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// 发表为（如：资源链接）。
        /// </summary>
        public string PublishedAs { get; set; }

        /// <summary>
        /// 发表时间。
        /// </summary>
        public string PublishedTime { get; set; }

        /// <summary>
        /// 发表者。
        /// </summary>
        public string PublishedBy { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        public string CreatedTime { get; set; }

        /// <summary>
        /// 创建者。
        /// </summary>
        public string CreatedBy { get; set; }


        /// <summary>
        /// 分类模型。
        /// </summary>
        public CategoryModel Category { get; set; }

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
