using System;
using Xunit;

namespace Librame.Extensions.Content.Tests
{
    using Content.Stores;
    using Data;

    public class ContentTableDescriptorExtensionsTests
    {
        [Fact]
        public void InsertContentPrefixTest()
        {
            var table = TableDescriptor.Create<ContentCategory<int, Guid>>();
            Assert.Equal("Content_Categories", table.InsertContentPrefix().Name);

            table = TableDescriptor.Create<ContentSource<int, Guid>>();
            Assert.Equal("Content_Sources", table.InsertContentPrefix().Name);

            table = TableDescriptor.Create<ContentClaim<int, int, Guid>>();
            Assert.Equal("Content_Claims", table.InsertContentPrefix().Name);

            table = TableDescriptor.Create<ContentTag<int, Guid>>();
            Assert.Equal("Content_Tags", table.InsertContentPrefix().Name);

            table = TableDescriptor.Create<ContentUnit<Guid, int, int, int, Guid>>();
            Assert.Equal("Content_Units", table.InsertContentPrefix().Name);

            table = TableDescriptor.Create<ContentUnitClaim<int, Guid, int, Guid>>();
            Assert.Equal("Content_UnitClaims", table.InsertContentPrefix().Name);

            table = TableDescriptor.Create<ContentUnitTag<int, Guid, int>>();
            Assert.Equal("Content_UnitTags", table.InsertContentPrefix().Name);

            table = TableDescriptor.Create<ContentUnitVisitCount<Guid>>();
            Assert.Equal("Content_UnitVisitCounts", table.InsertContentPrefix().Name);

            table = TableDescriptor.Create<ContentPane<int, Guid>>();
            Assert.Equal("Content_Panes", table.InsertContentPrefix().Name);

            table = TableDescriptor.Create<ContentPaneClaim<int, int, int, Guid>>();
            Assert.Equal("Content_PaneUnits", table.InsertContentPrefix().Name);
        }

    }
}
