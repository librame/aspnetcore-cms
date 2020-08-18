#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Update;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using System.Linq;

namespace Pomelo.EntityFrameworkCore.MySql.Update.Internal
{
    public class MySqlModificationCommandBatchFactoryRewrite : IModificationCommandBatchFactory
    {
        private readonly ModificationCommandBatchFactoryDependencies _dependencies;
        private readonly IDbContextOptions _options;

        public MySqlModificationCommandBatchFactoryRewrite(
            ModificationCommandBatchFactoryDependencies dependencies,
            IDbContextOptions options)
        {
            _dependencies = dependencies;
            _options = options;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual ModificationCommandBatch Create()
        {
            var optionsExtension = _options.Extensions.OfType<MySqlOptionsExtension>().FirstOrDefault();

            return new MySqlModificationCommandBatchRewrite(_dependencies, optionsExtension?.MaxBatchSize);
        }
    }
}
