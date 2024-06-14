using DemoWebMVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopRepository;

namespace DemoWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        public IUserRepository userRepository = null;
        public LoginController() {
            userRepository = new UserRepository();
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Hello";
            return View();  
        }
        [HttpPost]
        public IActionResult Index(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                var userName = userLogin.UserName;
                var password = ShopCommon.Library.EncryptMD5(userLogin.Password);
                var user = userRepository.GetUserByUserNamePassword(userName , password);       
                if (user != null)
                {
                   /* TempData["Message"] = "Login thành công";
                    TempData["AlertType"] = "success"; */
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    TempData["Message"] = " Login không thành công. Lỗi UserName hoặc Password ";
                    TempData["AlertType"] = "danger";
                }
            }
            return View(nameof(Index));
        }
    }
}
