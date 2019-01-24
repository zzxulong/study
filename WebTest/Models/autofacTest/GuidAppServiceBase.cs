using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Models.autofacTest
{
    public class GuidAppServiceBase: IGuidAppService
    {
        private readonly Guid _item;

        public GuidAppServiceBase()
        {
            _item = Guid.NewGuid();
        }

        public Guid GuidItem()
        {
            return _item;
        }
    }
    public class GuidTransientAppService : GuidAppServiceBase, IGuidTransientAppService
    {
    }

    public class GuidScopedAppService : GuidAppServiceBase, IGuidScopedAppService
    {
    }

    public class GuidSingletonAppService : GuidAppServiceBase, IGuidSingletonAppService
    {
    }
}
