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
        // GET ALL
        public IEnumerable<User> GetUserAll()
        {
            return _context.Users.AsNoTrackingWithIdentityResolution().ToList();
        }
        //+ GET BY ID
        public User GetUserById(int id)
        {
            var user = _context.Users.AsNoTrackingWithIdentityResolution().FirstOrDefault(c => c.UserId == id);
            if (user == null) return null;

            return user;
        }
        //+ ADD
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        //+ UPDATE
        public void Update(User user)
        {
            _context = new ShopBatch182Context();
            var existingItem = GetUserById(user.UserId);
            if (existingItem != null)
            {
                // Cập nhật các thuộc tính cần thiết
                _context.Entry(existingItem).CurrentValues.SetValues(user);
            }
            else
            {
                // Thêm thực thể mới nếu nó chưa tồn tại
                _context.Users.Add(user);
            }
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        //+ DELETE
        public void Delete(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        //+ Get List By Name
        public IEnumerable<User> GetUserByName(string name)
        {
            return _context.Users.Where(u => u.UserName.Contains(name)).ToList();
        }
        //+ Change Status
        public bool ChangeStatus(int id)
        {
            var user = GetUserById(id);
            user.Status = !user.Status;
            _context.SaveChanges();
            return user.Status;
        }

        public User GetUserByUserNamePassword(string userName , string password)
        {
            var user = _context.Users.FirstOrDefault(c => c.UserName.Equals(userName) && c.Password.Equals(password));
            if (user == null) return null;

            return user;
        }

    }
}