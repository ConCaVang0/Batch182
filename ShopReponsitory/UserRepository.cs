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
        public async Task Add(User user)
        {
            await UserDAO.Instance.Add(user);
        }

        public async Task Delete(int id)
        {
            await UserDAO.Instance.Delete(id);
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await UserDAO.Instance.GetUserAll();
        }

        public async Task<User> GetUserById(int id)
        {
            return await UserDAO.Instance.GetUserById(id);
        }
        public async Task Update(User user)
        {
            await UserDAO.Instance.Update(user);
        }
        public async Task<bool> ChangeStatus(int id)
        {
            return await UserDAO.Instance.ChangeStatus(id);
        }
        public async Task<User> GetUserByUserNamePassword(string userName, string password) =>
            await UserDAO.Instance.GetUserByUserNamePassword(userName, password);
    }
}