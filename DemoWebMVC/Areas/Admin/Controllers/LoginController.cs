﻿using DemoWebMVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ShopBusiness.Models;
using ShopReponsitory;
using ShopRepository;
using System.Security.Claims;

namespace DemoWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : BaseController
    {
        public IUserRepository userRepository = null;
        private readonly IRoleRepository roleRepository = null;
        public LoginController()
        {
            userRepository = new UserRepository();
            roleRepository = new RoleRepository();
        }
        public async Task<IActionResult> Index(string ReturnUrl = null)
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                var userName = userLogin.UserName;
                var password = ShopCommon.Library.EncryptMD5(userLogin.Password);
                var user = await userRepository.GetUserByUserNamePassword(userName, password);
                var role = await roleRepository.GetRoleById(user.RoleId);
                ViewData["Role"] = role.RoleName;
                if (user != null)
                {
                    // A claim is a statement about a subject by an issuer and
                    //represent attributes of the subject that are useful in the context of authentication and authorization operations.
                    var claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name, userName),
                         new Claim("FullName", user.FullName),
                        new Claim(ClaimTypes.Role, "Admin"),
                    };
                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                    var identity = new ClaimsIdentity(claims, "Admin");
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                    HttpContext.SignInAsync("Admin", principal, new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });
                    var routeValues = new RouteValueDictionary
                {
                    {"area","Admin" },
                    {"returnURL",Request.Query["ReturnUrl"] },
                    {"claimValue","true" }
                };
                    if (TempData["ReturnUrl"] != null)
                    {
                        return Redirect(TempData["ReturnUrl"].ToString());
                    }
                    return RedirectToAction("Index", "Home", routeValues);
                }
                else
                {
                    TempData["Message"] = "Login không thành công. Kiểm tra lại thông tin của Username hoặc Password";
                    TempData["AlertType"] = "danger";
                }
            }
            return View(nameof(Index));
        }


        public IActionResult Logout()
        {
            // Đăng xuất người dùng
            HttpContext.SignOutAsync("Admin");
            SetAlert("Đăng xuất thành công!", "success");
            // Chuyển hướng đến trang đăng nhập hoặc trang chính
            return RedirectToAction("Index", "Login", new { area = "Admin" });
            // Thay thế bằng tên trang đăng nhập hoặc trang chính của bạn
        }

    }
}