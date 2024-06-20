using ShopBusiness.Models;
using ShopDataAccess;
using ShopRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopReponsitory
{
    public class RoleRepository : IRoleRepository
    {
        public async Task Add(Role role)
        {
            await RoleDAO.Instance.Add(role);
        }

        public async Task Delete(int id)
        {
            await RoleDAO.Instance.Delete(id);
        }

        public async Task<IEnumerable<Role>> GetAllRole()
        {
            return await RoleDAO.Instance.GetRoleAll();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await RoleDAO.Instance.GetRoleById(id);
        }
        public async Task Update(Role role)
        {
            await RoleDAO.Instance.Update(role);
        }
    }
}
