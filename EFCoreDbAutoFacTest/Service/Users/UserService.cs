using EFCoreDbAutoFacTest.Core.Data;
using EFCoreDbAutoFacTest.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreDbAutoFacTest.Service.Users
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

        public virtual User Add(User user)
        {
            _userRepository.Insert(user);
            return user;
        }

        public virtual User Get(int id)
        {
            return _userRepository.GetById(id);
        }
    }
}
