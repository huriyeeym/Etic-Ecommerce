using Etic.Data.Abstract;
using Etic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public string ChangeGuidKey(User user)
        {
            var key = Guid.NewGuid().ToString();
            user.LoginGuidKey = key;
            _userDal.Update(user);
            return key;
        }
        public User GetUserDataWithGuidKey(string guid)
        {
            return _userDal.Get(UserTablosu => UserTablosu.LoginGuidKey == guid);
        }

        public void ResetUserGuidKey(string guid)
        {
            var user = GetUserDataWithGuidKey(guid);
            user.LoginGuidKey = null;
            _userDal.Update(user);  
        }
    }
    public interface IUserService
    {
        string ChangeGuidKey(User user);
        User GetUserDataWithGuidKey(string guid);
        void ResetUserGuidKey(string guid);
    }
}
