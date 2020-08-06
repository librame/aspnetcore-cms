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
using System.Diagnostics.CodeAnalysis;

namespace Librame.Extensions.Portal.Services
{
    /// <summary>
    /// 密码哈希服务。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    public class PasswordHashService<TUser> : IPasswordHashService<TUser>
        where TUser : class
    {
        /// <summary>
        /// 获取密码哈希。
        /// </summary>
        /// <param name="user">给定的 <typeparamref name="TUser"/>。</param>
        /// <param name="password">给定的密码。</param>
        /// <returns>返回字符串。</returns>
        public string HashPassword(TUser user, string password)
            => ToPasswordBuffer(password).AsAes().AsBase64String();

        /// <summary>
        /// 验证密码哈希。
        /// </summary>
        /// <param name="user">给定的 <typeparamref name="TUser"/>。</param>
        /// <param name="hashedPassword">给定经过哈希的密码。</param>
        /// <param name="providedPassword">给定的原始密码。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public bool VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            user.NotNull(nameof(user));

            var decrypted = hashedPassword
                .FromBase64String()
                .FromAes()
                .AsBase64String();

            var hashed = ToPasswordBuffer(providedPassword)
                .AsBase64String();

            return decrypted.Equals(hashed, StringComparison.Ordinal);
        }


        private static byte[] ToPasswordBuffer(string password)
            => password.FromEncodingString().HmacSha256();

    }
}
