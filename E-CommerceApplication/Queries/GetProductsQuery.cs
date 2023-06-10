using AutoMapper;
using Azure.Core;
using E_CommerceCore.IRepositories;
using E_CommerceInfrastructure.Repositories;
using MediatR;
using SharedDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Queries
{
    public class GetProductsQuery:IRequest<ProductsWithPages>
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; }
        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsWithPages>
        {
            private IProductsRepository productsRepository;
            private IMapper Mapper;
            public GetProductsQueryHandler(IProductsRepository _IProductsRepository, IMapper mapper)
            {
                productsRepository = _IProductsRepository;
                Mapper = mapper;
            }
            public async Task<ProductsWithPages> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
              
                var productsLst=await productsRepository.GetProductsWithPagenation(request.PageIndex,request.PageSize);
                var groupingLst = productsLst.Select(g => new CategoryProductDTO { 
                    CategoryName=g.Key.Name,
                    Products= Mapper.Map<List<ProductsDTO>>(g.ToList())
                }).ToList();
                var totalItems = await productsRepository.GetCountAsyn();
                var totalPages= (int)Math.Ceiling(totalItems / (double)request.PageSize);
               
                
                return new ProductsWithPages {
                    LstCategoryProductsDTO= groupingLst,                    
                    TotalPages=totalPages,
                    PageIndex= request.PageIndex
                };
            }
        }
    }
}
