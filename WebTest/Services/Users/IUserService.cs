using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTest.DAL;

namespace WebTest.Services.Users
{
    public interface IUserService
    {
        IList<User> GetAll();
    }
}
