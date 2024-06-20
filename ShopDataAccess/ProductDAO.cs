using Microsoft.EntityFrameworkCore;
using ShopBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDataAccess
{
    public class ProductDAO : SingletonBase<ProductDAO>
    {
        /// <summary>
        /// GET ALL
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProductAll()
        {
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// GET BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.ProductId == id);
            if (product == null) return null;

            return product;
        }
        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Product product)
        {
            var existingItem = await GetProductById(product.CategoryId);
            if (existingItem != null)
            {
                // Cập nhật các thuộc tính cần thiết
                _context.Entry(existingItem).CurrentValues.SetValues(product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var product = await GetProductById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ChangeStatus(int id)
        {
            var product = await GetProductById(id);
            product.Status = !product.Status;
            await _context.SaveChangesAsync();
            return product.Status;
        }
    }
}