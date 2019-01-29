using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTest.Core;
using WebTest.DAL;

namespace WebTest.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        #region Ctor

        public UserService(IRepository<User> userRepository)
        {
            this._userRepository = userRepository;
        }

        #endregion

        public virtual IList<User> GetAll()
        {
            var query = _userRepository.Table;
           
            query = query.OrderByDescending(c => c.CreatedOnUtc);
            return query.ToList(); 
        }
    }
}
