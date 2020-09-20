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
using System.Diagnostics.CodeAnalysis;

namespace Librame.Extensions.Content.Options
{
    /// <summary>
    /// 内容存储初始化选项。
    /// </summary>
    public class ContentStoreInitializationOptions
    {
        /// <summary>
        /// 默认总数（如：窗格的单元数据条数）。
        /// </summary>
        public int DefaultTotal { get; set; }
            = 20;


        /// <summary>
        /// 默认类别字典集合（值元组分别表示父名称、备注；空父名称表示为根类别）。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public Dictionary<string, (string parentName, string description)> DefaultCategories { get; set; }
            = new Dictionary<string, (string parentName, string description)>
            {
                { "文章", ( null, "文章类别 (Article)" ) },
                { "文集", ( null, "文集类别 (Anthology)" ) },
                { "图片", ( null, "图片类别 (Picture)" ) },
                { "图册", ( null, "图册类别 (Album)" ) },
                { "专题", ( null, "专题类别 (Subject)" ) }
            };

        /// <summary>
        /// 默认声明字典集合（值元组分别表示类别名称、备注；空类别名称表示不限制类别，即所有类别）。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public Dictionary<string, (string categoryName, string description)> DefaultClaims { get; set; }
            = new Dictionary<string, (string categoryName, string description)>
            {
                { "正文", ( null, "正文声明 (Text)" ) },
                { "模板", ( null, "模板声明 (Template)" ) },
                { "总数", ( null, "总数声明 (Total)" ) }
            };

        /// <summary>
        /// 默认窗格字典集合（值元组分别表示父名称、备注；空父名称表示为根窗格）。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public Dictionary<string, (string parentName, string description)> DefaultPanes { get; set; }
            = new Dictionary<string, (string parentName, string description)>
            {
                { "友链", ( null, "友情链接" ) },
                { "快讯", ( null, "最新消息" ) },
                { "焦点", ( null, "最热排行" ) }
            };

        /// <summary>
        /// 默认来源字典集合（值元组分别表示父名称、备注；空父名称表示为根来源）。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public Dictionary<string, (string parentName, string description)> DefaultSources { get; set; }
            = new Dictionary<string, (string parentName, string description)>
            {
                { "原创", ( null, "本站原创" ) }
            };

        /// <summary>
        /// 默认标签列表集合。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public List<string> DefaultTags { get; set; }
            = new List<string>
            {
                "测试"
            };
    }
}
