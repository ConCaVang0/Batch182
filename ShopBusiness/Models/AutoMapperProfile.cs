using AutoMapper;
using ShopBusiness.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DemoWebMVC.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>();
        }
    }
}