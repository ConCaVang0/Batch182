using Microsoft.EntityFrameworkCore;
using ShopBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDataAccess
{
    public class CategoryDAO : SingletonBase<CategoryDAO>
    {
        /// <summary>
        /// GET ALL
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> GetCategoryAll()
        {
            return await _context.Categories.ToListAsync();
        }

        /// <summary>
        /// GET BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            return category;
        }
        public async Task Add(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Category category)
        {
            var existingItem = await GetCategoryById(category.CategoryId);
            if (existingItem != null)
            {
                // Cập nhật các thuộc tính cần thiết
                _context.Entry(existingItem).CurrentValues.SetValues(category);
                await _context.SaveChangesAsync();
            }
            //_context.Categories.Update(existingItem);

        }
        public async Task Delete(int id)
        {
            var category = await GetCategoryById(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<Category> GetCategoryByName(string name)
        {
            return _context.Categories.Where(u => u.CategoryName.Contains(name)).ToList();
        }

        public async Task<bool> ChangeStatus(int id)
        {
            var category = await GetCategoryById(id);
            if (category != null)
            {
                category.Status = !category.Status;
                await _context.SaveChangesAsync();
                return category.Status;
            }
            return false;
        }
    }
}