    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopBusiness.Models;
using ShopReponsitory;
using ShopRepository;
using X.PagedList;


namespace DemoWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : BaseController
    {
        IRoleRepository roleRepository;

        public RolesController()
        {   
            roleRepository = new RoleRepository();
        }

        // GET: Admin/Roles
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            var role = await roleRepository.GetAllRole();
            if (!string.IsNullOrEmpty(searchString))
            {
                role = role.Where(c => ShopCommon.Library.ConvertToUnSign(c.RoleName.ToLower()).Contains(ShopCommon.Library.ConvertToUnSign(searchString.ToLower())));
            }
            ViewBag.Page = 5;
            return View(role.ToPagedList(page ?? 1, (int)ViewBag.Page));
        }


        // GET: Admin/Roles/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                await roleRepository.Add(role);
                SetAlert(ShopCommon.Contants.UPDATE_SUCCESS, ShopCommon.Contants.SUCCESS);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Admin/Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var role = await roleRepository.GetRoleById(Convert.ToInt32(id));
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,RoleName")] Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await roleRepository.Update(role);
                SetAlert(ShopCommon.Contants.UPDATE_SUCCESS, ShopCommon.Contants.SUCCESS);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteId(int id)
        {
            try
            {
                var category = await roleRepository.GetRoleById(id);
                if (category == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy bản ghi" });
                }
                await roleRepository.Delete(id);
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

    }
}
