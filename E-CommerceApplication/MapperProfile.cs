using AutoMapper;
using E_CommerceApplication.Commands;
using E_CommerceApplication.Queries;
using E_CommerceCore.Entities;
using SharedDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication
{
    internal class MapperProfile:Profile
    {
        public MapperProfile():base()
        {
            base.CreateMap<GetCategoriesQuery, CategoriesDTO>().ReverseMap();
          
            base.CreateMap<Categories, CategoriesDTO>().ReverseMap();

           
            base.CreateMap<Products, ProductsDTO>().ReverseMap();
            
        }
    }
}
