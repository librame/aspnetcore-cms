#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.AspNetCore.Portal
{
    using Extensions.Data;

    /// <summary>
    /// 门户构建器选项。
    /// </summary>
    public class PortalBuilderOptions : DataBuilderOptionsBase<PortalTableNameSchemaOptions>
    {
    }


    /// <summary>
    /// 门户表名架构选项集合。
    /// </summary>
    public class PortalTableNameSchemaOptions : TableNameSchemaOptions
    {
        /// <summary>
        /// 分类工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> CategoryFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 编者工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> EditorFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 编者头衔工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> EditorTitleFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 窗格工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> PaneFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 来源工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> SourceFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 专题工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> SubjectFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 专题主体工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> SubjectBodyFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 标签工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> TagFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 标签引用工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> TagReferenceFactory { get; set; }
            = type => type.AsSchema();
    }
}
