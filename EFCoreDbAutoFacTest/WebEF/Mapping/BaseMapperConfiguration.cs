using AutoMapper;
using EFCoreDbAutoFacTest.Core.Infrastructure;
using EFCoreDbAutoFacTest.Models.Users;
using EFCoreDbAutoFacTest.WebEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreDbAutoFacTest.WebEF.Mapping
{
    public class BaseMapperConfiguration : Profile, IOrderedMapperProfile
    {
        public int Order => 0;
        public BaseMapperConfiguration()
        {
            CreateMap<User, UserModel>();
        }
    }
}
