using Librame.Extensions;
using Librame.Extensions.Content.Accessors;
using Librame.Extensions.Content.Stores;
using Librame.Extensions.Core.Identifiers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Design.Internal;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorExample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddLibrame()
                //.AddEncryption().AddGlobalSigningCredentials() // AddIdentity() Default: AddDeveloperGlobalSigningCredentials()
                .AddData(dependency =>
                {
                    dependency.Options.IdentifierGenerator = CombIdentifierGenerator.MySQL;

                    // Use MySQL
                    dependency.Options.DefaultTenant.DefaultConnectionString
                        = MySqlConnectionStringHelper.Validate("server=localhost;port=3306;database=librame_content_default;user=root;password=123456;");
                    dependency.Options.DefaultTenant.WritingConnectionString
                        = MySqlConnectionStringHelper.Validate("server=localhost;port=3306;database=librame_content_writing;user=root;password=123456;");

                    //dependency.Options.DefaultTenant.WritingSeparation = true;
                })
                .AddAccessor<ContentDbContextAccessor>((tenant, optionsBuilder) =>
                {
                    optionsBuilder.UseMySql(tenant.DefaultConnectionString, mySql =>
                    {
                        mySql.MigrationsAssembly(typeof(ContentDbContextAccessor).GetAssemblyDisplayName());
                        mySql.ServerVersion(new Version(5, 7, 28), ServerType.MySql);
                    });
                })
                .AddDatabaseDesignTime<MySqlDesignTimeServices>()
                .AddStoreHub<ContentStoreHub>()
                .AddStoreIdentifierGenerator<GuidContentStoreIdentifierGenerator>()
                .AddStoreInitializer<GuidContentStoreInitializer>()
                .AddContent(dependency =>
                {
                    dependency.Options.Stores.MaxLengthForProperties = 128;
                });

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }

    }
}
