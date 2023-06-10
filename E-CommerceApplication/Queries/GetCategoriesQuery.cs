using AutoMapper;
using E_CommerceCore.Entities;
using E_CommerceCore.IRepositories;
using MediatR;
using SharedDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Queries
{
    public class GetCategoriesQuery : IRequest<List<CategoriesDTO>>
    {
      
        public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoriesDTO>>
        {
            private ICategoriesRepository categoriesRepository;
            private IMapper Mapper;
       

            public GetCategoriesQueryHandler(ICategoriesRepository _categoriesRepository, IMapper mapper)
            {
                categoriesRepository = _categoriesRepository;
                Mapper = mapper;
            }
            public async Task<List<CategoriesDTO>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
            {
                var result = await categoriesRepository.GetAllAsyn();
                var lstCategoriesDTO = Mapper.Map<List<CategoriesDTO>>(result);
                return lstCategoriesDTO;
            }
        }
    }
}
