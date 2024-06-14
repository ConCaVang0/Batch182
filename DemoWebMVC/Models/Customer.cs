using System.ComponentModel.DataAnnotations;

namespace DemoWebMVC.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Display(Name="Tên khách hàng")]
        [Required(ErrorMessage ="Yêu cầu phải nhập tên khách hàng")]
        public string Name { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Yêu cầu phải nhập địa chỉ")]
        public string Address { get; set; }

    }
}
