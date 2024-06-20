using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopBusiness.Models;

public partial class User
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string UserName { get; set; }

    [DataType(DataType.Password)]
    public string? Password { get; set; } 

    public string? FullName { get; set; }

    public string Email { get; set; } 

    public bool Status { get; set; }

    public virtual ICollection<Product>? Products { get; set; }

    public virtual Role? Role { get; set; } = null!;
}
