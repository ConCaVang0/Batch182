using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopBusiness.Models;
using ShopRepository;

namespace DemoWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : BaseController
    {
        ICategoryRepository _categoryRepository;
        public CategoryController()
        {
            _categoryRepository = new CategoryRepository();
        }

        // GET: Admin/Category
        public async Task<IActionResult> Index(string searchString)
        {
            var category = await _categoryRepository.GetAllCategory();
            if (!string.IsNullOrEmpty(searchString))
            {
                category = category.Where(c => ShopCommon.Library.ConvertToUnSign(c.CategoryName.ToLower()).Contains(ShopCommon.Library.ConvertToUnSign(searchString.ToLower())));
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                SetAlert(ShopCommon.Contants.UPDATE_SUCCESS, ShopCommon.Contants.SUCCESS);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var category = await _categoryRepository.GetCategoryById(Convert.ToInt32(id));
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Status")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                SetAlert(ShopCommon.Contants.UPDATE_SUCCESS, ShopCommon.Contants.SUCCESS);
                _categoryRepository.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpPost]
        public JsonResult DeleteId(int id)
        {
            try
            {
                var category = _categoryRepository.GetCategoryById(id);
                if (category == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy bản ghi" });
                }
                _categoryRepository.Delete(id);
                SetAlert(ShopCommon.Contants.DELETE_SUCCESS, ShopCommon.Contants.SUCCESS);
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> ChangeStatus(int id)
        {
            var result = await _categoryRepository.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

    }
}