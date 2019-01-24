using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest2.Models.Services
{
    public interface IGuidAppService
    {
        Guid GuidItem();
    }
    public interface IGuidTransientAppService : IGuidAppService
    {
    }

    public interface IGuidScopedAppService : IGuidAppService
    {
    }

    public interface IGuidSingletonAppService : IGuidAppService
    {
    }
}
