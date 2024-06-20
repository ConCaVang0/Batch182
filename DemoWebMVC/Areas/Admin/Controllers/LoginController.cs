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
        public LoginController()
        {
            userRepository = new UserRepository();
        }
        public async Task<IActionResult> Index()
        {
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
                if (user != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "Login không thành công. Kiểm tra lại thông tin của Username hoặc Password";
                    TempData["AlertType"] = "danger";
                }
            }
            return View(nameof(Index));
        }


    }
}
