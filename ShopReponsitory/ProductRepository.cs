
using ShopBusiness.Models;
using ShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepository
{
    public class ProductRepository : IProductRepository
    {
        public void Add(Product product)
        {
            ProductDAO.Instance.Add(product);
        }

        public void Delete(int id)
        {
            ProductDAO.Instance.Delete(id);
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return ProductDAO.Instance.GetProductAll();
        }

        public Product GetProductById(int id)
        {
            return ProductDAO.Instance.GetProductById(id);
        }
        public void Update(Product product)
        {
            ProductDAO.Instance.Update(product);
        }
        public bool ChangeStatus(int id)
        {
            return ProductDAO.Instance.ChangeStatus(id);
        }
    }
}
