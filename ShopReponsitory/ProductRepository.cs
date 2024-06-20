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
        public async Task Add(Product product)
        {
            await ProductDAO.Instance.Add(product);
        }

        public async Task Delete(int id)
        {
            await ProductDAO.Instance.Delete(id);
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await ProductDAO.Instance.GetProductAll();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await ProductDAO.Instance.GetProductById(id);
        }
        public async Task Update(Product category)
        {
            await ProductDAO.Instance.Update(category);
        }
        public async Task<bool> ChangeStatus(int id)
        {
            return await ProductDAO.Instance.ChangeStatus(id);
        }
    }
}