using System;
using System.Collections.Generic;

namespace ShopBusiness.Models;

public partial class User
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Role Role { get; set; } = null!;
}
