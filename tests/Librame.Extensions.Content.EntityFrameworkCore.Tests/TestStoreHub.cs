using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.Extensions.Content.Tests
{
    using Content.Stores;
    using Data.Accessors;
    using Data.Collections;
    using Data.Stores;

    public class TestStoreHub : ContentStoreHub
    {
        public TestStoreHub(IStoreInitializer initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
        }


        public IList<ContentCategory<int, Guid>> GetCategories()
            => Accessor.Categories.ToList();

        public IList<ContentSource<int, Guid>> GetSources()
            => Accessor.Sources.ToList();

        public IList<ContentClaim<int, Guid>> GetClaims()
            => Accessor.Claims.ToList();

        public IList<ContentTag<int, Guid>> GetTags()
            => Accessor.Tags.ToList();

        public IPageable<ContentUnit<Guid, int, int, Guid>> GetUnits()
            => Accessor.Units.AsPagingByIndex(ordered => ordered.OrderBy(k => k.Id), 1, 10);

        public IList<ContentUnitClaim<int, Guid, int, Guid>> GetUnitClaims()
            => Accessor.UnitClaims.ToList();

        public IList<ContentUnitTag<int, Guid, int>> GetUnitTags()
            => Accessor.UnitTags.ToList();

        public IList<ContentUnitVisitCount<Guid>> GetUnitVisitCounts()
            => Accessor.UnitVisitCounts.ToList();

        public IList<ContentPane<int, Guid>> GetPanes()
            => Accessor.Panes.ToList();

        public IList<ContentPaneUnit<int, int, Guid, Guid>> GetPaneUnits()
            => Accessor.PaneUnits.ToList();
    }
}
