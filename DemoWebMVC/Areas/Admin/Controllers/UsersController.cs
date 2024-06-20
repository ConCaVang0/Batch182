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
    public class UsersController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        public UsersController()
        {
            userRepository = new UserRepository();
            roleRepository = new RoleRepository();
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index(string searchString, int? page, int roleId)
        {
            var user = await userRepository.GetAllUser();
            if (!string.IsNullOrEmpty(searchString))
            {
                user = user.Where(c => ShopCommon.Library.ConvertToUnSign(c.FullName.ToLower()).Contains(ShopCommon.Library.ConvertToUnSign(searchString.ToLower())));
            }
            if (roleId != 0)
            {
                user = user.Where(u => u.RoleId == roleId);
            }
            ViewData["RoleId"] = new SelectList(await roleRepository.GetAllRole(), "RoleId", "RoleName");
            ViewBag.Page = 5;
            return View(user.ToPagedList(page ?? 1, (int)ViewBag.Page));
        }

        // GET: Admin/Users/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RoleId"] = new SelectList(await roleRepository.GetAllRole(), "RoleId", "RoleName");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RoleId,UserName, Password, FullName,Email,Status")] User user)
        {
            ViewData["RoleId"] = new SelectList(await roleRepository.GetAllRole(), "RoleId", "RoleName", user.RoleId);

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(user.Password))
                {
                    SetAlert(ShopCommon.Contants.PASSWORD_FAIL, ShopCommon.Contants.FAIL);
                    return View(user);
                }
                user.Password = ShopCommon.Library.EncryptMD5(user.Password);
                await userRepository.Add(user);
                SetAlert(ShopCommon.Contants.UPDATE_SUCCESS, ShopCommon.Contants.SUCCESS);
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await userRepository.GetUserById(Convert.ToInt32(id));
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(await roleRepository.GetAllRole(), "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,RoleId,UserName,Password,FullName,Email,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(user.Password))
                {
                    var currentPassword = await userRepository.GetUserById(user.UserId);
                    user.Password = currentPassword.Password;
                }
                else
                {
                    user.Password = ShopCommon.Library.EncryptMD5(user.Password);
                }
                await userRepository.Update(user);
                SetAlert(ShopCommon.Contants.UPDATE_SUCCESS, ShopCommon.Contants.SUCCESS);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(await roleRepository.GetAllRole(), "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteId(int id)
        {
            try
            {
                var category = await userRepository.GetUserById(id);
                if (category == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy bản ghi" });
                }
                await userRepository.Delete(id);
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
            var result = await userRepository.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}