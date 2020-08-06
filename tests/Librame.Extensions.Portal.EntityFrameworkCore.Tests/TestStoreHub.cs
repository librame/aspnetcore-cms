using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.Extensions.Portal.Tests
{
    using Data.Accessors;
    using Data.Stores;
    using Portal.Stores;

    public class TestStoreHub : PortalStoreHub
    {
        public TestStoreHub(IStoreInitializer initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
        }


        public IList<PortalEditor<Guid, Guid, Guid>> GetEditors()
            => Accessor.Editors.ToList();

        public IList<PortalInternalUser<Guid, Guid>> GetInternalUsers()
            => Accessor.InternalUsers.ToList();
    }
}
