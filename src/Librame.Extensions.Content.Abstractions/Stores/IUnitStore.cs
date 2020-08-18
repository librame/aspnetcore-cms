#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Linq;

namespace Librame.Extensions.Content.Stores
{
    using Data.Stores;

    /// <summary>
    /// 单元存储接口。
    /// </summary>
    /// <typeparam name="TUnit">指定的单元类型。</typeparam>
    /// <typeparam name="TUnitClaim">指定的单元声明类型。</typeparam>
    /// <typeparam name="TUnitTag">指定的单元标签类型。</typeparam>
    /// <typeparam name="TUnitVisitCount">指定的单元访问计数。</typeparam>
    public interface IUnitStore<TUnit, TUnitClaim, TUnitTag, TUnitVisitCount> : IStore
        where TUnit : class
        where TUnitClaim : class
        where TUnitTag : class
        where TUnitVisitCount : class
    {
        /// <summary>
        /// 单元查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUnit}"/>。</value>
        IQueryable<TUnit> Units { get; }

        /// <summary>
        /// 单元声明查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUnitClaim}"/>。</value>
        IQueryable<TUnitClaim> UnitClaims { get; }

        /// <summary>
        /// 单元标签查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUnitTag}"/>。</value>
        IQueryable<TUnitTag> UnitTags { get; }

        /// <summary>
        /// 单元统计数据查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUnitVisitCount}"/>。</value>
        IQueryable<TUnitVisitCount> UnitVisitCounts { get; }
    }
}
