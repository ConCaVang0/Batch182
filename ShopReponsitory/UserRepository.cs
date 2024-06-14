using ShopBusiness.Models;
using ShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public class UserRepository : IUserRepository
    {
        public void Add(User user)
        {
            UserDAO.Instance.Add(user);
        }

        public void Delete(int id)
        {
            UserDAO.Instance.Delete(id);
        }

        public IEnumerable<User> GetAllUser()
        {
            return UserDAO.Instance.GetUserAll();
        }

        public User GetUserById(int id)
        {
            return UserDAO.Instance.GetUserById(id);
        }
        public void Update(User user)   
        {
            UserDAO.Instance.Update(user);
        }
        public bool ChangeStatus(int id)
        {


            return UserDAO.Instance.ChangeStatus(id);
        }

        public User GetUserByUserNamePassword(string userName, string password) =>  
            UserDAO.Instance.GetUserByUserNamePassword(userName, password);



    }



}
