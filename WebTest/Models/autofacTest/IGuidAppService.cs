﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Models.autofacTest
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
