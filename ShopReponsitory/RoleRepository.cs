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
        public void Add(Role role)
        {
            RoleDAO.Instance.Add(role);
        }

        public void Delete(int id)
        {
            RoleDAO.Instance.Delete(id);
        }

        public IEnumerable<Role> GetAllRole()
        {
            return RoleDAO.Instance.GetRoleAll();
        }

        public Role GetRoleById(int id)
        {
            return RoleDAO.Instance.GetRoleById(id);
        }
        public void Update(Role role)
        {
            RoleDAO.Instance.Update(role);
        }
     
    }
}
