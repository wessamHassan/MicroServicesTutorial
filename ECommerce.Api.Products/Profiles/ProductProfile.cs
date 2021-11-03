using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Profiles
{
    public class ProductProfile:AutoMapper.Profile
    {
        public ProductProfile()
        {
            //here I fill the field IsPriceMoreThan10 in the model(dest) if the price int DB (src )> 10  
            //with no need to add same field in DB 
            CreateMap<Db.Product, Models.Product>()                
               .ForMember(destinationMember:dest=> dest.IsPriceMoreThan10,memberOptions: opt=> opt.MapFrom(src => src.Price > 10));
            //we can do something like that:
            //.ForMember(destinationMember:dest=> dest.ProductName, opt=> opt.MapFrom(src => src.nm));
            CreateMap<Models.Product, Db.Product>();
        }
    }
}
