using Microsoft.EntityFrameworkCore;
using ShopBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDataAccess
{
    public class RoleDAO : SingletonBase<RoleDAO>
    {
        /// <summary>
        /// GET ALL
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Role>> GetRoleAll()
        {
            return await _context.Roles.ToListAsync();
        }

        /// <summary>
        /// GET BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Role> GetRoleById(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(c => c.RoleId == id);
            if (role == null) return null;

            return role;
        }
        public async Task Add(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Role role)
        {
            var existingItem = await GetRoleById(role.RoleId);
            if (existingItem != null)
            {
                // Cập nhật các thuộc tính cần thiết
                _context.Entry(existingItem).CurrentValues.SetValues(role);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var role = await GetRoleById(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
