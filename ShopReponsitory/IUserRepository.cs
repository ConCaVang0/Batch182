using ShopBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(int id);
        Task Add(User user);
        Task Update(User user);
        Task Delete(int id);
        Task<bool> ChangeStatus(int id);
        Task<User> GetUserByUserNamePassword(string userName, string password);
    }


}
