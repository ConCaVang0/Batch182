using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopBusiness.Models;
using ShopRepository;
using X.PagedList;

namespace DemoWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : BaseController
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IUserRepository userRepository;

        public ProductsController()
        {
            productRepository = new ProductRepository();
            categoryRepository = new CategoryRepository();
            userRepository = new UserRepository();
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index(string searchString, int? page, int categoyId)
        {
            var product = await productRepository.GetAllProduct();
            if (!string.IsNullOrEmpty(searchString))
            {
                product = product.Where(c => ShopCommon.Library.ConvertToUnSign(c.ProductName.ToLower()).Contains(ShopCommon.Library.ConvertToUnSign(searchString.ToLower())));
            }
            if (categoyId != 0)
            {
                product = product.Where(u => u.CategoryId == categoyId);
            }
            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllCategory(), "CategoryId", "CategoryName");
            ViewBag.Page = 5;
            return View(product.ToPagedList(page ?? 1, (int)ViewBag.Page));
        }

        // GET: Admin/Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllCategory(), "CategoryId", "CategoryName");
            ViewData["UserPost"] = new SelectList(await userRepository.GetAllUser(), "UserId", "FullName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,Ncontent,CategoryId,ImageUrl,Price,CreatePost,UserPost,Status")] Product product)
        {

            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllCategory(), "CategoryId", "CategoryName");
            ViewData["UserPost"] = new SelectList(await userRepository.GetAllUser(), "UserId", "FullName");
            if (ModelState.IsValid)
            {
                await productRepository.Add(product);
                SetAlert(ShopCommon.Contants.UPDATE_SUCCESS, ShopCommon.Contants.SUCCESS);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var product = await productRepository.GetProductById(Convert.ToInt32(id));
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllCategory(), "RoleId", "RoleName", product.CategoryId);
            ViewData["UserPost"] = new SelectList(await userRepository.GetAllUser(), "UserId", "FullName", product.UserPost);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,Ncontent,CategoryId,ImageUrl,Price,CreatePost,UserPost,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                await productRepository.Update(product);
                SetAlert(ShopCommon.Contants.UPDATE_SUCCESS, ShopCommon.Contants.SUCCESS);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllCategory(), "RoleId", "RoleName", product.CategoryId);
            ViewData["UserPost"] = new SelectList(await userRepository.GetAllUser(), "UserId", "FullName", product.UserPost);
            return View(product);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteId(int id)
        {
            try
            {
                var category = await productRepository.GetProductById(id);
                if (category == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy bản ghi" });
                }
                await productRepository.Delete(id);
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
            var result = await productRepository.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

    }
}