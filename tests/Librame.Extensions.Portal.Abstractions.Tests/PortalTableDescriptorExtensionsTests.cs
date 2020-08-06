using System;
using Xunit;

namespace Librame.Extensions.Portal.Tests
{
    using Data;
    using Portal.Stores;

    public class PortalTableDescriptorExtensionsTests
    {
        [Fact]
        public void InsertContentPrefixTest()
        {
            var table = TableDescriptor.Create<PortalEditor<Guid, Guid, Guid>>();
            Assert.Equal("Portal_Editors", table.InsertPortalPrefix().Name);

            table = TableDescriptor.Create<PortalInternalUser<Guid, Guid>>();
            Assert.Equal("Portal_InternalUsers", table.InsertPortalPrefix().Name);
        }

    }
}
