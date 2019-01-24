using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest2.Models.Services
{
    public class GuidAppServiceImpl
    {
        public Guid GuidItem()
        {
            return Guid.NewGuid();
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
