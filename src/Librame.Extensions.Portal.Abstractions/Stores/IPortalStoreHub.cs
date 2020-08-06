#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Portal.Stores
{
    using Data.Stores;

    /// <summary>
    /// 门户存储中心接口。
    /// </summary>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TInternalUser">指定的内置用户类型。</typeparam>
    public interface IPortalStoreHub<TEditor, TInternalUser> : IStoreHub,
        IEditorStore<TEditor>, IInternalUserStore<TInternalUser>
        where TEditor : class
        where TInternalUser : class
    {
    }
}
