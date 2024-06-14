﻿using ShopBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUser();
        User GetUserById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        bool ChangeStatus(int id);
        User GetUserByUserNamePassword(string userName, string password);

    }


}
