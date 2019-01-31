using EFCoreDbAutoFacTest.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreDbAutoFacTest.Service.Users
{
    public interface IUserService
    {
        IList<User> GetAll();
        User Add(User user);
        User Get(int id);
    }
}
