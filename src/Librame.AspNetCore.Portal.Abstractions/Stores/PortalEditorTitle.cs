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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Librame.AspNetCore.Portal
{
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// 门户编者头衔。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
    [Description("门户编者头衔")]
    public class PortalEditorTitle<TIncremId, TEditorId> : AbstractEntityCreation<TIncremId>, IEquatable<PortalEditorTitle<TIncremId, TEditorId>>
        where TIncremId : IEquatable<TIncremId>
        where TEditorId : IEquatable<TEditorId>
    {
        /// <summary>
        /// 编者标识。
        /// </summary>
        [Display(Name = nameof(EditorId), ResourceType = typeof(PortalEditorTitleResource))]
        public virtual TEditorId EditorId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        [Display(Name = nameof(Name), ResourceType = typeof(PortalEditorTitleResource))]
        public virtual string Name { get; set; }


        /// <summary>
        /// 唯一索引是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="PortalEditor{TIncremId, TEditorId}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(PortalEditorTitle<TIncremId, TEditorId> other)
            => EditorId.Equals(other.NotNull(nameof(other)).EditorId) && Name == other?.Name;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is PortalEditorTitle<TIncremId, TEditorId> other) ? Equals(other) : false;


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回 32 位整数。</returns>
        public override int GetHashCode()
            => ToString().GetHashCode(StringComparison.OrdinalIgnoreCase);


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => $"{nameof(EditorId)}={EditorId},{nameof(Name)}={Name}";


        /// <summary>
        /// 获取唯一索引表达式。
        /// </summary>
        /// <typeparam name="TEditorTitle">指定的编者头衔类型。</typeparam>
        /// <param name="editorId">给定的编者标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        public static Expression<Func<TEditorTitle, bool>> GetUniqueIndexExpression<TEditorTitle>(TEditorId editorId, string name)
            where TEditorTitle : PortalEditorTitle<TIncremId, TEditorId>
        {
            name.NotEmpty(nameof(name));

            return p => p.EditorId.Equals(editorId) && p.Name == name;
        }
    }
}
