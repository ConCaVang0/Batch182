using Microsoft.EntityFrameworkCore;
using ShopBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDataAccess
{
    public class UserDAO : SingletonBase<UserDAO>
    {
        /// <summary>
        /// GET ALL
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetUserAll()
        {
            return await _context.Users.Include(r => r.Role).ToListAsync();
        }

        /// <summary>
        /// GET BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.UserId == id);
            if (user == null) return null;

            return user;
        }
        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task Update(User user)
        {
            var existingItem = await GetUserById(user.UserId);
            if (existingItem != null)
            {
                // Cập nhật các thuộc tính cần thiết
                _context.Entry(existingItem).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var user = await GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ChangeStatus(int id)
        {
            var user = await GetUserById(id);
            user.Status = !user.Status;
            await _context.SaveChangesAsync();
            return user.Status;
        }

        public async Task<User> GetUserByUserNamePassword(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.UserName.Equals(userName) && c.Password.Equals(password));
            if (user == null) return null;
            return user;
        }
    }
}