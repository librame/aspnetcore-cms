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
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Content.Stores
{
    using Content.Resources;
    using Data.Stores;

    /// <summary>
    /// 单元访问计数。
    /// </summary>
    /// <typeparam name="TUnitId">指定的单元标识类型。</typeparam>
    [Description("单元访问计数")]
    public class ContentUnitVisitCount<TUnitId> : AbstractVisitUserCount<long>
        where TUnitId : IEquatable<TUnitId>
    {
        /// <summary>
        /// 单元标识。
        /// </summary>
        [Display(Name = nameof(UnitId), ResourceType = typeof(AbstractContentResource))]
        public virtual TUnitId UnitId { get; set; }


        /// <summary>
        /// 累减计数。
        /// </summary>
        /// <param name="count">给定要累减的计数。</param>
        /// <returns>返回长整数。</returns>
        public override long DegressiveCount(long count)
            => --count;

        /// <summary>
        /// 异步累减计数。
        /// </summary>
        /// <param name="count">给定要累减的计数。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含已累减计数的异步操作。</returns>
        public override ValueTask<long> DegressiveCountAsync(long count,
            CancellationToken cancellationToken = default)
            => cancellationToken.RunOrCancelValueAsync(() => --count);


        /// <summary>
        /// 累加计数。
        /// </summary>
        /// <param name="count">给定要累加的计数。</param>
        /// <returns>返回长整数。</returns>
        public override long ProgressiveCount(long count)
            => ++count;

        /// <summary>
        /// 异步累加计数。
        /// </summary>
        /// <param name="count">给定要累加的计数。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含已累加计数的异步操作。</returns>
        public override ValueTask<long> ProgressiveCountAsync(long count,
            CancellationToken cancellationToken = default)
            => cancellationToken.RunOrCancelValueAsync(() => ++count);

    }
}
