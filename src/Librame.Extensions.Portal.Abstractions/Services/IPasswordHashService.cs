#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Portal.Services
{
    /// <summary>
    /// 密码哈希服务接口。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    public interface IPasswordHashService<TUser>
        where TUser : class
    {
        /// <summary>
        /// 哈希指定用户的密码。
        /// </summary>
        /// <param name="user">给定的 <typeparamref name="TUser"/>。</param>
        /// <param name="password">给定的密码。</param>
        /// <returns>返回字符串。</returns>
        string HashPassword(TUser user, string password);

        /// <summary>
        /// 验证经过哈希的密码。
        /// </summary>
        /// <param name="user">给定的 <typeparamref name="TUser"/>。</param>
        /// <param name="hashedPassword">给定经过哈希的密码。</param>
        /// <param name="providedPassword">给定的原始密码。</param>
        /// <returns>返回布尔值。</returns>
        bool VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword);
    }
}
