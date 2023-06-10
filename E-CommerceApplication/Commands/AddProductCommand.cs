using AutoMapper;
using E_CommerceApplication.Queries;
using E_CommerceCore.Entities;
using E_CommerceCore.IRepositories;
using MediatR;
using SharedDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Commands
{
    public class AddProductCommand :ProductsDTO, IRequest<APIResult>
    {
       
        public class AddProductCommandHandler : IRequestHandler<AddProductCommand, APIResult>
        {
            private IProductsRepository productsRepository;
            private IMapper Mapper;
            public APIResult aPIResult { get; set; }
            public AddProductCommandHandler(IProductsRepository _productsRepository, IMapper mapper)
            {
                productsRepository = _productsRepository;
                Mapper = mapper;
                aPIResult = new APIResult();
            }
            public async Task<APIResult> Handle(AddProductCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var oNewDbProduct = Mapper.Map<Products>(request);
                    var result = await productsRepository.AddAsyn(oNewDbProduct);
                    await productsRepository.SaveChangesAsyn();
                    return aPIResult.SaveSucess(result);
                }
                catch (Exception e)
                {
                    return aPIResult.SaveFailer(e);
                }
               
            }
        }
    }
}
