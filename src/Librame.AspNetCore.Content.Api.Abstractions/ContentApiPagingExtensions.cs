#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Linq;

namespace Librame.Extensions.Data.Collections
{
    /// <summary>
    /// 内容 API 分页静态扩展。
    /// </summary>
    public static class ContentApiPagingExtensions
    {
        /// <summary>
        /// 投影分页。
        /// </summary>
        /// <typeparam name="TSource">指定的来源类型。</typeparam>
        /// <typeparam name="TResult">指定的结果类型。</typeparam>
        /// <param name="sources">给定的来源 <see cref="IPageable{TSource}"/>。</param>
        /// <param name="selector">给定的投影选择器。</param>
        /// <returns>返回 <see cref="IPageable{TResult}"/>。</returns>
        public static IPageable<TResult> SelectPaging<TSource, TResult>(this IPageable<TSource> sources,
            Func<TSource, TResult> selector)
        {
            if (sources.IsNull())
                return null;

            return new PagingCollection<TResult>(sources.Select(selector).ToList(), sources.Descriptor);
        }

        /// <summary>
        /// 投影分页。
        /// </summary>
        /// <typeparam name="TSource">指定的来源类型。</typeparam>
        /// <typeparam name="TResult">指定的结果类型。</typeparam>
        /// <param name="sources">给定的来源 <see cref="IPageable{TSource}"/>。</param>
        /// <param name="selector">给定的投影选择器。</param>
        /// <returns>返回 <see cref="IPageable{TResult}"/>。</returns>
        public static IPageable<TResult> SelectPaging<TSource, TResult>(this IPageable<TSource> sources,
            Func<TSource, int, TResult> selector)
        {
            if (sources.IsNull())
                return null;

            return new PagingCollection<TResult>(sources.Select(selector).ToList(), sources.Descriptor);
        }

    }
}
