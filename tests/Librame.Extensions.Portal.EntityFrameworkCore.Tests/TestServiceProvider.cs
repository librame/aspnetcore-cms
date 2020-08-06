using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Design.Internal;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Librame.Extensions.Portal.Tests
{
    using Core.Identifiers;
    using Portal.Accessors;
    using Portal.Stores;

    internal static class TestServiceProvider
    {
        static TestServiceProvider()
        {
            Current = Current.EnsureSingleton(() =>
            {
                var services = new ServiceCollection();

                services.AddLibrame()
                    //.AddEncryption().AddGlobalSigningCredentials() // AddIdentity() Default: AddDeveloperGlobalSigningCredentials()
                    .AddData(dependency =>
                    {
                        dependency.Options.IdentifierGenerator = CombIdentifierGenerator.MySQL;

                        // Use MySQL
                        dependency.Options.DefaultTenant.DefaultConnectionString
                            = MySqlConnectionStringHelper.Validate("server=localhost;port=3306;database=librame_portal_default;user=root;password=123456;");
                        dependency.Options.DefaultTenant.WritingConnectionString
                            = MySqlConnectionStringHelper.Validate("server=localhost;port=3306;database=librame_portal_writing;user=root;password=123456;");

                        //dependency.Options.DefaultTenant.WritingSeparation = true;
                    })
                    .AddAccessor<PortalDbContextAccessor>((tenant, optionsBuilder) =>
                    {
                        optionsBuilder.UseMySql(tenant.DefaultConnectionString, mySql =>
                        {
                            mySql.MigrationsAssembly(typeof(PortalDbContextAccessor).GetAssemblyDisplayName());
                            mySql.ServerVersion(new Version(5, 7, 28), ServerType.MySql);
                        });
                    })
                    .AddDatabaseDesignTime<MySqlDesignTimeServices>()
                    .AddStoreHub<TestStoreHub>()
                    .AddStoreIdentifierGenerator<GuidPortalStoreIdentifierGenerator>()
                    .AddStoreInitializer<GuidPortalStoreInitializer>()
                    .AddInternalUserPortal(dependency =>
                    {
                        dependency.Options.Stores.MaxLengthForProperties = 128;
                    });

                return services.BuildServiceProvider();
            });
        }

        public static IServiceProvider Current { get; }
    }
}
