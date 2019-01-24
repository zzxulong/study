using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest2.Models.Services
{
    public class GuidAppServiceImpl
    {
        private readonly Guid _item;
        public GuidAppServiceImpl()
        {
            _item= Guid.NewGuid();
        }
        public Guid GuidItem()
        {
            return _item;
        }

    }
    public class GuidTransientAppServiceImpl : GuidAppServiceImpl, IGuidTransientAppService
    {
    }

    public class GuidScopedAppServiceImpl : GuidAppServiceImpl, IGuidScopedAppService
    {
    }

    public class GuidSingletonAppServiceImpl : GuidAppServiceImpl, IGuidSingletonAppService
    {
    }
}
