using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace Librame.Extensions.Portal.Tests
{
    using Core.Services;
    using Data.Builders;

    public class TestStoreHubTests
    {
        [Fact]
        public void AllTest()
        {
            var stores = TestServiceProvider.Current.GetRequiredService<TestStoreHub>();
            var dependency = TestServiceProvider.Current.GetRequiredService<DataBuilderDependency>();

            // Content
            var categories = stores.GetCategories();
            VerifyDefaultData(categories);

            categories = stores.UseWriteDbConnection().GetCategories();
            Assert.NotEmpty(categories);

            var sources = stores.GetSources();
            VerifyDefaultData(sources);

            sources = stores.UseWriteDbConnection().GetSources();
            Assert.NotEmpty(sources);

            var claims = stores.GetClaims();
            VerifyDefaultData(claims);

            claims = stores.UseWriteDbConnection().GetClaims();
            Assert.NotEmpty(claims);

            var tags = stores.GetTags();
            VerifyDefaultData(tags);

            tags = stores.UseWriteDbConnection().GetTags();
            Assert.NotEmpty(tags);

            var units = stores.GetUnits();
            VerifyDefaultData(units);

            units = stores.UseWriteDbConnection().GetUnits();
            Assert.NotEmpty(units);

            var unitClaims = stores.GetUnitClaims();
            VerifyDefaultData(unitClaims);

            unitClaims = stores.UseWriteDbConnection().GetUnitClaims();
            Assert.NotEmpty(unitClaims);

            var unitTags = stores.GetUnitTags();
            VerifyDefaultData(unitTags);

            unitTags = stores.UseWriteDbConnection().GetUnitTags();
            Assert.NotEmpty(unitTags);

            var unitVisitCounts = stores.GetUnitVisitCounts();
            VerifyDefaultData(unitVisitCounts);

            unitVisitCounts = stores.UseWriteDbConnection().GetUnitVisitCounts();
            Assert.NotEmpty(unitVisitCounts);

            var panes = stores.GetPanes();
            VerifyDefaultData(panes);

            panes = stores.UseWriteDbConnection().GetPanes();
            Assert.NotEmpty(panes);

            var paneUnits = stores.GetPaneUnits();
            VerifyDefaultData(paneUnits);

            paneUnits = stores.UseWriteDbConnection().GetPaneUnits();
            Assert.NotEmpty(paneUnits);

            // Portal
            var editors = stores.GetEditors();
            VerifyDefaultData(editors);

            editors = stores.UseWriteDbConnection().GetEditors();
            Assert.NotEmpty(editors);

            var internalUsers = stores.GetInternalUsers();
            VerifyDefaultData(internalUsers);

            internalUsers = stores.UseWriteDbConnection().GetInternalUsers();
            Assert.NotEmpty(internalUsers);


            void VerifyDefaultData<TEntity>(IEnumerable<TEntity> items)
                where TEntity : class
            {
                Assert.True(dependency.Options.DefaultTenant.DataSynchronization
                    ? items.IsNotEmpty()
                    : items.IsEmpty());
            }
        }

    }
}
