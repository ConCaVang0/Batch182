using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopBusiness.Models;

public partial class Category
{
    [Display(Name = "Mã danh mục")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Yêu cầu nhập tên danh mục")]
    [Display(Name = "Tên danh mục")]
    [MaxLength(100)]
    public string? CategoryName { get; set; }
    [Display(Name = "Trạng thái")]
    public bool Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}