#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Content.Stores
{
    using Data.Stores;

    /// <summary>
    /// 内容存储中心接口。
    /// </summary>
    /// <typeparam name="TCategory">指定的内容分类类型。</typeparam>
    /// <typeparam name="TSource">指定的内容来源类型。</typeparam>
    /// <typeparam name="TClaim">指定的内容声明类型。</typeparam>
    /// <typeparam name="TTag">指定的内容标签类型。</typeparam>
    /// <typeparam name="TUnit">指定的内容单元类型。</typeparam>
    /// <typeparam name="TUnitClaim">指定的内容单元声明类型。</typeparam>
    /// <typeparam name="TUnitTag">指定的内容单元标签类型。</typeparam>
    /// <typeparam name="TUnitVisitCount">指定的内容单元访问计数类型。</typeparam>
    /// <typeparam name="TPane">指定的内容窗格类型。</typeparam>
    /// <typeparam name="TPaneUnit">指定的内容单元类型。</typeparam>
    public interface IContentStoreHub<TCategory, TSource, TClaim, TTag, TUnit, TUnitClaim, TUnitTag, TUnitVisitCount, TPane, TPaneUnit>
        : IStoreHub,
        ICategoryStore<TCategory>,
        ISourceStore<TSource>,
        IClaimStore<TClaim>,
        ITagStore<TTag>,
        IUnitStore<TUnit, TUnitClaim, TUnitTag, TUnitVisitCount>,
        IPaneStore<TPane, TPaneUnit>
        where TCategory : class
        where TSource : class
        where TClaim : class
        where TTag : class
        where TUnit : class
        where TUnitClaim : class
        where TUnitTag : class
        where TUnitVisitCount : class
        where TPane : class
        where TPaneUnit : class
    {
    }
}
