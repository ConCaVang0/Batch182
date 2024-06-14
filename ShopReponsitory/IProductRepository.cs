using ShopBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProduct();
        Product GetProductById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        bool ChangeStatus(int id);
    }


}
