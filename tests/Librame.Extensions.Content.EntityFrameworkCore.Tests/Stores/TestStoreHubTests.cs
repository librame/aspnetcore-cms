using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.Extensions.Content.Tests
{
    using Core.Services;

    public class TestStoreHubTests
    {
        [Fact]
        public void AllTest()
        {
            var stores = TestServiceProvider.Current.GetRequiredService<TestStoreHub>();

            var categories = stores.GetCategories();
            Assert.NotEmpty(categories);

            var sources = stores.GetSources();
            Assert.NotEmpty(sources);

            var claims = stores.GetClaims();
            Assert.NotEmpty(claims);

            var tags = stores.GetTags();
            Assert.NotEmpty(tags);

            var units = stores.GetUnits();
            Assert.NotEmpty(units);

            var unitClaims = stores.GetUnitClaims();
            Assert.NotEmpty(unitClaims);

            var unitTags = stores.GetUnitTags();
            Assert.NotEmpty(unitTags);

            var unitVisitCounts = stores.GetUnitVisitCounts();
            Assert.NotEmpty(unitVisitCounts);

            var panes = stores.GetPanes();
            Assert.NotEmpty(panes);

            var paneUnits = stores.GetPaneUnits();
            Assert.NotEmpty(paneUnits);
        }

    }
}
