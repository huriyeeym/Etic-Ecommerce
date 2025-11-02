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

        public IList<User> GetAll()
        {
            return _userDal.GetAll().Where(u => !u.IsDeleted).ToList();
        }

        public User GetById(int id)
        {
            return _userDal.Get(x => x.Id == id);
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public void Update(User user)
        {
            _userDal.Update(user);
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            if (user != null)
            {
                user.IsDeleted = true;
                user.DeletedDate = DateTime.Now;
                _userDal.Update(user);
            }
        }

        public int GetTotalCount()
        {
            return _userDal.GetAll().Where(u => !u.IsDeleted).Count();
        }
    }
    public interface IUserService
    {
        string ChangeGuidKey(User user);
        User GetUserDataWithGuidKey(string guid);
        void ResetUserGuidKey(string guid);
        IList<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        int GetTotalCount();
    }
}
