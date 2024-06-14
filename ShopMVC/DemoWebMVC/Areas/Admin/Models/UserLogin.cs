using System.ComponentModel.DataAnnotations;

namespace DemoWebMVC.Areas.Admin.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage ="Yêu cầu nhập tên đăng nhập ")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu ")]
        public string Password { get; set; }
    }
}
