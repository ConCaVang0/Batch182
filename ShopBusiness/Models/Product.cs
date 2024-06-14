using System;
using System.Collections.Generic;

namespace ShopBusiness.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public string? Ncontent { get; set; }

    public int CategoryId { get; set; }

    public string? ImageUrl { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatePost { get; set; }

    public int UserPost { get; set; }

    public bool Status { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual User UserPostNavigation { get; set; } = null!;
}
