using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.Extensions.Portal.Tests
{
    using Core.Services;

    public class TestStoreHubTests
    {
        [Fact]
        public void AllTest()
        {
            var stores = TestServiceProvider.Current.GetRequiredService<TestStoreHub>();

            var editors = stores.GetEditors();
            Assert.NotEmpty(editors);

            var internalUsers = stores.GetInternalUsers();
            Assert.NotEmpty(internalUsers);
        }

    }
}
